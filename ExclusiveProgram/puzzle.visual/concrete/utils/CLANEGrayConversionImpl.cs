using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using ExclusiveProgram.puzzle.visual.framework;
using ExclusiveProgram.puzzle.visual.framework.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExclusiveProgram.puzzle.visual.concrete.utils
{
    public class CLANEGrayConversionImpl : IGrayConversionImpl
    {
        private readonly double clipLimit;
        private readonly Size tileGridSize;
        private readonly IGrayConversionImpl impl;

        public CLANEGrayConversionImpl(double clipLimit, Size tileGridSize,IGrayConversionImpl impl)
        {
            this.clipLimit = clipLimit;
            this.tileGridSize = tileGridSize;
            this.impl = impl;
        }

        public void ConvertToGray(Image<Bgr, byte> input, Image<Gray, byte> output)
        {
            impl.ConvertToGray(input, output);  
            CvInvoke.CLAHE(output,clipLimit,tileGridSize,output);
        }
    }
}
