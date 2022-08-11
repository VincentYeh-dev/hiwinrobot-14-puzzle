namespace ExclusiveProgram.ui.component
{
    partial class ContestUserControl
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
            this.button1 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.listBox_detected_puzzles = new System.Windows.Forms.ListBox();
            this.pictureBox_puzzle_image = new System.Windows.Forms.PictureBox();
            this.textBox_puzzle_info = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_puzzle_image)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(211, 246);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "獲取所有拼圖";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(118, 246);
            this.button13.Margin = new System.Windows.Forms.Padding(4);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(86, 34);
            this.button13.TabIndex = 64;
            this.button13.Text = "拍照區2";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(24, 246);
            this.button12.Margin = new System.Windows.Forms.Padding(4);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(86, 34);
            this.button12.TabIndex = 63;
            this.button12.Text = "拍照區1";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // listBox_detected_puzzles
            // 
            this.listBox_detected_puzzles.FormattingEnabled = true;
            this.listBox_detected_puzzles.ItemHeight = 18;
            this.listBox_detected_puzzles.Location = new System.Drawing.Point(24, 73);
            this.listBox_detected_puzzles.Name = "listBox_detected_puzzles";
            this.listBox_detected_puzzles.Size = new System.Drawing.Size(328, 166);
            this.listBox_detected_puzzles.TabIndex = 65;
            this.listBox_detected_puzzles.SelectedIndexChanged += new System.EventHandler(this.listBox_detected_puzzles_SelectedIndexChanged);
            // 
            // pictureBox_puzzle_image
            // 
            this.pictureBox_puzzle_image.Location = new System.Drawing.Point(389, 13);
            this.pictureBox_puzzle_image.Name = "pictureBox_puzzle_image";
            this.pictureBox_puzzle_image.Size = new System.Drawing.Size(311, 251);
            this.pictureBox_puzzle_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_puzzle_image.TabIndex = 66;
            this.pictureBox_puzzle_image.TabStop = false;
            // 
            // textBox_puzzle_info
            // 
            this.textBox_puzzle_info.Location = new System.Drawing.Point(389, 270);
            this.textBox_puzzle_info.Multiline = true;
            this.textBox_puzzle_info.Name = "textBox_puzzle_info";
            this.textBox_puzzle_info.Size = new System.Drawing.Size(311, 117);
            this.textBox_puzzle_info.TabIndex = 67;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(389, 393);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(311, 33);
            this.button2.TabIndex = 68;
            this.button2.Text = "移動";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(236, 10);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(112, 34);
            this.button6.TabIndex = 71;
            this.button6.Text = "關閉";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(110, 10);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(112, 34);
            this.button5.TabIndex = 70;
            this.button5.Text = "開啟";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(20, 13);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 24);
            this.label10.TabIndex = 69;
            this.label10.Text = "攝影機";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(389, 432);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(311, 32);
            this.button3.TabIndex = 72;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(389, 470);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(311, 32);
            this.button4.TabIndex = 73;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // ContestUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox_puzzle_info);
            this.Controls.Add(this.pictureBox_puzzle_image);
            this.Controls.Add(this.listBox_detected_puzzles);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button1);
            this.Name = "ContestUserControl";
            this.Size = new System.Drawing.Size(1371, 521);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_puzzle_image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.ListBox listBox_detected_puzzles;
        private System.Windows.Forms.PictureBox pictureBox_puzzle_image;
        private System.Windows.Forms.TextBox textBox_puzzle_info;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}
