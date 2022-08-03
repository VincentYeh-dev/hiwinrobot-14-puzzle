using Emgu.CV;
using Emgu.CV.Structure;
using ExclusiveProgram.puzzle.visual.framework;
using System.Drawing;

namespace ExclusiveProgram.puzzle.visual.concrete
{
    public class PuzzleResultMerger : IPuzzleResultMerger
    {
        public PuzzleResultMerger()
        {
        }

        public Puzzle3D merge(LocationResult locationResult, Image<Bgr, byte> ROI, RecognizeResult recognizeResult,PointF realworldCoordinate)
        {
            Puzzle2D puzzle2D = new Puzzle2D();
            puzzle2D.Coordinate = locationResult.Coordinate;
            puzzle2D.ROI = ROI;

            Puzzle3D puzzle3D = new Puzzle3D(); 
            puzzle3D.ID= locationResult.ID;
            puzzle3D.Angel = recognizeResult.Angle;
            puzzle3D.RealWorldCoordinate =realworldCoordinate;
            puzzle3D.Position = recognizeResult.position;
            puzzle3D.puzzle2D = puzzle2D;

            return puzzle3D;
        }
    }
}
