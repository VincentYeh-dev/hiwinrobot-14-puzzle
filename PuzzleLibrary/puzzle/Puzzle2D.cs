using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace PuzzleLibrary.puzzle
{
    public struct Puzzle2D
    {
        public PointF Coordinate;
        public Rectangle ROI;
        public RotatedRect RotatedRect;
        public Image<Bgr,byte> Image;
    };
}
