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
                Assert.AreEqual(StrategyAction.recombine_puzzle, strategy.GetStrategyAction());
                strategy.Next();
            }
            Assert.AreEqual(StrategyAction.do_nothing, strategy.GetStrategyAction());
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
            Assert.AreEqual(StrategyAction.rescan_missing_puzzle,strategy.GetStrategyAction());
        }


        [TestMethod]
        public void DuplicateTest()
        {
            var strategy = new Strategy1();
            var puzzles = GeneratePuzzles();
            puzzles.Add(GeneratePuzzle(0, 0));

            strategy.Feed(puzzles);
            Assert.AreEqual(StrategyAction.rescan_duplicate_puzzle,strategy.GetStrategyAction());
        }

        [TestMethod]
        public void DuplicateAddTest()
        {
            var strategy = new Strategy1();
            var puzzles = GeneratePuzzles();
            var duplicate_puzzle = GeneratePuzzle(0, 0);
            puzzles.Add(duplicate_puzzle);

            strategy.Feed(puzzles);
            Assert.AreEqual(StrategyAction.rescan_duplicate_puzzle,strategy.GetStrategyAction());

            var new_rescan_puzzles = GeneratePuzzles().GetRange(0,5);
            strategy.ReplaceOnlyDuplicatePosition(new_rescan_puzzles);
            Assert.AreEqual(StrategyAction.recombine_puzzle,strategy.GetStrategyAction());
            

        }
        
        private static List<Puzzle2D> GeneratePuzzles()
        {
            List<Puzzle2D> puzzles = new List<Puzzle2D>();
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    puzzles.Add(GeneratePuzzle(i,j));
                }
            }
            return puzzles;
        }
        private static Puzzle2D GeneratePuzzle(int row,int col)
        {
            var puzzle=new Puzzle2D();
            puzzle.Position = String.Format("{0}{1}",row,col);
            return puzzle;
        }
    }
}