using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
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
    public class CLANEPreprocessImpl : IPreprocessImpl
    {
        private readonly double clipLimit;
        private readonly Size tileGridSize;

        public CLANEPreprocessImpl(double clipLimit, Size tileGridSize)
        {
            this.clipLimit = clipLimit;
            this.tileGridSize = tileGridSize;
        }

        public void Preprocess(Image<Bgr, byte> input, Image<Bgr, byte> output)
        {
            var channels = new VectorOfMat();
            CvInvoke.Split(input, channels);
            CvInvoke.CLAHE(channels[0],clipLimit,tileGridSize,channels[0]);
            CvInvoke.CLAHE(channels[1],clipLimit,tileGridSize,channels[1]);
            CvInvoke.CLAHE(channels[2],clipLimit,tileGridSize,channels[2]);
            CvInvoke.Merge(channels, output);
        }
    }
}
