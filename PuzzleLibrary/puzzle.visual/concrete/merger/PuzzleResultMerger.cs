using Emgu.CV;
using Emgu.CV.Structure;
using PuzzleLibrary.puzzle.visual.framework;
using System.Drawing;

namespace PuzzleLibrary.puzzle.visual.concrete
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
            var size = ROI.Size;
            var point = new Point((int)locationResult.Coordinate.X-size.Width/2,(int)locationResult.Coordinate.Y-size.Height/2);
            puzzle2D.ROI= new Rectangle(point,size);
            puzzle2D.Image = ROI;
            puzzle2D.RotatedRect= locationResult.RotatedRect;

            Puzzle3D puzzle3D = new Puzzle3D(); 
            puzzle3D.ID= locationResult.ID;
            puzzle3D.Angle = recognizeResult.Angle;
            puzzle3D.RealWorldCoordinate =realworldCoordinate;
            puzzle3D.Position = recognizeResult.Position;
            puzzle3D.puzzle2D = puzzle2D;

            return puzzle3D;
        }
    }
}
