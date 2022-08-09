namespace ExclusiveProgram.ui.component
{
    partial class PositioningUserControl
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.offset_y = new System.Windows.Forms.TextBox();
            this.offset_x = new System.Windows.Forms.TextBox();
            this.board_file_path = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // offset_y
            // 
            this.offset_y.Location = new System.Drawing.Point(285, 75);
            this.offset_y.Margin = new System.Windows.Forms.Padding(4);
            this.offset_y.Name = "offset_y";
            this.offset_y.Size = new System.Drawing.Size(78, 29);
            this.offset_y.TabIndex = 61;
            this.offset_y.Text = "-396.062";
            // 
            // offset_x
            // 
            this.offset_x.Location = new System.Drawing.Point(159, 75);
            this.offset_x.Margin = new System.Windows.Forms.Padding(4);
            this.offset_x.Name = "offset_x";
            this.offset_x.Size = new System.Drawing.Size(79, 29);
            this.offset_x.TabIndex = 59;
            this.offset_x.Text = "165.008";
            // 
            // board_file_path
            // 
            this.board_file_path.Location = new System.Drawing.Point(123, 35);
            this.board_file_path.Margin = new System.Windows.Forms.Padding(4);
            this.board_file_path.Name = "board_file_path";
            this.board_file_path.Size = new System.Drawing.Size(156, 29);
            this.board_file_path.TabIndex = 55;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label15.Location = new System.Drawing.Point(251, 76);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(26, 24);
            this.label15.TabIndex = 62;
            this.label15.Text = "Y";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(127, 76);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 24);
            this.label13.TabIndex = 60;
            this.label13.Text = "X";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(15, 75);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 24);
            this.label12.TabIndex = 57;
            this.label12.Text = "定位座標";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(409, 29);
            this.button10.Margin = new System.Windows.Forms.Padding(4);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(112, 34);
            this.button10.TabIndex = 56;
            this.button10.Text = "擷取";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(285, 29);
            this.button8.Margin = new System.Windows.Forms.Padding(4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(112, 34);
            this.button8.TabIndex = 54;
            this.button8.Text = "瀏覽";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(15, 37);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 24);
            this.label11.TabIndex = 53;
            this.label11.Text = "定位圖片";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(409, 115);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 34);
            this.button1.TabIndex = 63;
            this.button1.Text = "儲存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PositioningUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.offset_y);
            this.Controls.Add(this.offset_x);
            this.Controls.Add(this.board_file_path);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label11);
            this.Name = "PositioningUserControl";
            this.Size = new System.Drawing.Size(535, 172);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox offset_y;
        private System.Windows.Forms.TextBox offset_x;
        private System.Windows.Forms.TextBox board_file_path;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button1;
    }
}
