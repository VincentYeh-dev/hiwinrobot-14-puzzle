using PuzzleLibrary.puzzle.visual.framework.positioning;
using RASDK.Vision.Positioning;

namespace ExclusiveProgram.puzzle
{
    public class RASDKPositionerAdaptor : IPositioner
    {
        private readonly IVisionPositioning positioning;

        public RASDKPositionerAdaptor(IVisionPositioning positioning)
        {
            this.positioning = positioning;
        }

        public double[] ImageToWorldCoordinate(double[] image_coordinate)
        {
            positioning.ImageToWorld(image_coordinate[0],image_coordinate[1],out double X,out double Y);
            return new double[] {X,Y};
        }

        public float[] ImageToWorldCoordinate(float[] image_coordinate)
        {
            positioning.ImageToWorld(image_coordinate[0],image_coordinate[1],out double X,out double Y);
            return new float[] {(float)X,(float)Y};
        }
    }
}
