﻿using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleLibrary.puzzle.visual.framework
{
    public interface IPuzzleResultMerger
    {
        Puzzle3D merge(LocationResult locationResult,Image<Bgr,byte> correctedImage,RecognizeResult recognizeResult,PointF realworldCoordinate);
    }
}
