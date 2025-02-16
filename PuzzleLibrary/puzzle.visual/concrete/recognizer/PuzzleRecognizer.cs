﻿using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using PuzzleLibrary.puzzle.visual.framework;
using PuzzleLibrary.puzzle.visual.framework.utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
/*
 * https://www.796t.com/post/OXRpbnE=.html
 * https://www.twblogs.net/a/5cb5e690bd9eee0eff45642b
 */
namespace PuzzleLibrary.puzzle.visual.concrete
{

    public class PuzzleRecognizer : IPuzzleRecognizer
    {

        private Image<Bgr, byte> preprocessModelImage = null;
        private readonly PuzzleRecognizerImpl impl;
        private readonly IPreprocessImpl preprocessImpl;
        private readonly IGrayConversionImpl grayConversionImpl;
        private readonly IThresholdImpl thresholdImpl;
        private readonly IBinaryPreprocessImpl binaryPreprocessImpl;
        private readonly Image<Bgr, byte> modelImage;
        private readonly float width_per_puzzle;
        private readonly float height_per_puzzle;
        private readonly float area_per_puzzle;
        private readonly double uniquenessThreshold;


        public PuzzleRecognizer(Image<Bgr, byte> modelImage, double uniquenessThreshold, PuzzleRecognizerImpl impl, IPreprocessImpl preprocessImpl, IGrayConversionImpl grayConversionImpl, IThresholdImpl thresholdImpl, IBinaryPreprocessImpl binaryPreprocessImpl)
        {
            this.modelImage = modelImage;

            width_per_puzzle  = (modelImage.Width / 7.0f);
            height_per_puzzle = (modelImage.Height / 5.0f);
            area_per_puzzle = width_per_puzzle*height_per_puzzle;
            this.uniquenessThreshold = uniquenessThreshold;
            this.impl = impl;
            this.preprocessImpl = preprocessImpl;
            this.grayConversionImpl = grayConversionImpl;
            this.thresholdImpl = thresholdImpl;
            this.binaryPreprocessImpl = binaryPreprocessImpl;
        }

        public void PreprocessModelImage()
        {
            preprocessModelImage = new Image<Bgr, byte>(modelImage.Size);
            if (preprocessImpl != null)
                preprocessImpl.Preprocess(modelImage, preprocessModelImage);
            else
                preprocessModelImage = modelImage;
        }

        public bool ModelImagePreprocessIsDone()
        {
            return preprocessModelImage != null;
        }

