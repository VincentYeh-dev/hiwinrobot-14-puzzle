using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleLibrary.puzzle
{
    public struct Puzzle3D
    {
        public int ID;
        public PointF RealWorldCoordinate;
        public double Angle;
        public Point Position;
        public Puzzle2D puzzle2D;
    };
}
