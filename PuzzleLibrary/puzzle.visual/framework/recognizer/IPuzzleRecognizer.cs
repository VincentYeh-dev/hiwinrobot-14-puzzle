using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Collections.Generic;
using System.Drawing;

namespace PuzzleLibrary.puzzle.visual.framework
{
    public struct RecognizeResult
    {
        public double Angle;
        public string position;
        public int id;
    };

    public interface PuzzleRecognizerImpl
    {
        void DetectFeatures(Mat image, object p, VectorOfKeyPoint keyPoints, Mat descriptors, bool v);
        void MatchFeatures(Mat modelDescriptors, Mat observedDescriptors, VectorOfKeyPoint modelKeyPoints, VectorOfKeyPoint observedKeyPoints, VectorOfVectorOfDMatch matches);
    }

    public interface IPuzzleRecognizer
    {
        bool ModelImagePreprocessIsDone();
        void PreprocessModelImage();
        RecognizeResult Recognize(int id,Image<Bgr,byte> image);
    }

}
