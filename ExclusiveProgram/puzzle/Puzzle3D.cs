using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExclusiveProgram.puzzle
{
    public struct Puzzle3D
    {
        public int ID;
        public PointF RealWorldCoordinate;
        public double Angle;
        public string Position;
        public Puzzle2D puzzle2D;
    };
}
