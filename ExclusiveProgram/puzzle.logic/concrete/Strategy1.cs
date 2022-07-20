﻿using ExclusiveProgram.puzzle.logic.framework;
using ExclusiveProgram.puzzle.visual.framework;
using System;
using System.Collections.Generic;

namespace ExclusiveProgram.puzzle.logic.concrete
{
    public class Strategy1 : IPuzzleStrategy
    {

        private HashSet<string> recombinedPuzzles = new HashSet<string>();
        private List<Puzzle_sturct> puzzles = new List<Puzzle_sturct>();
        private Dictionary<string,List<int>> puzzles_directory=new Dictionary<string, List<int>>();
        private string[] missing_positions;

        public void Feed(List<Puzzle_sturct> puzzles)
        {
            HashSet<string> recorded_positions = new HashSet<string>();
            for (int i = 0; i < puzzles.Count; i++)
            {
                this.puzzles.Add(puzzles[i]);

                var position = puzzles[i].position;


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
            if (position!=null)
            {
                bool success=puzzles_directory.TryGetValue(position,out List<int> target_indexes);
                if (!success)
                    throw new Exception();
                target = puzzles[target_indexes[0]];
                recombinedPuzzles.Add(position);
                return framework.Action.recombine;
            }

            target = null;
            return framework.Action.do_nothing;
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
    }
}
