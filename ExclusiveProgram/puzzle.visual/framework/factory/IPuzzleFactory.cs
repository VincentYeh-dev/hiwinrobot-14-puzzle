using Emgu.CV;
using Emgu.CV.Structure;
using ExclusiveProgram.puzzle.visual.framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExclusiveProgram.puzzle.visual.concrete
{
    public interface PuzzleFactoryListener
    {
        void onLocated(List<LocationResult> results);
        void onRecognized(RecognizeResult result);
    }
    public interface IPuzzleFactory
    {
        List<Puzzle3D> Execute(Image<Bgr, byte> input, Rectangle ROI);
        void setListener(PuzzleFactoryListener listener);
    }
}
