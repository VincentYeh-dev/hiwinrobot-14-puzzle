using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using ExclusiveProgram.puzzle.visual.framework.utils;

namespace ExclusiveProgram.puzzle.visual.concrete.utils
{
    public class NormalThresoldImpl: IThresholdImpl
    {
        private readonly int threshold;

        public NormalThresoldImpl(int threshold)
        {
            this.threshold = threshold;
        }

        public void Threshold(Image<Gray, byte> input, Image<Gray, byte> output)
        {
            CvInvoke.Threshold(input,output,threshold, 255, ThresholdType.Binary);
        }
    }
}
