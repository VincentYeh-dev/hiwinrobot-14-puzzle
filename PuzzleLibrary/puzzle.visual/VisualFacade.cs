using Emgu.CV;
using Emgu.CV.Structure;
using PuzzleLibrary.file;
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

        public static IPuzzleFactory GenerateFactoryFromCSVFile(string filename,PuzzleFactoryListener listener)
        {
            var rows=CSV.Read(filename);
            var scaler=new MCvScalar(double.Parse(rows[0][1]), double.Parse(rows[0][2]), double.Parse(rows[0][3]));
            var thresold = int.Parse(rows[1][1]);
            var uniquenessThreshold = double.Parse(rows[2][1]);
            Size minSize = new Size(int.Parse(rows[3][1]),int.Parse(rows[3][2]));
            Size maxSize = new Size(int.Parse(rows[4][1]),int.Parse(rows[4][2]));
            var filepath = rows[5][1];
            var dilateErodeSize=int.Parse(rows[6][1]);
            return GenerateFactory(scaler,thresold,uniquenessThreshold,minSize,maxSize,new Image<Bgr,byte>(filepath),dilateErodeSize,listener);
        }

    }
}
