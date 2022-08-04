using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
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
    public class WeightGrayConversionImpl : IGrayConversionImpl
    {
        private readonly double green_weight;
        private readonly double red_weight;
        private readonly double blue_weight;


        public WeightGrayConversionImpl(MCvScalar scalar)
        {
            this.blue_weight = scalar.V0;
            this.green_weight = scalar.V1;
            this.red_weight = scalar.V2;
        }

        public WeightGrayConversionImpl(double blue_weight=0.114f,double green_weight=0.587f,double red_weight=0.299f)
        {
            this.blue_weight = blue_weight;
            this.green_weight = green_weight;
            this.red_weight = red_weight;
        }

        public void ConvertToGray(Image<Bgr, byte> input, Image<Gray, byte> output)
        {

            for (int i = 0; i < input.Rows; i++)
            {
                for (int j = 0; j < input.Cols; j++)
                {
                    var R = input.Data[i,j,2];
                    var G = input.Data[i,j,1];
                    var B = input.Data[i,j,0];
                    var scale = (red_weight * R + green_weight * G + blue_weight * B);
                    if(scale>255)
                        output.Data[i,j,0] = 255;
                    else
                    output.Data[i,j,0] = (byte)scale;
                }
            }
        }
    }
}
