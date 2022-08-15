using Emgu.CV;
using Emgu.CV.Aruco;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using PuzzleLibrary.puzzle.visual.framework;
using RASDK.Arm;
using RASDK.Vision.IDS;
using RASDK.Vision.Positioning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExclusiveProgram.puzzle
{
    public class VisionLocator  
    {
        private readonly RoboticArm arm ;
        private readonly IDSCamera camera;
        private readonly IPuzzleLocator locator;

        public VisionLocator(RoboticArm arm,IDSCamera camera,IPuzzleLocator locator)
        {
            this.arm = arm;
            this.camera = camera;
            this.locator = locator;
        }

        public List<LocationResult> Locate(Image<Bgr, byte> rawImage,Rectangle ROI)
        {
            var results = new List<LocationResult>();
            LocationResult? result=null;
            Func<PointF> func2 = () =>
            {
                var img = camera.GetImage().ToImage<Bgr,byte>();
                if(!result.HasValue)
                    result = locator.Locate(img,ROI,0)[0];   
                else
                    result=locator.Locate(img,new Rectangle(Point.Round(result.Value.Coordinate),result.Value.Size),0)[0];
                return result.Value.Coordinate;
            };
            var kp = (20.0 / 130.0) * 0.8;
            var func = VisualServo.BasicArmMoveFunc(arm,kp);
            VisualServo.Tracking(rawImage.Size, 10, func2, func);

            return results;
        }

        private Image<Bgr,byte> Mask(Image<Bgr, byte> rawImage, Rectangle ROI)
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

    }
}
