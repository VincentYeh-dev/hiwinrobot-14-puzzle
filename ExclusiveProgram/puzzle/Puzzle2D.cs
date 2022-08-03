using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace ExclusiveProgram.puzzle
{
    public struct Puzzle2D
    {
        public Point Coordinate;
        public Image<Bgr,byte> ROI;
    };
}
