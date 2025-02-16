﻿using ExclusiveProgram.puzzle.logic.framework;
using PuzzleLibrary.puzzle;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace ExclusiveProgram.puzzle.logic.concrete
{
    public class Strategy1 : IPuzzleStrategy
    {

        private HashSet<Point> recombinedPuzzles = new HashSet<Point>();
        private List<Puzzle3D> puzzles = new List<Puzzle3D>();
        private Dictionary<Point,List<int>> position_index_map=new Dictionary<Point, List<int>>();

        private StrategyAction action;
        private Puzzle3D? target_puzzle;

        public void Feed(List<Puzzle3D> puzzles)
        {
            if (puzzles == null)
                throw new ArgumentNullException("puzzles == null");
            Reset();
            HashSet<Point> recorded_positions = new HashSet<Point>();
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

            //missing_positions = GetMissingPosition(recorded_positions);

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
            if (position.HasValue)
            {
                bool success=position_index_map.TryGetValue(position.Value,out List<int> target_indexes);
                if (!success)
                    throw new Exception();
                recombinedPuzzles.Add(position.Value);

                target_puzzle = puzzles[target_indexes[0]];
                action = StrategyAction.recombine_puzzle;
                return;
            }
            action = StrategyAction.do_nothing;
        }

        public void Reset()
        {
            recombinedPuzzles.Clear();
            puzzles.Clear();
            position_index_map.Clear();
            action = framework.StrategyAction.do_nothing;
            target_puzzle = null;
        }
        private Point CreatePoint(int x,int y)
        {
            return new Point(x,y);
        }
        private Point? NextTargetPosition()
        {
            var corner_points = new Point[] {
                CreatePoint(0,0),
                CreatePoint(6,0),
                CreatePoint(0,4),
                CreatePoint(6,4)
            };
            foreach(var corner in corner_points)
            {
                if (CanDoRecombine(corner))
                    return corner;
            }

            for(int up_down=0;up_down<=4;up_down+=4)
                for(int col=1; col<=6; col++)
                {
                    var p = CreatePoint(col, up_down);
                    if (CanDoRecombine(p)) 
                        return p;
                }

            for(int left_right=0;left_right<=6;left_right+=6)
                for(int row=1; row<=4; row++)
                {
                    var p = CreatePoint(left_right, row);
                    if (CanDoRecombine(p)) 
                        return p;
                }


            for(int row=1;row<=3;row++)
                for(int col=1; col<=5; col++)
                {
                    var p = CreatePoint(col, row);
                    if (CanDoRecombine(p)) 
                        return p;
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

        //public String[] GetMissingPosition()
        //{
        //    if (action!=framework.StrategyAction.rescan_missing_puzzle)
        //        throw new InvalidOperationException("action!=rescan_target");

        //    return missing_positions;
        //}
        public framework.StrategyAction GetStrategyAction()
        {
            return action;
        }

        private bool CanDoRecombine(Point position)
        {
            return !recombinedPuzzles.Contains(position)&&
                position_index_map.ContainsKey(position);
                
        }
    }
}
