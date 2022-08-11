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
using ExclusiveProgram.device;
using ExclusiveProgram.puzzle;
using ExclusiveProgram.puzzle.visual;
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
    enum PositioningMethod
    {
        CCIA,Homography
    }

    public partial class Control : MainForm.ExclusiveControl
    {
        private IDSCamera camera;
        private delegate void DelShowResult(Puzzle3D puzzles);
        private PositioningUserControl PositioningUserControl;

        public Control()
        {
            InitializeComponent();
            Config = new Config();
            this.PositioningUserControl = new PositioningUserControl();
            flowLayoutPanel_positioning_root.Controls.Add(PositioningUserControl);
            comboBox_method.Items.Add(PositioningMethod.CCIA);
            comboBox_method.Items.Add(PositioningMethod.Homography);
        }



        private class MyFactoryListener : PuzzleFactoryListener
        {
            private delegate void DelonLocated(List<LocationResult> results);
            private delegate void DelonPreprocessDone(Image<Gray, byte> result);
            public MyFactoryListener(Control ui)
            {
                this.ui = ui;
            }

            private readonly Control ui;

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
                        var control = new PuzzlePreviewUserControl();
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
        
        public IPuzzleFactory GetFactoryFromUIArguments()
        {
            var minSize = new Size((int)min_size_numeric.Value, (int)min_size_numeric.Value);
            var maxSize = new Size((int)max_size_numeric.Value, (int)max_size_numeric.Value);
            var threshold = (int)numericUpDown_threshold.Value;
            var uniquenessThreshold = ((double)numericUpDown_uniqueness_threshold.Value) * 0.01f;
            var modelImage = new Image<Bgr,byte>(modelImage_file_path.Text);
            var dilateErodeSize = (int)numeric_dilateErodeSize.Value;
            var red_weight = Double.Parse(text_red_weight.Text);
            var green_weight = Double.Parse(text_green_weight.Text);
            var blue_weight = Double.Parse(text_blue_weight.Text);
            var scalar = new MCvScalar(blue_weight,green_weight,red_weight);

            var factory = VisualFacade.GenerateFactory(scalar,threshold,uniquenessThreshold,minSize,maxSize,modelImage,dilateErodeSize,new MyFactoryListener(this));
            return factory;
        }
        private void DoPuzzleVisual()
        {
            var factory=GetFactoryFromUIArguments();
            factory.setVisionPositioning(GetVisionPositioning());

            var rawImage= new Image<Bgr,byte>(source_file_path.Text);
            Image<Bgr,byte> image= null;
            if (comboBox_method.SelectedItem == null)
                image = rawImage;
            else if (comboBox_method.SelectedItem.Equals(PositioningMethod.CCIA))
                image = rawImage;
            else if (comboBox_method.SelectedItem.Equals(PositioningMethod.Homography))
                image = CameraCalibration.UndistortImage(rawImage, CameraParameter.LoadFromCsv(textBox_camera_parameter_filepath.Text));

            capture_preview.Image = image.ToBitmap();
            List<Puzzle3D> results = factory.Execute(image,Rectangle.FromLTRB(1068,30,2440,1999));

            foreach (Puzzle3D result in results)
            {
                ShowResult(result);
            }

        }
        //private DefaultPuzzleFactory GenerateFactory(MCvScalar scalar,int threshold,double uniquenessThreshold,Size minSize,Size maxSize,Image<Bgr,byte> modelImage,int dilateErodeSize)
        //{
        //    //var preprocessImpl = new CLANEPreprocessImpl(3,new Size(8,8));
        //    IPreprocessImpl preprocessImpl=null;
        //    var grayConversionImpl = new WeightGrayConversionImpl(scalar);
        //    var thresoldImpl = new NormalThresoldImpl(threshold);
        //    var binaryPreprocessImpl = new DilateErodeBinaryPreprocessImpl(new Size(dilateErodeSize,dilateErodeSize));
        //    var locator = new PuzzleLocator(minSize, maxSize, null, grayConversionImpl, thresoldImpl, binaryPreprocessImpl, 0.01);

        //    var recognizer = new PuzzleRecognizer(modelImage, uniquenessThreshold, new SiftFlannPuzzleRecognizerImpl(), preprocessImpl, grayConversionImpl, thresoldImpl,binaryPreprocessImpl);
        //    //recognizer.setListener(new MyRecognizeListener(this));

        //    var factory = new DefaultPuzzleFactory(locator, recognizer, new PuzzleResultMerger(), 5);
        //    factory.setListener(new MyFactoryListener(this));
        //    factory.setVisionPositioning(GetVisionPositioning());
        //    return factory;
        //}

        private IVisionPositioning GetVisionPositioning()
        {
            if (check_positioning_enable.Checked)
                if (comboBox_method.SelectedItem==null)
                    return null;
                else if (comboBox_method.SelectedItem.Equals(PositioningMethod.CCIA))
                    return CCIA.LoadFromCsv(textBox_positioning_filepath.Text);
                else if (comboBox_method.SelectedItem.Equals(PositioningMethod.Homography))
                    return HomographyPositioner.LoadFromCsv(textBox_positioning_filepath.Text);
            return null;
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
                var control = new PuzzlePreviewUserControl();
                control.setImage(result.puzzle2D.ROI.ToBitmap());
                control.setLabel(new string[] { $"Angle:{ Math.Round(result.Angle, 2)}",$"R:({result.RealWorldCoordinate.X},{result.RealWorldCoordinate.Y})",$"I:({result.puzzle2D.Coordinate.X},{result.puzzle2D.Coordinate.Y})" });

                control.SetImageClicked(() => { 
                    var positions = new double[] { result.RealWorldCoordinate.X, result.RealWorldCoordinate.Y, 10.938, 180, 0, 90 };
                    if (MessageBox.Show("Yes or no", "The Title", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
                        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                    {
                        Arm.MoveAbsolute(positions, new RASDK.Arm.AdditionalMotionParameters { CoordinateType = RASDK.Arm.Type.CoordinateType.Descartes, NeedWait = true});
                    }
                });
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
            try
            {
                camera = new IDSCamera(new GeneralMessageHandler(new EmptyLogHandler()), camera_preview);
                camera.Connect();
                camera.LoadParameterFromEEPROM();
                PositioningUserControl.setCamera(camera);
                
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

        private void button8_Click(object sender, EventArgs e)
        {
            textBox_positioning_filepath.Text = SelectFile("", "CSV files|*.csv");
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            textBox_camera_parameter_filepath.Text = SelectFile(".", "CSV files|*.csv");
        }

        private void comboBox_method_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_camera_parameter_filepath.Enabled = comboBox_method.SelectedItem.Equals(PositioningMethod.Homography);
            button9.Enabled = comboBox_method.SelectedItem.Equals(PositioningMethod.Homography);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            contestUserControl.SetFactory(GetFactoryFromUIArguments());
            contestUserControl.SetArm(Arm);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Arm.MoveAbsolute(new double[] { -195.351, 368.003, 230.336, 180, 0, 90 }, new RASDK.Arm.AdditionalMotionParameters { CoordinateType = RASDK.Arm.Type.CoordinateType.Descartes, NeedWait = true});

            if (camera != null&&camera.Connected)
            {

                camera.GetImage().Save("Capture_Source.bmp", ImageFormat.Bmp);
                source_file_path.Text = "Capture_Source.bmp";
            }
            else
                MessageBox.Show("尚未連接攝影機");
        }
    }

}
