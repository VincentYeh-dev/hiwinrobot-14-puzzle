using Emgu.CV;
using Emgu.CV.Structure;
using ExclusiveProgram.device;
using ExclusiveProgram.puzzle;
using ExclusiveProgram.puzzle.visual.concrete;
using RASDK.Arm;
using RASDK.Basic;
using RASDK.Basic.Message;
using RASDK.Vision.IDS;
using RASDK.Vision.Positioning;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExclusiveProgram.ui.component
{
    struct Region{
        public Rectangle ROI;
        public double[] capture_position;
        public string positioning_filepath;
    }

    public partial class ContestUserControl : UserControl
    {
        private static Region GetRegion(int index)
        {
            if (index == 1)
            {
                Region region1 = new Region();
                region1.ROI= Rectangle.FromLTRB(1068, 30, 2440, 1999);
                region1.capture_position= new double[] { 195.351, 368.003, 230.336, 180, 0, 90 };
                region1.positioning_filepath = "positioning\\region1_ccia.csv";
                return region1;
            }else if (index == 2)
            {

                Region region2 = new Region();
                region2.ROI= Rectangle.FromLTRB(683,3,2362,2027);
                region2.capture_position= new double[] { -195.351, 368.003, 230.336, 180, 0, 90 };
                region2.positioning_filepath = "positioning\\region2_ccia.csv";
                return region2;
            }
            throw new Exception();
        }
            
        private IPuzzleFactory factory;
        private RoboticArm arm;
        private IDSCamera camera;
        private SuckerDevice sucker;
        private readonly List<Puzzle3D> puzzles=new List<Puzzle3D>();

        public ContestUserControl()
        {
            InitializeComponent();

        }

        public void SetFactory(IPuzzleFactory factory)
        {
            this.factory = factory;
        }
        public void SetArm(RoboticArm arm)
        {
            this.arm = arm;
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
            puzzles.AddRange(MoveToRegionAndGetPuzzles(1));
            UpdatePuzzleList();
        }

        private void listBox_detected_puzzles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (puzzles == null || puzzles.Count == 0)
                return;
            var puzzle = puzzles[listBox_detected_puzzles.SelectedIndex];
            pictureBox_puzzle_image.Image = puzzle.puzzle2D.ROI.ToBitmap();
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
                arm.MoveAbsolute(positions, new AdditionalMotionParameters { CoordinateType = RASDK.Arm.Type.CoordinateType.Descartes, NeedWait = true});
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            puzzles.Clear();
            puzzles.AddRange(MoveToRegionAndGetPuzzles(2));
            UpdatePuzzleList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sucker = new SuckerDevice();
            sucker.Connect();
            sucker.Disable();
            try
            {
                camera = new IDSCamera(new GeneralMessageHandler(new EmptyLogHandler()));
                camera.Connect();
                camera.LoadParameterFromEEPROM();
            }catch(Exception ex)
            {
                MessageBox.Show("攝影機連線錯誤");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (camera == null)
                return;
            camera.Disconnect();
            camera = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            puzzles.Clear();
            puzzles.AddRange(MoveToRegionAndGetPuzzles(1));
            puzzles.AddRange(MoveToRegionAndGetPuzzles(2));
            UpdatePuzzleList();
        }
        private List<Puzzle3D> MoveToRegionAndGetPuzzles(int index)
        {
            var puzzles=new List<Puzzle3D>();
            var region = GetRegion(index);
            arm.MoveAbsolute(region.capture_position, new RASDK.Arm.AdditionalMotionParameters { CoordinateType = RASDK.Arm.Type.CoordinateType.Descartes, NeedWait = true});
            factory.setVisionPositioning(CCIA.LoadFromCsv(region.positioning_filepath));

            var image = camera.GetImage().ToImage<Bgr, byte>();
            puzzles.AddRange(factory.Execute(image,region.ROI));
            return puzzles;
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
                arm.MoveAbsolute(new double[] { puzzle.RealWorldCoordinate.X, puzzle.RealWorldCoordinate.Y, 30, 180, 0, 90 }, new AdditionalMotionParameters { CoordinateType = RASDK.Arm.Type.CoordinateType.Descartes, NeedWait = true});
                arm.MoveAbsolute(new double[] { puzzle.RealWorldCoordinate.X, puzzle.RealWorldCoordinate.Y, 3.572, 180, 0, 90 }, new AdditionalMotionParameters { CoordinateType = RASDK.Arm.Type.CoordinateType.Descartes, NeedWait = true, MotionType = RASDK.Arm.Type.MotionType.Linear});
                sucker.Enable();
                Thread.Sleep(500);
                arm.MoveAbsolute(new double[] { puzzle.RealWorldCoordinate.X, puzzle.RealWorldCoordinate.Y, 40, 180, 0, 90 }, new AdditionalMotionParameters { CoordinateType = RASDK.Arm.Type.CoordinateType.Descartes, NeedWait = true, MotionType = RASDK.Arm.Type.MotionType.Linear});
                Thread.Sleep(500);
                sucker.Disable();
            }
        }
    }
}
