using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using ExclusiveProgram.puzzle;
using ExclusiveProgram.puzzle.visual.concrete;
using ExclusiveProgram.puzzle.visual.concrete.utils;
using ExclusiveProgram.puzzle.visual.framework;
using ExclusiveProgram.puzzle.visual.framework.utils;
using ExclusiveProgram.ui.component;
using RASDK.Basic;
using RASDK.Basic.Message;
using RASDK.Vision;
using RASDK.Vision.IDS;
using RASDK.Vision.Positioning;

namespace ExclusiveProgram
{


    public partial class Control : MainForm.ExclusiveControl
    {
        private IDSCamera camera;
        private bool positioning_enable=true;

        //private VideoCapture capture;
        private delegate void DelShowResult(Puzzle3D puzzles);


        public Control()
        {
            InitializeComponent();
            Config = new Config();
            check_positioning_enable.Checked = positioning_enable;
        }


        private class MyFactoryListener : PuzzleFactoryListener
        {
            private delegate void DelonLocated(List<LocationResult> results);
            private delegate void DelonPreprocessDone(Image<Gray, byte> result);
            private int index;
            public MyFactoryListener(Control ui)
            {
                this.ui = ui;
            }

            private readonly Control ui;

            public void onPreprocessDone(Image<Gray, byte> result)
            {
                if (this.ui.InvokeRequired)
                {
                    DelonPreprocessDone del = new DelonPreprocessDone(onPreprocessDone);
                    this.ui.Invoke(del, result);
                }
                else
                {

                    ui.capture_binarization_preview.Image = result.ToBitmap();
                }
            }

            public void onLocated(List<LocationResult> results)
            {
                if (ui.InvokeRequired)
                {
                    DelonLocated del = new DelonLocated(onLocated);
                    ui.Invoke(del, results);
                }
                else
                {
                    foreach (var result in results)
                    {
                        var control = new UserControl1();
                        control.setImage(result.ROI.ToBitmap());
                        control.setLabel(new string[] { $"({result.Coordinate.X},{result.Coordinate.Y})",$"[{result.Size.Width},{result.Size.Height}]",""});
                        ui.roi_puzzleView.Controls.Add(control);
                    }
                }
            }



            public void onRecognized(RecognizeResult result)
            {
            }

        }

        private void DoPuzzleVisual()
        {

            var minSize = new Size((int)min_size_numeric.Value, (int)min_size_numeric.Value);
            var maxSize = new Size((int)max_size_numeric.Value, (int)max_size_numeric.Value);
            var threshold = (int)numericUpDown_threshold.Value;
            var uniquenessThreshold = ((double)numericUpDown_uniqueness_threshold.Value) * 0.01f;
            var modelImage = new Image<Bgr,byte>(modelImage_file_path.Text);
            var boardImage= new Image<Bgr,byte>(positioning_file_path.Text);
            var offset = new PointF(float.Parse(positioning_x.Text),float.Parse(positioning_y.Text));
            //var offset = new PointF(0,0);
            var dilateErodeSize = (int)numeric_dilateErodeSize.Value;
            var red_weight = Double.Parse(text_red_weight.Text);
            var green_weight = Double.Parse(text_green_weight.Text);
            var blue_weight = Double.Parse(text_blue_weight.Text);
            var scalar = new MCvScalar(blue_weight,green_weight,red_weight);

            var factory = GenerateFactory(scalar,threshold,uniquenessThreshold,minSize,maxSize,modelImage,boardImage,offset,dilateErodeSize);
 
            var image= new Image<Bgr,byte>(source_file_path.Text);
            capture_preview.Image = image.ToBitmap();
            List<Puzzle3D> results = factory.Execute(image,Rectangle.FromLTRB(1068,30,2440,1999));

            foreach (Puzzle3D result in results)
            {
                ShowResult(result);
            }

        }
        private DefaultPuzzleFactory GenerateFactory(MCvScalar scalar,int threshold,double uniquenessThreshold,Size minSize,Size maxSize,Image<Bgr,byte> modelImage,Image<Bgr,byte> boardImage,PointF offset,int dilateErodeSize)
        {
            //var preprocessImpl = new CLANEPreprocessImpl(3,new Size(8,8));
            IPreprocessImpl preprocessImpl=null;
            var grayConversionImpl = new WeightGrayConversionImpl(scalar);
            var thresoldImpl = new NormalThresoldImpl(threshold);
            var binaryPreprocessImpl = new DilateErodeBinaryPreprocessImpl(new Size(dilateErodeSize,dilateErodeSize));
            var locator = new PuzzleLocator(minSize, maxSize, null, grayConversionImpl, thresoldImpl, binaryPreprocessImpl, 0.01);

            var recognizer = new PuzzleRecognizer(modelImage, uniquenessThreshold, new SiftFlannPuzzleRecognizerImpl(), preprocessImpl, grayConversionImpl, thresoldImpl,binaryPreprocessImpl);
            //recognizer.setListener(new MyRecognizeListener(this));

            var factory = new DefaultPuzzleFactory(locator, recognizer, new PuzzleResultMerger(), 5);
            factory.setListener(new MyFactoryListener(this));
            
            if(positioning_enable)
                factory.setVisionPositioning(GetVisionPositioning(boardImage,offset));
            else
                factory.setVisionPositioning(null);

            return factory;
        }

