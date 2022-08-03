﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
using RASDK.Vision.IDS;

namespace ExclusiveProgram
{


    public partial class Control : MainForm.ExclusiveControl
    {
        private IDSCamera camera;

        //private VideoCapture capture;
        private delegate void DelShowResult(Puzzle2D puzzles);


        public Control()
        {
            InitializeComponent();
            Config = new Config();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            corrector_binarization_puzzleView.Controls.Clear();
            corrector_result_puzzleView.Controls.Clear();
            corrector_ROI_puzzleView.Controls.Clear();
            recognize_match_puzzleView.Controls.Clear();
            //Thread thread = new Thread(DoPuzzleVisual);
            //thread.Start();
            DoPuzzleVisual();
        }

        private void DoPuzzleVisual()
        {

            var minSize = new Size((int)min_width_numeric.Value, (int)min_height_numeric.Value);
            var maxSize = new Size((int)max_width_numeric.Value, (int)max_height_numeric.Value);
            var threshold = (int)numericUpDown_blockSize.Value;
            var green_weight = Double.Parse(textBox_param.Text);

            //var preprocessImpl = new CLANEPreprocessImpl(3,new Size(8,8));
            IPreprocessImpl preprocessImpl=null;
            
            var grayConversionImpl = new GreenBackgroundGrayConversionImpl(green_weight);
            var thresoldImpl = new NormalThresoldImpl(threshold);
            var binaryPreprocessImpl = new DilateErodeBinaryPreprocessImpl(new Size(3,3));

            IPuzzleFactory factory = null;
            try
            {
                var locator = new PuzzleLocator(minSize, maxSize, null, grayConversionImpl, thresoldImpl, binaryPreprocessImpl, 0.01);
                var uniquenessThreshold = ((double)numericUpDown_uniqueness_threshold.Value) * 0.01f;


                Color backgroundColor = getColorFromTextBox();

                var modelImage = CvInvoke.Imread("samples\\modelImage3.jpg").ToImage<Bgr, byte>();
                var recognizer = new PuzzleRecognizer(modelImage, uniquenessThreshold, new SiftFlannPuzzleRecognizerImpl(), preprocessImpl, grayConversionImpl, thresoldImpl,binaryPreprocessImpl);
                recognizer.setListener(new MyRecognizeListener(this));

                factory = new DefaultPuzzleFactory(locator, recognizer, new PuzzleResultMerger(), 5);
                factory.setListener(new MyFactoryListener(this));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "辨識錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var image = CvInvoke.Imread(file_path.Text).ToImage<Bgr, byte>();
            capture_preview.Image = image.ToBitmap();
            List<Puzzle2D> results = factory.Execute(image);

            foreach (Puzzle2D result in results)
            {
                ShowResult(result);
            }

        }

        private void ShowResult(Puzzle2D result)
        {
            if (this.InvokeRequired)
            {
                DelShowResult del = new DelShowResult(ShowResult);
                this.Invoke(del, result);
            }
            else
            {
                var control = new UserControl1();
                control.setImage(result.image.ToBitmap());
                control.setLabel("Angle:" + Math.Round(result.Angel, 2), result.Position);
                recognize_match_puzzleView.Controls.Add(control);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "D:\\git_projects\\Windows\\nfu-irs-lab\\hiwinrobot-14-puzzle\\ExclusiveProgram\\bin\\x64\\Debug";
            openFileDialog1.Filter = "Image files (*.jpg, *.png)|*.jpg;*.png";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                file_path.Text = selectedFileName;
                //...
            }
        }


        private void backgroundColor_textbox_TextChanged(object sender, EventArgs e)
        {
            var colorCode = backgroundColor_textbox.Text;
            if (colorCode == null || colorCode.Length != 7 || !colorCode.StartsWith("#"))
                return;
            Color preview_color = getColorFromTextBox();
            Bitmap Bmp = new Bitmap(100, 100);
            using (Graphics gfx = Graphics.FromImage(Bmp))
            using (SolidBrush brush = new SolidBrush(preview_color))
            {
                gfx.FillRectangle(brush, 0, 0, 100, 100);
            }
            backgroundColor_preview.Image = Bmp;
        }

        private Color getColorFromTextBox()
        {
            String colorCode = backgroundColor_textbox.Text;
            return (Color)new ColorConverter().ConvertFromString(colorCode);
        }


        private void corrector_binarization_puzzleView_Paint(object sender, PaintEventArgs e)
        {

        }

        private class MyCorrectorListener : PuzzleCorrectorListener
        {
            int index = 0;
            public void onPreprocessDone(Image<Gray, byte> result)
            {
                result.Save("results\\" + index++ + ".jpg");
            }
        }

        private class MyRecognizeListener : PuzzleRecognizerListener
        {
            int index = 0;
            public MyRecognizeListener(Control ui)
            {
                this.ui = ui;
            }

            private readonly Control ui;

            public void OnMatched(int id, Image<Bgr, byte> modelImage, VectorOfKeyPoint modelKeyPoints, Image<Bgr, byte> observedImage, VectorOfKeyPoint observedKeyPoints, VectorOfVectorOfDMatch matches, Mat mask, long matchTime)
            {
                Mat resultImage = new Mat();
                Features2DToolbox.DrawMatches(modelImage, modelKeyPoints, observedImage, observedKeyPoints,
                   matches, resultImage, new MCvScalar(0, 0, 255), new MCvScalar(255, 255, 255), mask);

                resultImage.Save("results\\matching_" + id + ".jpg");
                resultImage.Dispose();
            }

            public void OnPerspective(int id, Image<Bgr, byte> warpedPerspectiveImage, String position)
            {
                CvInvoke.PutText(warpedPerspectiveImage, string.Format("position: {0}", position), new Point(1, 50), FontFace.HersheySimplex, 1, new MCvScalar(100, 100, 255), 2, LineType.FourConnected);
                warpedPerspectiveImage.Save("results\\perspective_" + id + ".jpg");
            }
        }

        private class MyFactoryListener : PuzzleFactoryListener
        {
            private delegate void DelonLocated(List<LocationResult> results);
            private delegate void DelOnCorrected(Image<Bgr, byte> result);
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
                        control.setLabel(String.Format("({0},{1})", result.Coordinate.X, result.Coordinate.Y), String.Format("[{0},{1}]", result.Size.Width, result.Size.Height));
                        ui.corrector_ROI_puzzleView.Controls.Add(control);
                    }
                }
            }



            public void onRecognized(RecognizeResult result)
            {
            }

            public void onCorrected(Image<Bgr, byte> result)
            {
                if (ui.InvokeRequired)
                {
                    DelOnCorrected del = new DelOnCorrected(onCorrected);
                    ui.Invoke(del, result);
                }
                else
                {
                    var control = new UserControl1();
                    control.setImage(result.ToBitmap());
                    control.setLabel("", "");
                    this.ui.corrector_result_puzzleView.Controls.Add(control);
                    result.Save("results\\SS" + index++ + ".jpg");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (camera != null&&camera.Connected)
            {

                camera.GetImage().Save("Capture.jpg");
                file_path.Text = "Capture.jpg";
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
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (camera == null)
                return;
            camera.Disconnect();
            camera = null;
        }
    }

}
