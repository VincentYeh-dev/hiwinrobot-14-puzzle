using ExclusiveProgram.puzzle.visual.framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExclusiveProgram.puzzle.logic.framework
{
    public enum StrategyAction
    {
        //從新尋找拼圖
        rescan_missing_puzzle,
        //從新尋找未重複的拼圖
        rescan_duplicate_puzzle,
        //將拼圖拼回拼圖板上
        recombine_puzzle,
        //不進行任何動作
        do_nothing
    }
    public interface IPuzzleStrategy
    {
        void Feed(List<Puzzle3D> puzzles);
        void Next();
        void Reset();
        bool HasThingToDo();
        Puzzle3D GetTargetPuzzle();
        string[] GetMissingPosition();
        StrategyAction GetStrategyAction();

        void Fix(List<Puzzle3D> new_puzzles);
    }
}
