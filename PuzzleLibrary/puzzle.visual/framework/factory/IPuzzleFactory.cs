using Emgu.CV;
using Emgu.CV.Structure;
using PuzzleLibrary.puzzle.visual.framework;
using PuzzleLibrary.puzzle.visual.framework.positioning;
using System.Collections.Generic;
using System.Drawing;

namespace PuzzleLibrary.puzzle.visual.concrete
{
    public interface PuzzleFactoryListener
    {
        void onLocated(List<LocationResult> results);
        void onRecognized(RecognizeResult result);
    }
    public interface IPuzzleFactory
    {
        List<Puzzle3D> Execute(Image<Bgr, byte> input,IPositioner positioner,int IDOfStart=0);
        List<Puzzle3D> Execute(Image<Bgr, byte> input, Rectangle ROI,IPositioner positioner,int IDOfStart=0);
        List<Puzzle3D> Execute(Image<Bgr, byte> input, List<Puzzle3D> ignoredPuzzles, Rectangle ROI, IPositioner positioner, int IDOfStart);
        void setListener(PuzzleFactoryListener listener);
    }
}
