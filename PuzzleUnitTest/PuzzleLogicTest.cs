using ExclusiveProgram.puzzle;
using ExclusiveProgram.puzzle.logic.concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace PuzzleUnitTest.puzzle.logic
{
    [TestClass]
    public class PuzzleLogicTest
    {
        [TestMethod]
        public void IdealTest()
        {
            var strategy = new Strategy1();
            List<Puzzle2D> puzzles = new List<Puzzle2D>();
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    var puzzle=new Puzzle2D();
                    puzzle.Position = String.Format("{0}{1}",i,j);
                    puzzles.Add(puzzle);
                }
            }

            strategy.Feed(puzzles);

            while (strategy.HasThingToDo())
            {
                var action=strategy.GetAction();
                if (action == ExclusiveProgram.puzzle.logic.framework.Action.rescan_target)
                {
                    Console.Write("Rescan: ");
                    foreach (var position in strategy.GetMissingPosition())
                    {
                        Console.Write(position + " ");
                    }
                    Console.Write("\n");
                }
                else if (action == ExclusiveProgram.puzzle.logic.framework.Action.recombine)
                {
                    Console.Write("Recombine ");
                    Console.Write(strategy.GetTargetPuzzle().Position);
                    Console.Write("\n");
                }
                strategy.Next();
            }
            Console.Write(strategy.HasThingToDo());
        }
    }
}