        public RecognizeResult Recognize(int id, Image<Bgr, byte> image, List<Puzzle3D> ignoredPuzzles)
        {
            Image<Bgr, byte> observedImage = image.Clone();

            if (preprocessImpl != null)
                preprocessImpl.Preprocess(observedImage, observedImage);

            var IgnoredModelImage = GetIgnoredModelImage(modelImage,ignoredPuzzles);
            RecognizeResult result = new RecognizeResult();

            FindFeaturePointsAndMatch(IgnoredModelImage.Mat, observedImage.Mat, 
                out var modelKeyPoints, out var observedKeyPoints,
                out var matches,out var mask);

            DrawMatchesAndSave(id,IgnoredModelImage, modelKeyPoints, observedImage, observedKeyPoints,
               matches,mask);

            Mat homography = FindHomography(modelKeyPoints, observedKeyPoints, matches, mask);

            result.Angle = -Math.Atan2(GetDoubleValue(homography, 0, 1), GetDoubleValue(homography, 0, 0)) * 180 / Math.PI;

            Mat invert_homography = homography.Clone();
            CvInvoke.Invert(invert_homography, invert_homography, DecompMethod.Svd);
            var warpImage = new Mat(IgnoredModelImage.Size, IgnoredModelImage.Mat.Depth, 3);
            CvInvoke.WarpPerspective(observedImage, warpImage, invert_homography, preprocessModelImage.Size);

            Point point = FindCoordinateOnModelImage(id, warpImage.ToImage<Bgr, byte>());

            var preview_image = warpImage.Clone();

            Rectangle rect = new Rectangle(Point.Empty, observedImage.Size);

            /*
             0     左下
             1     右下
             2     右上
             3     左上
            */
            PointF[] points_on_observedImage = new PointF[]
            {
                      new PointF(rect.Left, rect.Bottom),
                      new PointF(rect.Right, rect.Bottom),
                      new PointF(rect.Right, rect.Top),
                      new PointF(rect.Left, rect.Top)
            };

            var point_on_modelImage = CvInvoke.PerspectiveTransform(points_on_observedImage, invert_homography);
            Point[] points = Array.ConvertAll<PointF, Point>(point_on_modelImage, Point.Round);

            CvInvoke.Polylines(preview_image, points, true, new MCvScalar(0, 0, 255));
            CvInvoke.Circle(preview_image, point, 3, new MCvScalar(0, 0, 255), 2);

            for (int i = 1; i <= 5; i++)
            {
                var point1 = new Point(0, (int)height_per_puzzle * i);
                var point2 = new Point(preview_image.Width - 1, (int)height_per_puzzle * i);
                CvInvoke.Line(preview_image, point1, point2, new MCvScalar(0, 0, 255), 2);
            }

            for (int j = 1; j <= 7; j++)
            {
                var point1 = new Point((int)width_per_puzzle * j, 0);
                var point2 = new Point((int)width_per_puzzle * j, preview_image.Height - 1);
                CvInvoke.Line(preview_image, point1, point2, new MCvScalar(0, 0, 255), 2);
            }

            int x = (int)(point.X / width_per_puzzle);
            int y = (int)(point.Y / height_per_puzzle);

            if (x >= 7)
                x = 6;
            if (y >= 5)
            { y = 4; }
            
            result.Position = new Point(x, y);

            DrawPerspectiveAndSave(id, preview_image.ToImage<Bgr, byte>(), result.Position.ToString());
            return result;
        }

        private Image<Bgr,byte> GetIgnoredModelImage(Image<Bgr,byte> image,List<Puzzle3D> ignoredPuzzles)
        {
            if(ignoredPuzzles==null)
                return image;
            var newImage = image.Clone();
            foreach(var puzzle in ignoredPuzzles)
            {
                var x = puzzle.Position.X* width_per_puzzle;
                var y=  puzzle.Position.Y* height_per_puzzle;
                CvInvoke.Rectangle(newImage, new Rectangle((int)x,(int)y,(int)width_per_puzzle,(int)height_per_puzzle), new MCvScalar(0, 0, 0), -1);
            }

            return newImage;
        }
        public RecognizeResult Recognize(int id, Image<Bgr, byte> image)
        {
            return Recognize(id, image, null);
        }

        private void DrawPerspectiveAndSave(int id, Image<Bgr, byte> image, string position)
        {
            CvInvoke.PutText(image, string.Format("position: {0}", position), new Point(1, 50), FontFace.HersheySimplex, 1, new MCvScalar(100, 100, 255), 2, LineType.FourConnected);
            image.Save("results\\perspective_" + id + ".jpg");
        }

        private void DrawMatchesAndSave(int id, Image<Bgr, byte> modelImage, VectorOfKeyPoint modelKeyPoints, Image<Bgr, byte> observedImage, VectorOfKeyPoint observedKeyPoints, VectorOfVectorOfDMatch matches, Mat mask)
        {
            Mat resultImage = new Mat();
            Features2DToolbox.DrawMatches(modelImage, modelKeyPoints, observedImage, observedKeyPoints,
               matches, resultImage, new MCvScalar(0, 0, 255), new MCvScalar(255, 255, 255), mask);

            resultImage.Save("results\\matching_" + id + ".jpg");
            resultImage.Dispose();
        }

