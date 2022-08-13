using Emgu.CV;
using Emgu.CV.Structure;
using PuzzleLibrary.puzzle;
using PuzzleLibrary.puzzle.visual.concrete;
using PuzzleLibrary.puzzle.visual.concrete.utils;
using PuzzleLibrary.puzzle.visual.framework.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleLibrary.puzzle.visual
{
    public class VisualFacade
    {
        private VisualFacade()
        {

        }

        public static IPuzzleFactory GenerateFactory(MCvScalar scalar,int threshold,double uniquenessThreshold,Size minSize,Size maxSize,Image<Bgr,byte> modelImage,int dilateErodeSize, PuzzleFactoryListener listener)
        {
            //var preprocessImpl = new CLANEPreprocessImpl(3,new Size(8,8));
            IPreprocessImpl preprocessImpl=null;
            var grayConversionImpl = new WeightGrayConversionImpl(scalar);
            var thresoldImpl = new NormalThresoldImpl(threshold);
            var binaryPreprocessImpl = new DilateErodeBinaryPreprocessImpl(new Size(dilateErodeSize,dilateErodeSize));
            var locator = new PuzzleLocator(minSize, maxSize, null, grayConversionImpl, thresoldImpl, binaryPreprocessImpl, 0.01);

            var recognizer = new PuzzleRecognizer(modelImage, uniquenessThreshold, new SiftFlannPuzzleRecognizerImpl(), preprocessImpl, grayConversionImpl, thresoldImpl,binaryPreprocessImpl);
            //recognizer.setListener(new MyRecognizeListener(this));

            var factory = new DefaultPuzzleFactory(locator, recognizer, new PuzzleResultMerger(), 8);
            factory.setListener(listener);
            return factory;
        }

    }
}
