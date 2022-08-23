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
        enum PositioningMethod
        {
            CCIA,Homography
        }
        public IDSCamera Camera;
        public RoboticArm Arm;

        public PositioningUserControl()
        {
            InitializeComponent();
            comboBox_method.Items.Add(PositioningMethod.CCIA);
            comboBox_method.Items.Add(PositioningMethod.Homography);
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

        private void button8_Click(object sender, EventArgs e)
        {
            board_file_path.Text = GlobalUtils.SelectFile(GlobalUtils.FILTER_IMAGE);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (Camera != null&&Camera.Connected)
            {
                var timeStamp = DateTime.Now.ToString("yyyy-MM-dd_hhmmss");
                var filename = $"positioning\\Capture_Board {timeStamp}.bmp";
                Camera.GetImage().Save(filename, ImageFormat.Bmp);
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
            var method = comboBox_method.SelectedItem;

            var cc = new CameraCalibration(new Size(12,9),15);
            var images = new List<Image<Bgr, byte>>();
            images.Add(new Image<Bgr,byte>(board_file_path.Text));
            foreach(var image_path in listBox_images.Items)
            {
                images.Add(new Image<Bgr,byte>(image_path.ToString()));
            }

            var cp = cc.CalCameraParameter(images);


            if (method.Equals(PositioningMethod.CCIA))
            {
                var positioning=GetCCIA(cp);
                positioning.SaveToCsv($"positioning\\CCIA {timeStamp}.csv");
            }
            else if(method.Equals(PositioningMethod.Homography))
            {
                var positioning=GetHomographyPositioner(cc);
                cp.SaveToCsv($"positioning\\CameraParamater {timeStamp}.csv");
                positioning.HomographyPositioner.SaveToCsv($"positioning\\Homography {timeStamp}.csv");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var files=GlobalUtils.SelectFiles(GlobalUtils.FILTER_IMAGE);
            foreach(var file in files)
            {
                listBox_images.Items.Add(file);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var timeStamp = DateTime.Now.ToString("yyyy-MM-dd_hhmmss");
            var filename = $"positioning\\Capture_Temp {timeStamp}.bmp";
            Camera.GetImage().Save(filename,ImageFormat.Bmp);
            listBox_images.Items.Add(filename);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var position = Arm.GetNowPosition();
            offset_x1.Text = position[0].ToString();
            if (comboBox_method.SelectedItem == null)
                return; 
            
            if (comboBox_method.SelectedItem.Equals(PositioningMethod.CCIA))
            {
                offset_y1.Text=(-position[1]).ToString();
            }
            else if(comboBox_method.SelectedItem.Equals(PositioningMethod.Homography))
            {
                offset_y1.Text=(position[1]).ToString();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var position = Arm.GetNowPosition();
            if (comboBox_method.SelectedItem == null)
                return; 
            if (comboBox_method.SelectedItem.Equals(PositioningMethod.CCIA))
            {
                throw new Exception("");
            }
            offset_x2.Text=position[0].ToString();
            offset_y2.Text=position[1].ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var position = Arm.GetNowPosition();
            if (comboBox_method.SelectedItem == null)
                return; 
            if (comboBox_method.SelectedItem.Equals(PositioningMethod.CCIA))
            {
                throw new Exception("");
            }
            offset_x3.Text=position[0].ToString();
            offset_y3.Text=position[1].ToString();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            var position = Arm.GetNowPosition();
            if (comboBox_method.SelectedItem == null)
                return; 
            if (comboBox_method.SelectedItem.Equals(PositioningMethod.CCIA))
            {
                throw new Exception("");
            }
            offset_x4.Text=position[0].ToString();
            offset_y4.Text=position[1].ToString();
        }

    }
}
