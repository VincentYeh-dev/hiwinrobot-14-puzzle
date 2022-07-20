using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;

namespace ExclusiveProgram.puzzle
{
    public struct Puzzle2D
    {
        public int ID;
        public double Angel;
        public Point coordinate;
        public Image<Bgr,byte> image;
        public string Position;
    };
    public struct Puzzle3D
    {
        public int ID;
        public RealWorldCoordinate coordinate;
        public string Position;
    };
    public struct RealWorldCoordinate
    {

    }

}
