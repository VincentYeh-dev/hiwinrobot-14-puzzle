using Emgu.CV;
using Emgu.CV.Structure;
using RASDK.Arm;
using RASDK.Basic;
using RASDK.Basic.Message;
using RASDK.Vision;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExclusiveProgram.ui.component
{
    public partial class PositioningUserControl : UserControl
    {
        private IDSCamera camera;
        public PositioningUserControl()
        {
            InitializeComponent();
        }
        public void setCamera(IDSCamera camera)
        {
            this.camera = camera;
        }

        private CCIA GetCCIA(CameraParameter cp) {
            List<Image<Bgr,byte>> images = new List<Image<Bgr,byte>>();
            images.Add(new Image<Bgr, byte>(board_file_path.Text));
            var positioning = new CCIA(cp, 5, null, Approx);
            positioning.WorldOffset = new PointF(float.Parse(offset_x1.Text),float.Parse(offset_y1.Text));
            positioning.InterativeTimeout = 3;
            return positioning;
        }

        private AdvancedHomographyPositioner GetHomographyPositioner(CameraCalibration cameraCalibration)
        {
            var pointsOfWorld = new PointF[4];
            pointsOfWorld[0] = new PointF(float.Parse(offset_x1.Text),float.Parse(offset_y1.Text));
            pointsOfWorld[1] = new PointF(float.Parse(offset_x2.Text),float.Parse(offset_y2.Text));
            pointsOfWorld[2] = new PointF(float.Parse(offset_x3.Text),float.Parse(offset_y3.Text));
            pointsOfWorld[3] = new PointF(float.Parse(offset_x4.Text),float.Parse(offset_y4.Text));

            return new AdvancedHomographyPositioner(pointsOfWorld,cameraCalibration);
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
        private void button8_Click(object sender, EventArgs e)
        {
            board_file_path.Text = SelectFile("", "Image files|*.*");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (camera != null&&camera.Connected)
            {
                var timeStamp = DateTime.Now.ToString("yyyy-MM-dd_hhmmss");
                var filename = $"positioning\\Capture_Board {timeStamp}.bmp";
                camera.GetImage().Save(filename, ImageFormat.Bmp);
                board_file_path.Text = filename;
            }
            else
                MessageBox.Show("尚未連接攝影機");
        }


        private void Approx(double ex, double ey, ref double vx, ref double vy)
        {
            double p = 0.01;
            vx += p * ex;
            vy += p * ey;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            var timeStamp = DateTime.Now.ToString("yyyy-MM-dd_hhmmss");
            var method = comboBox_method.SelectedItem.ToString();

            var cc = new CameraCalibration(new Size(12,9),15);
            var images = new List<Image<Bgr, byte>>();
            images.Add(new Image<Bgr,byte>(board_file_path.Text));

            var cp = cc.CalCameraParameter(images);


            if (method.Equals("CCIA"))
            {
                var positioning=GetCCIA(cp);
                positioning.SaveToCsv($"positioning\\CCIA {timeStamp}.csv");
            }
            else if(method.Equals("Homography"))
            {
                var positioning=GetHomographyPositioner(cc);
                cp.SaveToCsv($"positioning\\CameraParamater {timeStamp}.csv");
                positioning.HomographyPositioner.SaveToCsv($"positioning\\Homography {timeStamp}.csv");
            }

        }

    }
}
