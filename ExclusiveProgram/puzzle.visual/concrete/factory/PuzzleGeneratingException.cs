using System;
namespace ExclusiveProgram.puzzle.visual.concrete.factory
{
    public class PuzzleGeneratingException : Exception
    {
        public PuzzleGeneratingException(string message) : base(message)
        {
        }

        public PuzzleGeneratingException(string message,Exception exception) : base(message, exception)
        {
        }
        
    }

    public class PuzzleLocatingException:PuzzleGeneratingException
    {
        public PuzzleLocatingException(string message) : base(message) { 
        }
        public PuzzleLocatingException(string message, Exception exception) : base(message, exception)
        {
        }
        
    }

    public class PuzzleRecognizingException : PuzzleGeneratingException
    {
        public PuzzleRecognizingException(string message) : base(message)
        {
        }
        public PuzzleRecognizingException(string message, Exception exception) : base(message, exception)
        {
        }
    }
}
