using ExclusiveProgram.puzzle.logic.framework;
using PuzzleLibrary.puzzle;
using System;
using System.Collections.Generic;

namespace ExclusiveProgram.puzzle.logic.concrete
{
    public class Strategy1 : IPuzzleStrategy
    {

        private HashSet<string> recombinedPuzzles = new HashSet<string>();
        private List<Puzzle3D> puzzles = new List<Puzzle3D>();
        private Dictionary<string,List<int>> position_index_map=new Dictionary<string, List<int>>();

        private string[] missing_positions=new string[0];
        private StrategyAction action;
        private Puzzle3D? target_puzzle;

        public void Feed(List<Puzzle3D> puzzles)
        {
            if (puzzles == null)
                throw new ArgumentNullException("puzzles == null");
            Reset();
            HashSet<string> recorded_positions = new HashSet<string>();
            for (int i = 0; i < puzzles.Count; i++)
            {
                this.puzzles.Add(puzzles[i]);

                var position = puzzles[i].Position;


                bool exist = position_index_map.TryGetValue(position,out List<int> list);
                if (exist)
                    list.Add(i);
                else
                {
                    var new_list=new List<int>();
                    new_list.Add(i);
                    position_index_map.Add(position, new_list);
                }

                recorded_positions.Add(position);
            }

            missing_positions = GetMissingPosition(recorded_positions);

            Next();
        }
        public void Next()
        {
            //if (missing_positions == null)
            //    throw new NullReferenceException("missing_positions == null");

            //if (missing_positions.Length != 0)
            //{
            //    action = StrategyAction.rescan_missing_puzzle;
            //    return;
            //}

            //foreach (string key in position_index_map.Keys)
            //{
            //    position_index_map.TryGetValue(key, out List<int> list);
            //    if (list.Count > 1)
            //    {
            //        action = StrategyAction.rescan_duplicate_puzzle;
            //        return;
            //    }
            //}

            var position = NextTargetPosition();
            if (position!=null)
            {
                bool success=position_index_map.TryGetValue(position,out List<int> target_indexes);
                if (!success)
                    throw new Exception();
                recombinedPuzzles.Add(position);

                target_puzzle = puzzles[target_indexes[0]];
                action = StrategyAction.recombine_puzzle;
                return;
            }
            action = StrategyAction.do_nothing;
        }

        public void Fix(List<Puzzle3D> new_puzzles)
        {
            List<Puzzle3D> output_puzles= new List<Puzzle3D>();
            for (int i=0;i<new_puzzles.Count;i++)
            {
                Puzzle3D puzzle= new_puzzles[i];

                bool found=position_index_map.TryGetValue(puzzle.Position,out List<int> indexes);
                if (!found)
                {
                    output_puzles.Add(puzzle);
                }
                else if (indexes.Count > 1)
                {
                    Puzzle3D[] duplicate_puzzles=new Puzzle3D[indexes.Count];
                    for (int j = 0; j < indexes.Count; j++)
                    {
                        duplicate_puzzles[j] = puzzles[indexes[j]];
                    }
                    foreach(var duplicate_puzzle in duplicate_puzzles)
                    {
                        puzzles.Remove(duplicate_puzzle);
                    }

                    output_puzles.Add(puzzle);
                }
            }
            output_puzles.AddRange(puzzles);
            Feed(output_puzles);
        }

        public void Reset()
        {
            recombinedPuzzles.Clear();
            puzzles.Clear();
            position_index_map.Clear();
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
            if (CanDoRecombine("00"))
                return "00";
            if (CanDoRecombine("06")) 
                return "06";
            if (CanDoRecombine("40")) 
                return "40";
            if (CanDoRecombine("46")) 
                return "46";

            for(int up_down=0;up_down<=4;up_down+=4)
                for(int col=1; col<=6; col++)
                {
                    if (CanDoRecombine(up_down+""+col)) 
                        return up_down+""+col;
                }

            for(int left_right=0;left_right<=6;left_right+=6)
                for(int row=1; row<=4; row++)
                {
                    if (CanDoRecombine(row+""+left_right)) 
                        return row+""+left_right;
                }


            for(int row=1;row<=3;row++)
                for(int col=1; col<=5; col++)
                {
                    if (CanDoRecombine(row+""+col)) 
                        return row+""+col;
                }

            return null;
        }

        public bool HasThingToDo()
        {
            return action != framework.StrategyAction.do_nothing;
        }

        public Puzzle3D GetTargetPuzzle()
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

        private bool CanDoRecombine(string position)
        {
            return !recombinedPuzzles.Contains(position)&&
                position_index_map.ContainsKey(position);
                
        }
    }
}
