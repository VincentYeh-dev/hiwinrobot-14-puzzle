using ExclusiveProgram.puzzle.logic.framework;
using ExclusiveProgram.puzzle.visual.framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExclusiveProgram.puzzle.logic.concrete
{
    public class Strategy1 : IPuzzleStrategy
    {

        private HashSet<string> recombinedPuzzles = new HashSet<string>();
        private List<Puzzle_sturct> puzzles = new List<Puzzle_sturct>();
        private Dictionary<string,int> puzzles_directory=new Dictionary<string, int>();


        public void Feed(List<Puzzle_sturct> puzzles)
        {
            for (int i = 0; i < puzzles.Count; i++)
            {
                this.puzzles.Add(puzzles[i]);
                puzzles_directory.Add(puzzles[i].position, i);
            }

        }

        public void Reset()
        {
            recombinedPuzzles.Clear();
            puzzles.Clear();
            puzzles_directory.Clear();
        }

        public framework.Action KnowWhatToDo(out Puzzle_sturct? target)
        {
            var position = NextTargetPosition();
            if (position.Equals("NN"))
            {
                target = null;
                return framework.Action.do_nothing;
            }
            else
            {
                bool success=puzzles_directory.TryGetValue(position,out int target_index);
                if (!success)
                    throw new Exception();
                target = puzzles[target_index];
                recombinedPuzzles.Add(position);
                return framework.Action.recombine;
            }



        }


        private int[] GetDuplicatePosition(List<Puzzle_sturct> puzzles)
        {
            List<int> duplocateList = new List<int>();
            HashSet<string> records = new HashSet<string>();

            for (int i = 0; i < puzzles.Count; i++)
            {
                var position = puzzles[i].position;

                if (records.Contains(position))
                    duplocateList.Add(i);

                records.Add(position);
            }

            return duplocateList.ToArray();
        }

        private string[] GetMissingPosition(List<Puzzle_sturct> puzzles)
        {
            List<string> missingList = new List<string>();
            HashSet<string> records = new HashSet<string>();

            for (int i = 0; i < puzzles.Count; i++)
            {
                records.Add(puzzles[i].position);
            }
            for (int row = 0; row <= 4; row++)
                for (int col = 0; col <= 6; col++)
                {
                    var position = String.Format("{0}{1}", row, col);
                    if (!records.Contains(position))
                        missingList.Add(position);
                }

            return missingList.ToArray();
        }
        private string NextTargetPosition()
        {
            if (!(recombinedPuzzles.Contains("00")))
                return "00";
            if (!(recombinedPuzzles.Contains("06"))) 
                return "06";
            if (!(recombinedPuzzles.Contains("40"))) 
                return "40";
            if (!(recombinedPuzzles.Contains("46"))) 
                return "46";

            for(int up_down=0;up_down<=4;up_down+=4)
                for(int col=1; col<=6; col++)
                {
                    if (!(recombinedPuzzles.Contains(up_down+""+col))) 
                        return up_down+""+col;
                }

            for(int left_right=0;left_right<=6;left_right+=6)
                for(int row=1; row<=4; row++)
                {
                    if (!(recombinedPuzzles.Contains(row+""+left_right))) 
                        return row+""+left_right;
                }


            for(int row=1;row<=3;row++)
                for(int col=1; col<=5; col++)
                {
                    if (!(recombinedPuzzles.Contains(row+""+col))) 
                        return row+""+col;
                }

            return "NN";
        }
    }
}
