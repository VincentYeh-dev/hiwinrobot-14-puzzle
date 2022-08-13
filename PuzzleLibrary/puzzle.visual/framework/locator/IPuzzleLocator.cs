using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleLibrary.puzzle.visual.framework
{
    

    public struct LocationResult
    {
        public PointF Coordinate;
        //public double Angle;
        public Size Size;
        public Image<Bgr,byte> ROI;
        public int ID;
    }

    public interface IPuzzleLocator
    {
        List<LocationResult> Locate(Image<Bgr, byte> rawImage,Rectangle ROI,int IDOfStart=0);
    }
}
