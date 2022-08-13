using Emgu.CV;
using Emgu.CV.Structure;
using PuzzleLibrary.puzzle.visual.framework;
using PuzzleLibrary.threading;
using PuzzleLibrary.puzzle.visual.framework.positioning;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace PuzzleLibrary.puzzle.visual.concrete
{
    public class DefaultPuzzleFactory : IPuzzleFactory
    {
        private IPuzzleRecognizer recognizer;
        private IPuzzleLocator locator;
        private readonly IPuzzleResultMerger merger;
        private PuzzleFactoryListener listener;
        private readonly TaskFactory factory;

        public DefaultPuzzleFactory(IPuzzleLocator locator, IPuzzleRecognizer recognizer, IPuzzleResultMerger merger, int threadCount)
        {
            this.recognizer = recognizer;
            this.locator = locator;
            this.merger = merger;

            // Create a scheduler that uses two threads.
            LimitedConcurrencyLevelTaskScheduler lcts = new LimitedConcurrencyLevelTaskScheduler(threadCount);

            // Create a TaskFactory and pass it our custom scheduler.
            factory = new TaskFactory(lcts);
        }

        public List<Puzzle3D> Execute(Image<Bgr, byte> input,Rectangle ROI,IPositioner positioner,int IDOfStart)
        { 
            if (!recognizer.ModelImagePreprocessIsDone())
                recognizer.PreprocessModelImage();
            List<LocationResult> dataList;
            dataList = locator.Locate(input,ROI,IDOfStart);

            if (listener != null)
                listener.onLocated(dataList);

            List<Puzzle3D> results = new List<Puzzle3D>();
            List<Task> tasks = new List<Task>();
            var cts = new CancellationTokenSource();
            foreach (LocationResult location in dataList)
            {
                Task task = factory.StartNew(() =>
                {
                    try
                    {
                        var recognized_result = recognizer.Recognize(location.ID, location.ROI);
                        if (listener != null)
                            listener.onRecognized(recognized_result);

                        var imageCoordinate = new float[] { location.Coordinate.X, location.Coordinate.Y };
                        float[] worldCoordinate=new float[] {0f,0f};
                        if(positioner != null)
                            worldCoordinate = positioner.ImageToWorldCoordinate(imageCoordinate);

                        results.Add(merger.merge(location, location.ROI, recognized_result,new PointF(worldCoordinate[0],worldCoordinate[1])));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }, cts.Token);
                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
            cts.Dispose();
            return results;
        }

        public List<Puzzle3D> Execute(Image<Bgr, byte> input, IPositioner positioner, int IDOfStart = 0)
        {
            return Execute(input, Rectangle.Empty ,positioner, IDOfStart);
        }

        public void setListener(PuzzleFactoryListener listener)
        {
            this.listener = listener;
        }
    }

}
