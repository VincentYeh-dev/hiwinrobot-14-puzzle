using Emgu.CV;
using Emgu.CV.Structure;
using ExclusiveProgram.puzzle.visual.concrete.utils;
using System;
using System.Windows.Forms;

namespace ExclusiveProgram
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm.MainForm(new Control()));
        }

    }
}
