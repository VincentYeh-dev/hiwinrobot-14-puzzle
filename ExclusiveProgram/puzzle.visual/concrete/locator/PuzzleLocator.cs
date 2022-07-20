using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using ExclusiveProgram.puzzle.visual.framework;
using ExclusiveProgram.puzzle.visual.framework.utils;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ExclusiveProgram.puzzle.visual.concrete
{
    public class PuzzleLocator : IPuzzleLocator
    {
        private readonly Size minSize;
        private readonly Size maxSize;
        private readonly IPuzzlePreprocessImpl preProcessImpl;
        private readonly IPuzzleGrayConversionImpl grayConversionImpl;
        private readonly IPuzzleThresholdImpl thresholdImpl;
        private readonly IPuzzleBinaryPreprocessImpl binaryPreprocessImpl;
        private readonly double approx_paramater;

        public PuzzleLocator(Size minSize, Size maxSize, IPuzzlePreprocessImpl preProcessImpl, IPuzzleGrayConversionImpl grayConversionImpl, IPuzzleThresholdImpl thresholdImpl, IPuzzleBinaryPreprocessImpl binaryPreprocessImpl, double approx_paramater = 0.005f)
        {
            this.approx_paramater = approx_paramater;
            this.minSize = minSize;
            this.maxSize = maxSize;
            this.preProcessImpl = preProcessImpl;
            this.grayConversionImpl = grayConversionImpl;
            this.thresholdImpl = thresholdImpl;
            this.binaryPreprocessImpl = binaryPreprocessImpl;
        }


        public List<LocationResult> Locate(Image<Bgr, byte> rawImage)
        {
            var preprocessImage = rawImage.Clone();
            if (preProcessImpl != null)
                preProcessImpl.Preprocess(preprocessImage, preprocessImage);

            var binaryImage = new Image<Gray, byte>(preprocessImage.Size);
            grayConversionImpl.ConvertToGray(preprocessImage, binaryImage);
            thresholdImpl.Threshold(binaryImage, binaryImage);
            binaryPreprocessImpl.BinaryPreprocess(binaryImage, binaryImage);
            binaryImage.Save("results\\binary.jpg");

            var contours = FindContours(binaryImage);

            List<LocationResult> location_results = new List<LocationResult>();

            //取得輪廓組

            var preview_image = rawImage.Clone();

            int valid_id = 0;
            //尋遍輪廓組之單一輪廓
            for (int i = 0; i < contours.Size; i++)
            {
                //多邊形逼近之套件
                VectorOfPoint approxContour = GetApproxContour(contours[i]);

                CvInvoke.Polylines(preview_image, approxContour, true, new MCvScalar(0, 0, 255), 2);

                //框選輪廓最小矩形
                Rectangle minRectangle = CvInvoke.BoundingRectangle(approxContour);

                //畫在圖片上
                CvInvoke.Rectangle(preview_image, minRectangle, new MCvScalar(255, 0, 0), 2);

                //獲得最小旋轉矩形，取得角度用
                RotatedRect minAreaRotatedRectangle = CvInvoke.MinAreaRect(approxContour);
                //var angle = GetAngle(minAreaRotatedRectangle, minRectangle);
                var corner_points = GetCornerPoints(minAreaRotatedRectangle);
                Point coordinate = GetCentralCoordinate(approxContour);


                LocationResult location_result = new LocationResult();
                if (CheckDuplicatePuzzlePosition(location_results, coordinate) && CheckSize(minRectangle, coordinate))
                {
                    CvInvoke.Polylines(preview_image, corner_points, true, new MCvScalar(0, 255, 255), 2);
                    CvInvoke.Circle(preview_image, coordinate, 1, new MCvScalar(0, 0, 255), 2);
                    location_result.ID = valid_id++;
                    //location_result.Angle = angle;
                    location_result.Coordinate = coordinate;
                    location_result.Size = new Size(minRectangle.Width, minRectangle.Height);
                    location_result.ROI = getROI(location_result.Coordinate, location_result.Size, rawImage);
                    location_result.BinaryROI = getBinaryROI(location_result.Coordinate, location_result.Size, binaryImage);

                    location_results.Add(location_result);
                }

            }
            preview_image.Save("results\\contours.jpg");
            preview_image.Dispose();

            return location_results;
        }

        private VectorOfPoint GetApproxContour(VectorOfPoint contour)
        {
            VectorOfPoint approxContour = new VectorOfPoint();
            //將輪廓組用多邊形框選 「0.05」為可更改逼近值
            CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * approx_paramater, true);
            return approxContour;
        }

        private Point[] GetCornerPoints(RotatedRect boundingBox)
        {
            return Array.ConvertAll<PointF, Point>(boundingBox.GetVertices(), Point.Round);
        }

        private Image<Bgr, byte> getROI(Point Coordinate, Size Size, Image<Bgr, byte> input)
        {
            Rectangle rect = new Rectangle((int)(Coordinate.X - Size.Width / 2.0f), (int)(Coordinate.Y - Size.Height / 2.0f), Size.Width, Size.Height);

            //將ROI選取區域使用Mat型式讀取
            return new Mat(input.Mat, rect).ToImage<Bgr, byte>();
        }
        private Image<Gray, byte> getBinaryROI(Point Coordinate, Size Size, Image<Gray, byte> input)
        {
            Rectangle rect = new Rectangle((int)(Coordinate.X - Size.Width / 2.0f), (int)(Coordinate.Y - Size.Height / 2.0f), Size.Width, Size.Height);

            //將ROI選取區域使用Mat型式讀取
            return new Mat(input.Mat, rect).ToImage<Gray, byte>();
        }

        private bool CheckSize(Rectangle rect, Point Position)
        {
            if (rect.Size.Width < minSize.Width || rect.Size.Height < minSize.Height)
                return false;

            if (rect.Size.Width > maxSize.Width || rect.Size.Height > maxSize.Height)
                return false;

            if (Position.X - rect.Width / 2.0f < 0)
                return false;

            if (Position.Y - rect.Height / 2.0f < 0)
                return false;

            return true;
        }

        private Point GetCentralCoordinate(VectorOfPoint contour)
        {
            //畫出最小外切圓，獲得圓心用
            CircleF Puzzle_circle = CvInvoke.MinEnclosingCircle(contour);
            return new Point((int)Puzzle_circle.Center.X, (int)Puzzle_circle.Center.Y);
        }

        private VectorOfVectorOfPoint FindContours(Image<Gray, byte> image)
        {
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(image, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);
            return contours;
        }

        #region 檢查重複項

        /// <summary>
        /// 檢查重複項
        /// </summary>
        /// <param name="currentPuzzlePosition"></param>
        /// <returns></returns>
        private bool CheckDuplicatePuzzlePosition(List<LocationResult> definedPuzzleDataList, Point currentPuzzlePosition)
        {
            bool Answer = true;
            for (int i = 0; i < definedPuzzleDataList.Count; i++)
            {
                int x_up = definedPuzzleDataList[i].Coordinate.X + 10,
                    x_down = definedPuzzleDataList[i].Coordinate.X - 10,
                    y_up = definedPuzzleDataList[i].Coordinate.Y + 10,
                    y_down = definedPuzzleDataList[i].Coordinate.Y - 10;

                if (currentPuzzlePosition.X < x_up && currentPuzzlePosition.X > x_down && currentPuzzlePosition.Y < y_up && currentPuzzlePosition.Y > y_down)
                    Answer = false;
            }
            return Answer;
        }

        #endregion 檢查重複項
        private float GetAngle(RotatedRect BoundingBox, Rectangle rect)
        {
            //計算角度
            //詳情請詢問我

            ////畫出最小矩形
            //CvInvoke.Rectangle(Ori_img, rect, new MCvScalar(0, 0, 255));

            //畫出可旋轉矩形
            //CvInvoke.Polylines(Ori_img, Array.ConvertAll(BoundingBox.GetVertices(), Point.Round), true, new Bgr(Color.DeepPink).MCvScalar, 3);


            //return BoundingBox.Angle;

            float Angel = 0;
            if (rect.Size.Width < rect.Size.Height)
            {
                Angel = -(BoundingBox.Angle + 90);
            }
            else
            {
                Angel = -BoundingBox.Angle;
            }


            return Angel;
        }
    }
}
