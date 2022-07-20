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
        rescaon_all,rescan_target,recombine,do_nothing
    }
    public interface IPuzzleStrategy
    {
        void Feed(List<Puzzle_sturct> puzzles);
        void Reset();
        Action KnowWhatToDo(out Puzzle_sturct? target);
    }
}
