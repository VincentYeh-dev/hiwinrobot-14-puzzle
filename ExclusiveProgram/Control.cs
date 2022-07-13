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
using ExclusiveProgram.puzzle.visual.concrete;
using ExclusiveProgram.puzzle.visual.framework;
using ExclusiveProgram.ui.component;

namespace ExclusiveProgram
{
    public partial class Control : MainForm.ExclusiveControl
    {
        //private VideoCapture capture;
        private delegate void DelShowResult(Puzzle_sturct puzzles);
        public Control()
        {
            InitializeComponent();
            Config = new Config();
            //capture=new VideoCapture(0);
            //capture.Set(CapProp.Brightness, 120);
            //capture.Set(CapProp.Exposure, 5);
            Thread thread = new Thread(monitor);
            thread.Start();
        }

        private void monitor()
        {
            /*
              Mat mat=null;
            while (true)
            {
                if(mat!=null)
                    mat.Dispose();
                var new_mat= capture.QueryFrame();
                var image = new_mat.ToImage<Bgr,byte>().ToBitmap();
                capture_image.Image = image;
                mat = new_mat;
                CvInvoke.WaitKey(100);
            }
            */
        }

        private void button1_Click(object sender, EventArgs e)
        {
            corrector_binarization_puzzleView.Controls.Clear();
            corrector_result_puzzleView.Controls.Clear();
            corrector_ROI_puzzleView.Controls.Clear();
            recognize_match_puzzleView.Controls.Clear();
            Thread thread = new Thread(DoPuzzleVisual);
            thread.Start();
        }

        private void DoPuzzleVisual()
        {

            var minSize = new Size((int)min_width_numeric.Value, (int)min_height_numeric.Value);
            var maxSize = new Size((int)max_width_numeric.Value, (int)max_height_numeric.Value);
            var threshold = (int)numericUpDown_threshold.Value;
            var locator = new PuzzleLocator(threshold, minSize, maxSize);
            var uniquenessThreshold = ((double)numericUpDown_uniqueness_threshold.Value) * 0.01f;

            locator.setListener(new MyPuzzleLocatorListener(this));


            var corrector_threshold = (int)corrector_threshold_numric.Value;
            Color backgroundColor = getColorFromTextBox();
            var corrector = new PuzzleCorrector(corrector_threshold,backgroundColor);

            var modelImage = CvInvoke.Imread("samples\\modelImage2.jpg").ToImage<Bgr,byte>();
            var recognizer = new PuzzleRecognizer(modelImage, uniquenessThreshold, new SiftFlannPuzzleRecognizerImpl(),corrector);
            recognizer.setListener(new MyRecognizeListener(this));

            var factory = new DefaultPuzzleFactory(locator, recognizer, corrector, new PuzzleResultMerger());
            factory.setListener(new MyFactoryListener(this));

            var image  = CvInvoke.Imread(file_path.Text).ToImage<Bgr,byte>();
            capture_preview.Image = image.ToBitmap();
            try
            {
                List<Puzzle_sturct> results = factory.Execute(image);

                foreach (Puzzle_sturct result in results)
                {
                    ShowResult(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                MessageBox.Show(ex.Message, "辨識錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowResult(Puzzle_sturct result)
        {
            if (this.InvokeRequired)
            {
                DelShowResult del = new DelShowResult(ShowResult);
                this.Invoke(del,result);
            }
            else
            {
                var control = new UserControl1();
                control.setImage(result.image.ToBitmap());
                control.setLabel("Angle:" + Math.Round(result.Angel, 2), result.position);
                recognize_match_puzzleView.Controls.Add(control);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "D:\\git_projects\\Windows\\nfu-irs-lab\\hiwinrobot-14-puzzle\\ExclusiveProgram\\bin\\x64\\Debug" ;
            openFileDialog1.Filter = "Image files (*.jpg, *.png)|*.jpg;*.png" ;
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true ;

            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                file_path.Text = selectedFileName;
                //...
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {

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


        private class MyPuzzleLocatorListener : PuzzleLocatorListener
        {
            private delegate void DelonPreprocessDone(Image<Gray, byte> result);

            public MyPuzzleLocatorListener(Control ui)
            {
                this.ui = ui;
            }

            private readonly Control ui;

            public void onBinarizationDone(Image<Gray, byte> result)
            {
            }

            public void onLocated(LocationResult result)
            {

            }

            public void onPreprocessDone(Image<Gray, byte> result)
            {
                if (this.ui.InvokeRequired)
                {
                    DelonPreprocessDone del = new DelonPreprocessDone(onPreprocessDone);
                    this.ui.Invoke(del,result);
                }
                else
                {
                    ui.capture_binarization_preview.Image=result.ToBitmap();
                }
            }
        }

        private class MyRecognizeListener : PuzzleRecognizerListener
        {
            int index = 0;
            public MyRecognizeListener (Control ui)
            {
                this.ui = ui;
            }

            private readonly Control ui;

            public void OnMatched(Image<Bgr, byte> modelImage, VectorOfKeyPoint modelKeyPoints, Image<Bgr, byte> observedImage, VectorOfPoint vp, VectorOfKeyPoint observedKeyPoints, VectorOfVectorOfDMatch matches, Mat mask, long matchTime, double Slope,double Angle)
            {
                Mat resultImage = new Mat();
                Features2DToolbox.DrawMatches(modelImage, modelKeyPoints, observedImage, observedKeyPoints,
                   matches, resultImage, new MCvScalar(0, 0, 255), new MCvScalar(255, 255, 255), mask);

                CvInvoke.PutText(resultImage, string.Format("{0}ms , Slop:{1:0.00} , Angle:{2:0.00}", matchTime, Slope,Angle), new Point(1, 50), FontFace.HersheySimplex, 1, new MCvScalar(100, 100, 255), 2, LineType.FourConnected);

                resultImage.Save("matching_results\\"+index+++".jpg");
                resultImage.Dispose();
            }

            public void OnPerspective(Mat observedImage,Mat modelImage,Mat homography,PointF[] pts)
            {
                var perspective_image = new Mat(observedImage.Size,modelImage.Depth,3);
                CvInvoke.WarpPerspective(modelImage, perspective_image, homography, observedImage.Size);
                observedImage.Save("perspective_results\\R"+index+".jpg");
                perspective_image.Save("perspective_results\\P"+index+".jpg");
            }

            public void OnCorrected(Image<Bgr, byte> image)
            {
                image.Save("perspective_results\\C"+index+".jpg");
            }
        }

        private class MyFactoryListener : PuzzleFactoryListener
        {
            private delegate void DelonLocated(List<LocationResult> results);
            private delegate void DelOnCorrected(Image<Bgr, byte> result);
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
                    ui.Invoke(del,results);
                }
                else
                {
                    foreach(var result in results)
                    {
                        var control = new UserControl1();
                        control.setImage(result.ROI.ToBitmap());
                        control.setLabel(result.Size.ToString(),result.Coordinate.ToString());
                        ui.corrector_ROI_puzzleView.Controls.Add(control);
                    }
                }
            }


            public void onCorrected(Image<Bgr, byte> result)
            {
                if (ui.InvokeRequired)
                {
                    DelOnCorrected del = new DelOnCorrected(onCorrected);
                    ui.Invoke(del,result);
                }
                else
                {
                    var control = new UserControl1();
                    control.setImage(result.ToBitmap());
                    control.setLabel("","");
                    this.ui.corrector_result_puzzleView.Controls.Add(control);
                }
            }

            public void onRecognized(RecognizeResult result)
            {
            }
        }
    


    }

}
