using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using PuzzleLibrary.puzzle.visual.framework;
using PuzzleLibrary.puzzle.visual.framework.utils;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace PuzzleLibrary.puzzle.visual.concrete
{
    public class PuzzleLocator : IPuzzleLocator
    {
        private readonly Size minSize;
        private readonly Size maxSize;
        private readonly IPreprocessImpl preProcessImpl;
        private readonly IGrayConversionImpl grayConversionImpl;
        private readonly IThresholdImpl thresholdImpl;
        private readonly IBinaryPreprocessImpl binaryPreprocessImpl;
        private readonly double approx_paramater;

        public PuzzleLocator(Size minSize, Size maxSize, IPreprocessImpl preProcessImpl, IGrayConversionImpl grayConversionImpl, IThresholdImpl thresholdImpl, IBinaryPreprocessImpl binaryPreprocessImpl, double approx_paramater = 0.005f)
        {
            this.approx_paramater = approx_paramater;
            this.minSize = minSize;
            this.maxSize = maxSize;
            this.preProcessImpl = preProcessImpl;
            this.grayConversionImpl = grayConversionImpl;
            this.thresholdImpl = thresholdImpl;
            this.binaryPreprocessImpl = binaryPreprocessImpl;
        }

        public List<LocationResult> Locate(Image<Bgr, byte> rawImage, Rectangle ROI,List<Puzzle3D> ignoredPuzzles,int IDOfStart)
        {
            var ROIImage = GetROIImage(rawImage,ROI);
            ROIImage = GetMaskedImage(ROIImage,ignoredPuzzles);
            ROIImage.Save("results\\roi.jpg");

            var preprocessImage = ROIImage.Clone();
            if (preProcessImpl != null)
                preProcessImpl.Preprocess(preprocessImage, preprocessImage);
            var channels =new VectorOfMat();
            CvInvoke.Split(preprocessImage,channels);
            channels[0].Save("results\\channel_B.jpg");
            channels[1].Save("results\\channel_G.jpg");
            channels[2].Save("results\\channel_R.jpg");

            var binaryImage = new Image<Gray, byte>(preprocessImage.Size);
            grayConversionImpl.ConvertToGray(preprocessImage, binaryImage);
            binaryImage.Save("results\\gray.jpg");
            thresholdImpl.Threshold(binaryImage, binaryImage);

            if(binaryPreprocessImpl!=null)
                binaryPreprocessImpl.BinaryPreprocess(binaryImage, binaryImage);

            binaryImage.Save("results\\binary.jpg");

            var contours = FindContours(binaryImage);

            List<LocationResult> location_results = new List<LocationResult>();

            //取得輪廓組

            var preview_image = ROIImage.Clone();

            int valid_id = 0;
            //尋遍輪廓組之單一輪廓
            for (int i = 0; i < contours.Size; i++)
            {
                //VectorOfPoint contour = GetHullContour(contours[i]);
                VectorOfPoint contour = contours[i];

                //框選輪廓最小矩形
                Rectangle minRectangle = CvInvoke.BoundingRectangle(contour);

                //獲得最小旋轉矩形，取得角度用
                RotatedRect minAreaRotatedRectangle = CvInvoke.MinAreaRect(contour);
                //var angle = GetAngle(minAreaRotatedRectangle, minRectangle);
                var corner_points = GetCornerPoints(minAreaRotatedRectangle);
                PointF coordinate = GetCentralCoordinateByMinEncloseingCircle(contour);


                LocationResult location_result = new LocationResult();
                if (CheckDuplicatePuzzlePosition(location_results, coordinate) && CheckSize(minRectangle, coordinate))
                {
                    //畫在圖片上
                    CvInvoke.Polylines(preview_image, contour, true, new MCvScalar(0, 0, 255), 2);
                    CvInvoke.Rectangle(preview_image, minRectangle, new MCvScalar(255, 0, 0), 2);
                    CvInvoke.Polylines(preview_image, corner_points, true, new MCvScalar(0, 255, 255), 2);
                    CvInvoke.Circle(preview_image, Point.Round(coordinate), 5, new MCvScalar(0, 0, 255),-1);
                    preview_image.Save("results\\contours.jpg");
                    location_result.ID = IDOfStart + valid_id++;
                    //location_result.Angle = angle;
                    location_result.Coordinate = coordinate;
                    location_result.Size = new Size(minRectangle.Width, minRectangle.Height);
                    location_result.ROI= minRectangle;
                    location_result.RotatedRect = minAreaRotatedRectangle;
                    location_result.Image= getROIImage(location_result.Coordinate, location_result.Size, rawImage);

                    location_results.Add(location_result);
                }

            }
            preview_image.Save("results\\contours.jpg");
            preview_image.Dispose();

            return location_results;
        }

        private Image<Bgr, byte> GetMaskedImage(Image<Bgr, byte> image, List<Puzzle3D> ignoredPuzzles)
        {
            if (ignoredPuzzles == null)
                return image;
            var masked= image.Clone();
            foreach(var puzzle in ignoredPuzzles)
            {
                var _2D = puzzle.puzzle2D;
                var points = new VectorOfPoint();
                points.Push(GetCornerPoints(_2D.RotatedRect));
                CvInvoke.FillPoly(masked,points, new MCvScalar(0, 0, 0));
                //CvInvoke.Polylines(masked, GetCornerPoints(), true, new MCvScalar(0, 0, 0), -1);
                //CvInvoke.Rectangle(masked, new Rectangle(Point.Round(_2D.Coordinate), _2D.Image.Size),new MCvScalar(0,0,0),-1);
            }
            return masked;
        }

        private Image<Bgr,byte> GetROIImage(Image<Bgr, byte> rawImage, Rectangle ROI)
        {
            if (ROI == Rectangle.Empty)
                return rawImage;

            Image<Bgr, byte> newImage = new Image<Bgr, byte>(rawImage.Size);
            for(int y = 0; y < newImage.Rows; y++)
            {
                for(int x=0;x<newImage.Cols; x++)
                {
                    if (x < ROI.Left || y < ROI.Top)
                        continue;

                    if (x > ROI.Right|| y>ROI.Bottom)
                        continue;

                    newImage.Data[y,x,0]=rawImage.Data[y,x,0];
                    newImage.Data[y,x,1]=rawImage.Data[y,x,1];
                    newImage.Data[y,x,2]=rawImage.Data[y,x,2];
                }
            }
            return newImage;
        }

        //private VectorOfPoint GetApproxContour(VectorOfPoint contour)
        //{
        //    VectorOfPoint approxContour = new VectorOfPoint();
        //    //將輪廓組用多邊形框選 「0.05」為可更改逼近值
        //    CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * approx_paramater, true);
        //    return approxContour;
        //}

        //private VectorOfPoint GetHullContour(VectorOfPoint contour)
        //{
        //    //VectorOfPoint approxContour = new VectorOfPoint();
        //    ////將輪廓組用多邊形框選 「0.05」為可更改逼近值
        //    //CvInvoke.ApproxPolyDP(contour, approxContour, CvInvoke.ArcLength(contour, true) * approx_paramater, true);
        //    var hull= new VectorOfPoint();
        //    CvInvoke.ConvexHull(contour,hull);
        //    return hull;
        //}

        private Point[] GetCornerPoints(RotatedRect boundingBox)
        {
            return Array.ConvertAll<PointF, Point>(boundingBox.GetVertices(), Point.Round);
        }

        private Image<Bgr, byte> getROIImage(PointF Coordinate, Size Size, Image<Bgr, byte> input)
        {
            Rectangle rect = new Rectangle((int)(Coordinate.X - Size.Width / 2.0f), (int)(Coordinate.Y - Size.Height / 2.0f), Size.Width, Size.Height);
            //設定ROI
            input.ROI = rect;
            //裁切ROI區域
            var new_image = input.Copy();
            //取消ROI
            input.ROI=Rectangle.Empty;

            //將ROI選取區域使用Mat型式讀取
            return new_image;
        }

        private bool CheckSize(Rectangle rect, PointF Position)
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

        private Point GetCentralCoordinateByMinEncloseingCircle(VectorOfPoint contour)
        {
            //畫出最小外切圓，獲得圓心用
            CircleF Puzzle_circle = CvInvoke.MinEnclosingCircle(contour);
            return new Point((int)Puzzle_circle.Center.X, (int)Puzzle_circle.Center.Y);
        }
        private PointF GetCentralCoordinateByMoments(VectorOfPoint contour)
        {
            var hu = CvInvoke.Moments(contour, false);
            double X = hu.M10 / hu.M00; //get center X
            double Y = hu.M01 / hu.M00; //get center y
            return new PointF((int)X, (int)Y);
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
        private bool CheckDuplicatePuzzlePosition(List<LocationResult> definedPuzzleDataList, PointF currentPuzzlePosition)
        {
            bool Answer = true;
            for (int i = 0; i < definedPuzzleDataList.Count; i++)
            {
                float x_up = definedPuzzleDataList[i].Coordinate.X + 10,
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

        public List<LocationResult> Locate(Image<Bgr, byte> rawImage, Rectangle ROI, int IDOfStart = 0)
        {
            return Locate(rawImage, ROI,null, IDOfStart);
        }
    }
}
