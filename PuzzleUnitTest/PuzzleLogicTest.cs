using ExclusiveProgram.puzzle;
using ExclusiveProgram.puzzle.logic.concrete;
using ExclusiveProgram.puzzle.logic.framework;
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
            var puzzles = GeneratePuzzles();
            strategy.Feed(puzzles);

            while (strategy.HasThingToDo())
            {
                var action=strategy.GetStrategyAction();
                if (action == StrategyAction.rescan_missing_puzzle)
                {
                    Console.Write("Rescan: ");
                    foreach (var position in strategy.GetMissingPosition())
                    {
                        Console.Write(position + " ");
                    }
                    Console.Write("\n");
                }
                else if (action == StrategyAction.recombine_puzzle)
                {
                    Console.Write("Recombine ");
                    Console.Write(strategy.GetTargetPuzzle().Position);
                    Console.Write("\n");
                }
                strategy.Next();
            }
            Console.Write(strategy.HasThingToDo());
        }

        [TestMethod]
        public void ResetTest()
        {
            var strategy = new Strategy1();
            var puzzles = GeneratePuzzles();
            strategy.Feed(puzzles);
            strategy.Reset();
            Assert.IsTrue(strategy.GetStrategyAction()==StrategyAction.do_nothing);
            Assert.ThrowsException<InvalidOperationException>(()=>{ strategy.GetMissingPosition(); });
            Assert.ThrowsException<InvalidOperationException>(()=>{ strategy.GetTargetPuzzle(); });
        }

        [TestMethod]
        public void EmptyTest()
        {
            var strategy = new Strategy1();
            var puzzles = GeneratePuzzles();
            strategy.Feed(new List<Puzzle2D>());
            Assert.AreEqual(strategy.GetStrategyAction(),StrategyAction.rescan_missing_puzzle);
        }
        
        private static List<Puzzle2D> GeneratePuzzles()
        {
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
            return puzzles;
        }
    }
}