        private Point FindCoordinateOnModelImage(int id, Image<Bgr, byte> warpImage)
        {
            var stage1 = new Image<Bgr, byte>(warpImage.Size);
            if (preprocessImpl != null)
                preprocessImpl.Preprocess(warpImage, stage1);
            else
                stage1 = warpImage;


            var binaryImage = new Image<Gray, byte>(stage1.Size);
            grayConversionImpl.ConvertToGray(stage1, binaryImage);
            thresholdImpl.Threshold(binaryImage, binaryImage);
            if (binaryPreprocessImpl != null)
                binaryPreprocessImpl.BinaryPreprocess(binaryImage, binaryImage);

            if (CvInvoke.CountNonZero(binaryImage) > 1.2 * area_per_puzzle)
                throw new Exception("辨識結果大於1.5張拼圖");


            //取得輪廓組套件
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(binaryImage, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);

            var resultRectangle = new Rectangle();
            int max = -1;
            //尋遍輪廓組之單一輪廓
            for (int i = 0; i < contours.Size; i++)
            {
                VectorOfPoint contour = contours[i];
                //多邊形逼近之套件
                VectorOfPoint approxContour = new VectorOfPoint();

                //將輪廓組用多邊形框選 「0.05」為可更改逼近值
                CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * 0.005, true);

                //畫出最小框選矩形，裁切用
                Rectangle BoundingBox_ = CvInvoke.BoundingRectangle(approxContour);
                //Rectangle BoundingBox_ = CvInvoke.BoundingRectangle(contour);

                int current = BoundingBox_.Width * BoundingBox_.Height;
                if (current >= max)
                {
                    resultRectangle = BoundingBox_;
                    max = current;
                }

            }

            //畫在圖片上
            CvInvoke.Rectangle(binaryImage, resultRectangle, new MCvScalar(255, 0, 0), 2);

            binaryImage.Save("results\\G" + id + ".jpg");

            int x_central = (resultRectangle.Left + resultRectangle.Right) / 2;
            int y_central = (resultRectangle.Top + resultRectangle.Bottom) / 2;
            return new Point(x_central, y_central);
        }

        //四捨五入至想要位數
        private static double Round(double value, int d)
        {
            return Math.Round(value / Math.Pow(10, d)) * Math.Pow(10, d);
        }

        private void FindFeaturePointsAndMatch(Mat modelImage, Mat observedImage, out VectorOfKeyPoint out_modelKeyPoints, out VectorOfKeyPoint out_observedKeyPoints, out VectorOfVectorOfDMatch out_matches,out Mat mask)
        {
            var modelKeyPoints = new VectorOfKeyPoint();
            var observedKeyPoints = new VectorOfKeyPoint();
            var matches = new VectorOfVectorOfDMatch();
            var good_matches = new VectorOfVectorOfDMatch();

            Mat modelDescriptors = new Mat();
            impl.DetectFeatures(modelImage, null, modelKeyPoints, modelDescriptors, false);

            Mat observedDescriptors = new Mat();
            impl.DetectFeatures(observedImage, null, observedKeyPoints, observedDescriptors, false);
            impl.MatchFeatures(modelDescriptors, observedDescriptors, modelKeyPoints, observedKeyPoints, matches);

            mask = new Mat(matches.Size, 1, DepthType.Cv8U, 1);
            mask.SetTo(new MCvScalar(255));
            Features2DToolbox.VoteForUniqueness(matches, uniquenessThreshold, mask);
            out_modelKeyPoints = modelKeyPoints;
            out_observedKeyPoints = observedKeyPoints;
            out_matches = matches;

        }

        private Mat FindHomography(VectorOfKeyPoint modelKeyPoints, VectorOfKeyPoint observedKeyPoints, VectorOfVectorOfDMatch matches, Mat mask)
        {
            int nonZeroCount = CvInvoke.CountNonZero(mask);
            if (nonZeroCount >= 4)
            {
                nonZeroCount = Features2DToolbox.VoteForSizeAndOrientation(modelKeyPoints, observedKeyPoints, matches, mask, 1.5, 20);
                if (nonZeroCount >= 4)
                {
                    Mat homography = Features2DToolbox.GetHomographyMatrixFromMatchedFeatures(modelKeyPoints, observedKeyPoints, matches, mask, 2);
                    if (homography == null)
                        throw new Exception("Matrix can not be recoverd.");

                    return homography;
                }
            }

            throw new Exception("No enough non-zero element.(>=4)");
        }

        private double GetDoubleValue(Mat mat, int row, int col)
        {
            var value = new double[1];
            Marshal.Copy(mat.DataPointer + (row * mat.Cols + col) * mat.ElementSize, value, 0, 1);
            return value[0];
        }

    }
}
