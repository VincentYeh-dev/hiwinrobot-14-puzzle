using ExclusiveProgram.puzzle.logic.concrete;
using ExclusiveProgram.puzzle.logic.framework;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PuzzleLibrary.puzzle;
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
            //Assert.ThrowsException<InvalidOperationException>(()=>{ strategy.GetMissingPosition(); });
            Assert.ThrowsException<InvalidOperationException>(()=>{ strategy.GetTargetPuzzle(); });
        }

        [TestMethod]
        public void EmptyTest()
        {
            var strategy = new Strategy1();
            var puzzles = GeneratePuzzles();
            strategy.Feed(new List<Puzzle3D>());
            Assert.AreEqual(StrategyAction.do_nothing,strategy.GetStrategyAction());
        }


        [TestMethod]
        public void DuplicateTest()
        {
            var strategy = new Strategy1();
            var puzzles = GeneratePuzzles();
            puzzles.Add(GeneratePuzzle(0, 0));

            strategy.Feed(puzzles);
            Assert.AreEqual(StrategyAction.recombine_puzzle,strategy.GetStrategyAction());
        }

        [TestMethod]
        public void MissingTest()
        {
            var strategy = new Strategy1();
            var puzzles = GeneratePuzzles();
            puzzles.RemoveAt(0);
            strategy.Feed(puzzles);
            Assert.AreEqual(StrategyAction.recombine_puzzle,strategy.GetStrategyAction());
        }

        [TestMethod]
        public void MissingFixTest()
        {
            var strategy = new Strategy1();
            var puzzles = GeneratePuzzles();
            var missing_puzzle=puzzles[1];
            puzzles.Remove(missing_puzzle);

            strategy.Feed(puzzles);
            Assert.AreEqual(StrategyAction.recombine_puzzle,strategy.GetStrategyAction());
            var list=new List<Puzzle3D>();
            list.Add(missing_puzzle);
            list.Add(GeneratePuzzle(0,0));
            strategy.Fix(list);
            Assert.AreEqual(StrategyAction.recombine_puzzle,strategy.GetStrategyAction()); 
        }

        [TestMethod]
        public void DuplicateFixTest()
        {
            var strategy = new Strategy1();
            var puzzles = GeneratePuzzles();
            var duplicate_puzzle = GeneratePuzzle(0, 0);
            puzzles.Add(duplicate_puzzle);

            strategy.Feed(puzzles);
            Assert.AreEqual(StrategyAction.recombine_puzzle,strategy.GetStrategyAction());

            var new_rescan_puzzles = GeneratePuzzles().GetRange(0,5);
            strategy.Fix(new_rescan_puzzles);
            Assert.AreEqual(StrategyAction.recombine_puzzle,strategy.GetStrategyAction());

        }
        [TestMethod]
        public void DuplicateMissingFixMethodTest()
        {
            var strategy = new Strategy1();
            var puzzles = GeneratePuzzles();
            var duplicate_puzzle = puzzles[0];
            var missing_puzzle = puzzles[2];
            puzzles.Add(duplicate_puzzle);
            puzzles.Remove(missing_puzzle);
            strategy.Feed(puzzles);

            var list = new List<Puzzle3D>();
            list.Add(missing_puzzle);
            list.Add(duplicate_puzzle);

            Assert.AreEqual(StrategyAction.recombine_puzzle, strategy.GetStrategyAction());
            strategy.Fix(list);
            Assert.AreEqual(StrategyAction.recombine_puzzle, strategy.GetStrategyAction());

        }


        
        private static List<Puzzle3D> GeneratePuzzles()
        {
            List<Puzzle3D> puzzles = new List<Puzzle3D>();
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < 7; j++)
                {
                    puzzles.Add(GeneratePuzzle(i,j));
                }
            }
            return puzzles;
        }
        private static Puzzle3D GeneratePuzzle(int row,int col)
        {
            var puzzle=new Puzzle3D();
            puzzle.Position = new System.Drawing.Point(col,row);
            return puzzle;
        }
    }
}