using Emgu.CV;
using Emgu.CV.Structure;
using ExclusiveProgram.device;
using ExclusiveProgram.puzzle;
using PuzzleLibrary.puzzle;
using PuzzleLibrary.puzzle.visual.concrete;
using RASDK.Arm;
using RASDK.Basic;
using RASDK.Basic.Message;
using RASDK.Vision.IDS;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ExclusiveProgram.ui.component
{
    public partial class ContestUserControl : UserControl
    {
        public IPuzzleFactory Factory{ get; set; }
        public RoboticArm Arm { get; set; }
        public IDSCamera Camera{ get; set; }

        private readonly List<Puzzle3D> puzzles=new List<Puzzle3D>();
        private PuzzleHandler handler;
        
        public ContestUserControl()
        {
            InitializeComponent();
        }

        private void UpdatePuzzleList()
        {
            listBox_detected_puzzles.Items.Clear();
            foreach(var puzzle in puzzles)
            {
                listBox_detected_puzzles.Items.Add($"P:{puzzle.Position} A:{puzzle.Angle}\nR:({puzzle.RealWorldCoordinate.X},{puzzle.RealWorldCoordinate.Y})");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            puzzles.Clear();
            puzzles.AddRange(handler.MoveToRegionAndGetPuzzles(1));
            UpdatePuzzleList();
        }

        private void listBox_detected_puzzles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (puzzles == null || puzzles.Count == 0||listBox_detected_puzzles.SelectedIndex>=puzzles.Count)
                return;
            var puzzle = puzzles[listBox_detected_puzzles.SelectedIndex];
            pictureBox_puzzle_image.Image = puzzle.puzzle2D.Image.ToBitmap();
            textBox_puzzle_info.Text = $"ID{puzzle.ID}\nAngle:{puzzle.Angle}\n" +
                $"Coordinate:({Math.Round(puzzle.RealWorldCoordinate.X,2)},{Math.Round(puzzle.RealWorldCoordinate.Y,2)})";

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (puzzles == null || puzzles.Count == 0)
                return;
            var puzzle= puzzles[listBox_detected_puzzles.SelectedIndex];

            if (MessageBox.Show("確定移動", "警告", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 
                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                var positions = new double[] { puzzle.RealWorldCoordinate.X, puzzle.RealWorldCoordinate.Y, 10.938, 180, 0, 90 };
                Arm.MoveAbsolute(positions, new AdditionalMotionParameters { CoordinateType = RASDK.Arm.Type.CoordinateType.Descartes, NeedWait = true});
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            puzzles.Clear();
            puzzles.AddRange(handler.MoveToRegionAndGetPuzzles(2));
            UpdatePuzzleList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var sucker = new SuckerDevice();
            sucker.Connect();
            sucker.Disable();

            if(Factory != null&&Arm!=null&&Camera!=null&&sucker!=null)
                handler = new PuzzleHandler(Factory,Arm,Camera,sucker);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            puzzles.Clear();
            puzzles.AddRange(handler.MoveToRegionAndGetPuzzles(1));
            puzzles.AddRange(handler.MoveToRegionAndGetPuzzles(2,puzzles.Count));
            UpdatePuzzleList();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (puzzles == null || puzzles.Count == 0)
                return;
            var puzzle= puzzles[listBox_detected_puzzles.SelectedIndex];

            if (MessageBox.Show("確定移動", "警告", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, 
                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                handler.PickPuzzle(puzzle);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            handler.DropPuzzle();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (puzzles == null || puzzles.Count == 0)
                return;
            var puzzle= puzzles[listBox_detected_puzzles.SelectedIndex];
            handler.PickPuzzle(puzzle);
            handler.PutPuzzle(puzzle);
        }

        private void button9_Click(object sender, EventArgs e)
        {

            handler.AAA();
        }
    }
}
