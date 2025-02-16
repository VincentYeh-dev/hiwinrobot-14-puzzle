﻿using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using PuzzleLibrary.puzzle.visual.framework;
using PuzzleLibrary.puzzle.visual.framework.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleLibrary.puzzle.visual.concrete.utils
{
    public class NormalPreprocessImpl : IPreprocessImpl
    {
        public void Preprocess(Image<Bgr, byte> input, Image<Bgr, byte> output)
        {
            VisualSystem.WhiteBalance(input,output);
            VisualSystem.ExtendColor(output,output);
        }
    }
}
