using ExclusiveProgram.puzzle.logic.framework;
using ExclusiveProgram.puzzle.visual.framework;
using System;
using System.Collections.Generic;

namespace ExclusiveProgram.puzzle.logic.concrete
{
    public class Strategy1 : IPuzzleStrategy
    {

        private HashSet<string> recombinedPuzzles = new HashSet<string>();
        private List<Puzzle2D> puzzles = new List<Puzzle2D>();
        private Dictionary<string,List<int>> puzzles_directory=new Dictionary<string, List<int>>();

        private string[] missing_positions;
        private framework.StrategyAction action;
        private Puzzle2D? target_puzzle;

        public void Feed(List<Puzzle2D> puzzles)
        {
            if (puzzles == null)
                throw new ArgumentNullException("puzzles == null");
            Reset();
            HashSet<string> recorded_positions = new HashSet<string>();
            for (int i = 0; i < puzzles.Count; i++)
            {
                this.puzzles.Add(puzzles[i]);

                var position = puzzles[i].Position;


                bool exist = puzzles_directory.TryGetValue(position,out List<int> list);
                if (exist)
                    list.Add(i);
                else
                {
                    var new_list=new List<int>();
                    new_list.Add(i);
                    puzzles_directory.Add(position, new_list);
                }

                recorded_positions.Add(position);
            }

            missing_positions = GetMissingPosition(recorded_positions);

            Next();
        }
        public void Next() { 

            if (missing_positions!= null&&missing_positions.Length!=0)
            {
                action = framework.StrategyAction.rescan_missing_puzzle;
                return;
            }


            var position = NextTargetPosition();
            if (position!=null)
            {
                bool success=puzzles_directory.TryGetValue(position,out List<int> target_indexes);
                if (!success)
                    throw new Exception();
                recombinedPuzzles.Add(position);

                target_puzzle = puzzles[target_indexes[0]];
                action = framework.StrategyAction.recombine_puzzle;
                return;
            }
            action = framework.StrategyAction.do_nothing;
        }
        public void Append(Puzzle2D puzzle)
        {
            List<Puzzle2D> new_puzzles = new List<Puzzle2D>();
            new_puzzles.AddRange(this.puzzles);
            new_puzzles.Add(puzzle);
            Feed(new_puzzles);
        }
        public void AddOnlyMissingPosition(List<Puzzle2D> new_puzzles)
        {
            List<Puzzle2D> output_puzles= new List<Puzzle2D>();
            output_puzles.AddRange(this.puzzles);
            foreach (Puzzle2D puzzle in new_puzzles)
            {
                foreach(var position in missing_positions)
                {
                    if (puzzle.Position.Equals(position))
                    {
                        output_puzles.Add(puzzle);
                    }
                }
            }
            Feed(output_puzles);
        }


        public void Reset()
        {
            recombinedPuzzles.Clear();
            puzzles.Clear();
            puzzles_directory.Clear();
            missing_positions=new string[0];
            action = framework.StrategyAction.do_nothing;
            target_puzzle = null;
        }

        private string[] GetMissingPosition(HashSet<string> records)
        {
            List<string> missingList = new List<string>();

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

            return null;
        }

        public bool HasThingToDo()
        {
            return action != framework.StrategyAction.do_nothing;
        }

        public Puzzle2D GetTargetPuzzle()
        {
            if (action!=framework.StrategyAction.recombine_puzzle)
                throw new InvalidOperationException("action!=framework.Action.recombine");

            return target_puzzle.Value;
        }

        public String[] GetMissingPosition()
        {
            if (action!=framework.StrategyAction.rescan_missing_puzzle)
                throw new InvalidOperationException("action!=rescan_target");

            return missing_positions;
        }
        public framework.StrategyAction GetStrategyAction()
        {
            return action;
        }
    }
}
