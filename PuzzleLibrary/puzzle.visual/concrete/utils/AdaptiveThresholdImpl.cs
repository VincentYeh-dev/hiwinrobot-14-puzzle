using Emgu.CV;
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
    public class AdaptiveThresholdImpl : IThresholdImpl
    {
        private readonly double param1;
        private readonly int blockSize;
        public AdaptiveThresholdImpl(int blockSize,double param1)
        {
            if (blockSize % 2 == 0)
                throw new ArgumentException("blockSize % 2 == 0");
            this.param1 = param1;
            this.blockSize = blockSize;
        }
        public void Threshold(Image<Gray, byte> input, Image<Gray, byte> output)
        {
            CvInvoke.AdaptiveThreshold(input,output, 255,AdaptiveThresholdType.MeanC, ThresholdType.Binary,blockSize,param1);
        }
    }
}
