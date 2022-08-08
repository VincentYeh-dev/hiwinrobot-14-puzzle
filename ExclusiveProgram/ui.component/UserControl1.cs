using RASDK.Arm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExclusiveProgram.ui.component
{
    public partial class UserControl1 : UserControl
    {
        private readonly RoboticArm arm;
        private double[] positions;

        public UserControl1(RoboticArm arm)
        {
            InitializeComponent();
            this.arm = arm;
        }

        public void setImage(Bitmap bitmap)
        { 
            pictureBox1.Image = bitmap;
        }
        public void setLabel(string[] labels)
        {
            this.label1.Text = labels[0];
            this.label2.Text=labels[1];   
            this.label3.Text=labels[2];   
        }
        public void setPosition(double[] positions)
        {
            this.positions = positions;
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Yes or no", "The Title", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, 
                MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                arm.MoveAbsolute(positions, new RASDK.Arm.AdditionalMotionParameters { CoordinateType = RASDK.Arm.Type.CoordinateType.Descartes, NeedWait = true});
            }
        }
    }

}
