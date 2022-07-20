﻿using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExclusiveProgram.puzzle.visual.framework
{
    

    public struct LocationResult
    {
        public Point Coordinate;
        //public double Angle;
        public Size Size;
        public Image<Bgr,byte> ROI;
        public Image<Gray,byte> BinaryROI;
        public int ID;
    }

    public interface IPuzzleLocator
    {
        List<LocationResult> Locate(Image<Bgr, byte> rawImage);
    }
}
