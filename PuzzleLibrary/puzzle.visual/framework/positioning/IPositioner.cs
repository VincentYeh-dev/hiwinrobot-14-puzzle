using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleLibrary.puzzle.visual.framework.positioning
{
    public interface IPositioner
    {
        double[] ImageToWorldCoordinate(double[] image_coordinate);
        float[] ImageToWorldCoordinate(float[] image_coordinate);
    }
}
