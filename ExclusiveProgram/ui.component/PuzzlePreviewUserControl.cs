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
    public partial class PuzzlePreviewUserControl : UserControl
    {
        public delegate void OnImageClicked();
        private OnImageClicked onImageClicked;

        public PuzzlePreviewUserControl()
        {
            InitializeComponent();
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
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(onImageClicked!=null)
                onImageClicked();
        }

        public void SetImageClicked(OnImageClicked onImageClicked)
        {
            this.onImageClicked = onImageClicked;
        }
    }
}
