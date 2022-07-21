using ExclusiveProgram.puzzle.visual.framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExclusiveProgram.puzzle.logic.framework
{
    public enum Action
    {
        rescan_all,rescan_target,recombine,do_nothing
    }
    public interface IPuzzleStrategy
    {
        void Feed(List<Puzzle2D> puzzles);
        void Reset();
        bool HasThingToDo();
        Puzzle2D GetTargetPuzzle();
        string[] GetMissingPosition();
        void Next();
        Action GetAction();
    }
}
