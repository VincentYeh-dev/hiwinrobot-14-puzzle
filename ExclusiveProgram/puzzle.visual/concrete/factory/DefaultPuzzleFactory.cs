using Emgu.CV;
using Emgu.CV.Structure;
using ExclusiveProgram.puzzle.visual.concrete.factory;
using ExclusiveProgram.puzzle.visual.framework;
using ExclusiveProgram.puzzle.visual.framework.utils;
using ExclusiveProgram.threading;
using RASDK.Vision.Positioning;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace ExclusiveProgram.puzzle.visual.concrete
{
    public class DefaultPuzzleFactory : IPuzzleFactory
    {
        private IPuzzleRecognizer recognizer;
        private IPuzzleLocator locator;
        private readonly IPuzzleResultMerger merger;
        private IVisionPositioning positioning;
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

        public List<Puzzle3D> Execute(Image<Bgr, byte> input,Rectangle ROI)
        { 
            if (!recognizer.ModelImagePreprocessIsDone())
                recognizer.PreprocessModelImage();
            List<LocationResult> dataList;
            dataList = locator.Locate(input,ROI);

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
                        var realWorldCoordinate=positioning!=null?positioning.ImageToWorld(location.Coordinate):new PointF();
                        results.Add(merger.merge(location, location.ROI, recognized_result,realWorldCoordinate));
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

        public void setVisionPositioning(IVisionPositioning positioning)
        {
            this.positioning = positioning;
        }

        public void setListener(PuzzleFactoryListener listener)
        {
            this.listener = listener;
        }
    }

}