        private void Approx(double ex, double ey, ref double vx, ref double vy)
        {
            double p = 0.05;
            vx += p * ex;
            vy += p * ey;
        }

        private IVisionPositioning GetVisionPositioning(Image<Bgr,byte> image,PointF WorldOffset)
        {
            List<Image<Bgr,byte>> images = new List<Image<Bgr,byte>>();
            images.Add(image);
            images.Add(new Image<Bgr, byte>("cb_01.jpg"));
            images.Add(new Image<Bgr, byte>("cb_02.jpg"));
            images.Add(new Image<Bgr, byte>("cb_03.jpg"));
            images.Add(new Image<Bgr, byte>("cb_04.jpg"));
            var cc = new CameraCalibration(new Size(12,9),15);
            cc.Run(images,out var cameraMatrix, out var distortionCoeffs, out var rotationVectors, out var translationVectors);
            var positioning= new CCIA(new CameraParameter(cameraMatrix, distortionCoeffs, rotationVectors[0], translationVectors[0]), 5, null, Approx );
            positioning.WorldOffset = WorldOffset;
            positioning.InterativeTimeout = 3;
            return positioning;
        }
        private string SelectFile(string InitialDirectory,string Filter)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = InitialDirectory ;
            openFileDialog1.Filter = Filter;
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                return openFileDialog1.FileName;
            }
            return "";
        }

        private void ShowResult(Puzzle3D result)
        {
            if (this.InvokeRequired)
            {
                DelShowResult del = new DelShowResult(ShowResult);
                this.Invoke(del, result);
            }
            else
            {
                var control = new UserControl1();
                control.setImage(result.puzzle2D.ROI.ToBitmap());
                control.setLabel(new string[] { $"Angle:{ Math.Round(result.Angel, 2)}",$"R:({result.RealWorldCoordinate.X},{result.RealWorldCoordinate.Y})",$"I:({result.puzzle2D.Coordinate.X},{result.puzzle2D.Coordinate.Y})" });
                recognize_match_puzzleView.Controls.Add(control);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            roi_puzzleView.Controls.Clear();
            recognize_match_puzzleView.Controls.Clear();
            //Thread thread = new Thread(DoPuzzleVisual);
            //thread.Start();
            DoPuzzleVisual();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            source_file_path.Text = SelectFile("", "Image files|*.*");
        }
        private void button7_Click(object sender, EventArgs e)
        {
            modelImage_file_path.Text = SelectFile("", "Image files|*.*");
        }
        private void button8_Click(object sender, EventArgs e)
        {
            positioning_file_path.Text = SelectFile("", "Image files|*.*");
        }


        private void button3_Click(object sender, EventArgs e)
        {

            if (camera != null&&camera.Connected)
            {

                camera.GetImage().Save("Capture_Source.bmp", ImageFormat.Bmp);
                source_file_path.Text = "Capture_Source.bmp";
            }
            else
                MessageBox.Show("尚未連接攝影機");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (camera!=null&&camera.Connected)
                camera.ShowSettingForm();
            else
                MessageBox.Show("尚未連接攝影機");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            camera = new IDSCamera(new GeneralMessageHandler(new EmptyLogHandler()),camera_preview);
            camera.Connect();
            camera.LoadParameterFromEEPROM();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (camera == null)
                return;
            camera.Disconnect();
            camera = null;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (camera != null&&camera.Connected)
            {

                camera.GetImage().Save("Capture_Positioning.bmp", ImageFormat.Bmp);
                positioning_file_path.Text = "Capture_Positioning.bmp";
            }
            else
                MessageBox.Show("尚未連接攝影機");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            double[] position = Arm.GetNowPosition();
            var x=position[0];
            var y = -position[1];
            var z = position[2];
            positioning_x.Text = x.ToString();
            positioning_y.Text = y.ToString();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            camera.LoadParameterFromEEPROM();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            //Arm.MoveAbsolute(195.351, 368.003, 230.336, 180, 0,90, new RASDK.Arm.AdditionalMotionParameters { CoordinateType = RASDK.Arm.Type.CoordinateType.Descartes, NeedWait = true});
            Arm.MoveAbsolute(new double[] { 195.351, 368.003, 230.336, 180, 0, 90 }, new RASDK.Arm.AdditionalMotionParameters { CoordinateType = RASDK.Arm.Type.CoordinateType.Descartes, NeedWait = true});

            if (camera != null&&camera.Connected)
            {

                camera.GetImage().Save("Capture_Source.bmp", ImageFormat.Bmp);
                source_file_path.Text = "Capture_Source.bmp";
            }
            else
                MessageBox.Show("尚未連接攝影機");
        }

        private void positioning_enable_CheckedChanged(object sender, EventArgs e)
        {
            positioning_enable = check_positioning_enable.Checked;
            positioning_x.Enabled = positioning_enable;
            positioning_y.Enabled = positioning_enable;
            button8.Enabled = positioning_enable;
            button10.Enabled = positioning_enable;
            button11.Enabled = positioning_enable;
            positioning_file_path.Enabled = positioning_enable;
        }

    }

}
