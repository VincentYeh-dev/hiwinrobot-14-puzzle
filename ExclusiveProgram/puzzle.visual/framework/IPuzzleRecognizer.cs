﻿using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Collections.Generic;
using System.Drawing;

namespace ExclusiveProgram.puzzle.visual.framework
{
    public struct CorrectedPuzzleArgument
    {
        public Point Coordinate;
        public double Angle;
        public Size Size;
        public Image<Bgr,byte> image;
    }

    public struct RecognizeResult
    {
        public double Angle;
        public PointF[] pts;
        public string position;
    };

    public struct Puzzle_sturct
    {
        public double Angel;
        public Point coordinate;
        public Image<Bgr,byte> image;
        public string position;
    };

    public interface PuzzleRecognizerListener
    {
        void OnMatched(Image<Bgr, byte> modelImage, VectorOfKeyPoint modelKeyPoints, Image<Bgr, byte> observedImage, VectorOfKeyPoint observedKeyPoints, VectorOfVectorOfDMatch matches, Mat mask, long matchTime, double slope);
    }

    public interface PuzzleRecognizerImpl
    {
        void DetectFeatures(Mat image, object p, VectorOfKeyPoint keyPoints, Mat descriptors, bool v);
        void MatchFeatures(Mat modelDescriptors, Mat observedDescriptors, VectorOfKeyPoint modelKeyPoints, VectorOfKeyPoint observedKeyPoints, VectorOfVectorOfDMatch matches);
    }

    public interface IPuzzleRecognizer
    {
        RecognizeResult Recognize(Image<Bgr,byte> image);
        void setListener(PuzzleRecognizerListener listener);
    }

}
