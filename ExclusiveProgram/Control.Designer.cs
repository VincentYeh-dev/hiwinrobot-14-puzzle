﻿namespace ExclusiveProgram
{
    partial class Control
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.recognize_match_puzzleView = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.roi_puzzleView = new System.Windows.Forms.FlowLayoutPanel();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.capture_binarization_preview = new System.Windows.Forms.PictureBox();
            this.capture_preview = new System.Windows.Forms.PictureBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.numericUpDown_threshold = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.min_size_numeric = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.max_size_numeric = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numericUpDown_uniqueness_threshold = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.camera_preview = new System.Windows.Forms.PictureBox();
            this.button4 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.button11 = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.source_file_path = new System.Windows.Forms.TextBox();
            this.text_red_weight = new System.Windows.Forms.TextBox();
            this.modelImage_file_path = new System.Windows.Forms.TextBox();
            this.board_file_path = new System.Windows.Forms.TextBox();
            this.offset_x = new System.Windows.Forms.TextBox();
            this.offset_y = new System.Windows.Forms.TextBox();
            this.text_green_weight = new System.Windows.Forms.TextBox();
            this.text_blue_weight = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.numeric_dilateErodeSize = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.check_positioning_enable = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.capture_binarization_preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.capture_preview)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_threshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.min_size_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.max_size_numeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_uniqueness_threshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.camera_preview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_dilateErodeSize)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.recognize_match_puzzleView);
            this.tabPage4.Location = new System.Drawing.Point(4, 28);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage4.Size = new System.Drawing.Size(1584, 546);
            this.tabPage4.TabIndex = 5;
            this.tabPage4.Text = "辨識結果";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // recognize_match_puzzleView
            // 
            this.recognize_match_puzzleView.AutoScroll = true;
            this.recognize_match_puzzleView.Location = new System.Drawing.Point(6, 4);
            this.recognize_match_puzzleView.Margin = new System.Windows.Forms.Padding(4);
            this.recognize_match_puzzleView.Name = "recognize_match_puzzleView";
            this.recognize_match_puzzleView.Size = new System.Drawing.Size(1566, 530);
            this.recognize_match_puzzleView.TabIndex = 11;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.roi_puzzleView);
            this.tabPage3.Location = new System.Drawing.Point(4, 28);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(1584, 546);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "拼圖定位";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // roi_puzzleView
            // 
            this.roi_puzzleView.AutoScroll = true;
            this.roi_puzzleView.Location = new System.Drawing.Point(6, 4);
            this.roi_puzzleView.Margin = new System.Windows.Forms.Padding(4);
            this.roi_puzzleView.Name = "roi_puzzleView";
            this.roi_puzzleView.Size = new System.Drawing.Size(1566, 530);
            this.roi_puzzleView.TabIndex = 11;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.flowLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 28);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1584, 546);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "擷取預覽";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.capture_preview);
            this.flowLayoutPanel1.Controls.Add(this.capture_binarization_preview);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1570, 530);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // capture_binarization_preview
            // 
            this.capture_binarization_preview.Location = new System.Drawing.Point(756, 4);
            this.capture_binarization_preview.Margin = new System.Windows.Forms.Padding(4);
            this.capture_binarization_preview.Name = "capture_binarization_preview";
            this.capture_binarization_preview.Size = new System.Drawing.Size(744, 520);
            this.capture_binarization_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.capture_binarization_preview.TabIndex = 0;
            this.capture_binarization_preview.TabStop = false;
            // 
            // capture_preview
            // 
            this.capture_preview.Location = new System.Drawing.Point(4, 4);
            this.capture_preview.Margin = new System.Windows.Forms.Padding(4);
            this.capture_preview.Name = "capture_preview";
            this.capture_preview.Size = new System.Drawing.Size(744, 520);
            this.capture_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.capture_preview.TabIndex = 1;
            this.capture_preview.TabStop = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.check_positioning_enable);
            this.tabPage1.Controls.Add(this.button13);
            this.tabPage1.Controls.Add(this.button12);
            this.tabPage1.Controls.Add(this.button9);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.numeric_dilateErodeSize);
            this.tabPage1.Controls.Add(this.label16);
            this.tabPage1.Controls.Add(this.text_blue_weight);
            this.tabPage1.Controls.Add(this.text_green_weight);
            this.tabPage1.Controls.Add(this.offset_y);
            this.tabPage1.Controls.Add(this.offset_x);
            this.tabPage1.Controls.Add(this.board_file_path);
            this.tabPage1.Controls.Add(this.modelImage_file_path);
            this.tabPage1.Controls.Add(this.text_red_weight);
            this.tabPage1.Controls.Add(this.source_file_path);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label15);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.button11);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.button10);
            this.tabPage1.Controls.Add(this.button8);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.button7);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.button6);
            this.tabPage1.Controls.Add(this.button5);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.camera_preview);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.numericUpDown_uniqueness_threshold);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.max_size_numeric);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.min_size_numeric);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.numericUpDown_threshold);
            this.tabPage1.Location = new System.Drawing.Point(4, 28);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1584, 546);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "參數設定";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // numericUpDown_threshold
            // 
            this.numericUpDown_threshold.Location = new System.Drawing.Point(230, 296);
            this.numericUpDown_threshold.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown_threshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_threshold.Name = "numericUpDown_threshold";
            this.numericUpDown_threshold.Size = new System.Drawing.Size(78, 29);
            this.numericUpDown_threshold.TabIndex = 5;
            this.numericUpDown_threshold.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 488);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(172, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(322, 64);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(112, 34);
            this.button2.TabIndex = 6;
            this.button2.Text = "瀏覽";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label1.Location = new System.Drawing.Point(52, 296);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "二值化臨界值";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label2.Location = new System.Drawing.Point(98, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 24);
            this.label2.TabIndex = 9;
            this.label2.Text = "來源";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(446, 64);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 34);
            this.button3.TabIndex = 10;
            this.button3.Text = "擷取";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label3.Location = new System.Drawing.Point(52, 336);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "拼圖最小大小";
            // 
            // min_size_numeric
            // 
            this.min_size_numeric.Location = new System.Drawing.Point(230, 338);
            this.min_size_numeric.Margin = new System.Windows.Forms.Padding(4);
            this.min_size_numeric.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.min_size_numeric.Name = "min_size_numeric";
            this.min_size_numeric.Size = new System.Drawing.Size(78, 29);
            this.min_size_numeric.TabIndex = 13;
            this.min_size_numeric.Value = new decimal(new int[] {
            175,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label6.Location = new System.Drawing.Point(340, 338);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 24);
            this.label6.TabIndex = 15;
            this.label6.Text = "拼圖最大大小";
            // 
            // max_size_numeric
            // 
            this.max_size_numeric.Location = new System.Drawing.Point(494, 338);
            this.max_size_numeric.Margin = new System.Windows.Forms.Padding(4);
            this.max_size_numeric.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.max_size_numeric.Name = "max_size_numeric";
            this.max_size_numeric.Size = new System.Drawing.Size(86, 29);
            this.max_size_numeric.TabIndex = 17;
            this.max_size_numeric.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label9.Location = new System.Drawing.Point(52, 378);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(130, 24);
            this.label9.TabIndex = 24;
            this.label9.Text = "獨特性臨界";
            // 
            // numericUpDown_uniqueness_threshold
            // 
            this.numericUpDown_uniqueness_threshold.Location = new System.Drawing.Point(230, 378);
            this.numericUpDown_uniqueness_threshold.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDown_uniqueness_threshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericUpDown_uniqueness_threshold.Name = "numericUpDown_uniqueness_threshold";
            this.numericUpDown_uniqueness_threshold.Size = new System.Drawing.Size(78, 29);
            this.numericUpDown_uniqueness_threshold.TabIndex = 25;
            this.numericUpDown_uniqueness_threshold.Value = new decimal(new int[] {
            85,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label8.Location = new System.Drawing.Point(52, 424);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 24);
            this.label8.TabIndex = 28;
            this.label8.Text = "顏色加權";
            // 
            // camera_preview
            // 
            this.camera_preview.Location = new System.Drawing.Point(765, 8);
            this.camera_preview.Name = "camera_preview";
            this.camera_preview.Size = new System.Drawing.Size(807, 532);
            this.camera_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.camera_preview.TabIndex = 30;
            this.camera_preview.TabStop = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(446, 22);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(112, 34);
            this.button4.TabIndex = 31;
            this.button4.Text = "設定";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label10.Location = new System.Drawing.Point(74, 26);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 24);
            this.label10.TabIndex = 32;
            this.label10.Text = "攝影機";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(196, 22);
            this.button5.Margin = new System.Windows.Forms.Padding(4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(112, 34);
            this.button5.TabIndex = 33;
            this.button5.Text = "開啟";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(322, 22);
            this.button6.Margin = new System.Windows.Forms.Padding(4);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(112, 34);
            this.button6.TabIndex = 34;
            this.button6.Text = "關閉";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label7.Location = new System.Drawing.Point(52, 114);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 24);
            this.label7.TabIndex = 37;
            this.label7.Text = "樣板圖片";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(322, 106);
            this.button7.Margin = new System.Windows.Forms.Padding(4);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(112, 34);
            this.button7.TabIndex = 38;
            this.button7.Text = "瀏覽";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label11.Location = new System.Drawing.Point(52, 192);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 24);
            this.label11.TabIndex = 40;
            this.label11.Text = "定位圖片";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(322, 184);
            this.button8.Margin = new System.Windows.Forms.Padding(4);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(112, 34);
            this.button8.TabIndex = 41;
            this.button8.Text = "瀏覽";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(446, 184);
            this.button10.Margin = new System.Windows.Forms.Padding(4);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(112, 34);
            this.button10.TabIndex = 44;
            this.button10.Text = "擷取";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label12.Location = new System.Drawing.Point(52, 230);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(106, 24);
            this.label12.TabIndex = 45;
            this.label12.Text = "定位座標";
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(446, 228);
            this.button11.Margin = new System.Windows.Forms.Padding(4);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(112, 34);
            this.button11.TabIndex = 46;
            this.button11.Text = "擷取";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label13.Location = new System.Drawing.Point(164, 231);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(26, 24);
            this.label13.TabIndex = 48;
            this.label13.Text = "X";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label15.Location = new System.Drawing.Point(288, 231);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(26, 24);
            this.label15.TabIndex = 52;
            this.label15.Text = "Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label4.Location = new System.Drawing.Point(186, 424);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 24);
            this.label4.TabIndex = 53;
            this.label4.Text = "R";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label5.Location = new System.Drawing.Point(280, 424);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 24);
            this.label5.TabIndex = 55;
            this.label5.Text = "G";
            // 
            // source_file_path
            // 
            this.source_file_path.Location = new System.Drawing.Point(160, 72);
            this.source_file_path.Margin = new System.Windows.Forms.Padding(4);
            this.source_file_path.Name = "source_file_path";
            this.source_file_path.Size = new System.Drawing.Size(156, 29);
            this.source_file_path.TabIndex = 7;
            this.source_file_path.Text = "samples\\\\Test2.jpg";
            // 
            // text_red_weight
            // 
            this.text_red_weight.Location = new System.Drawing.Point(219, 424);
            this.text_red_weight.Margin = new System.Windows.Forms.Padding(4);
            this.text_red_weight.Name = "text_red_weight";
            this.text_red_weight.Size = new System.Drawing.Size(56, 29);
            this.text_red_weight.TabIndex = 29;
            this.text_red_weight.Text = "0.299";
            // 
            // modelImage_file_path
            // 
            this.modelImage_file_path.Location = new System.Drawing.Point(160, 112);
            this.modelImage_file_path.Margin = new System.Windows.Forms.Padding(4);
            this.modelImage_file_path.Name = "modelImage_file_path";
            this.modelImage_file_path.Size = new System.Drawing.Size(156, 29);
            this.modelImage_file_path.TabIndex = 39;
            this.modelImage_file_path.Text = "modelImage2.jpg";
            // 
            // board_file_path
            // 
            this.board_file_path.Location = new System.Drawing.Point(160, 190);
            this.board_file_path.Margin = new System.Windows.Forms.Padding(4);
            this.board_file_path.Name = "board_file_path";
            this.board_file_path.Size = new System.Drawing.Size(156, 29);
            this.board_file_path.TabIndex = 42;
            this.board_file_path.Text = "C:\\Users\\robotlab\\Pictures\\\\cb_00.jpg";
            // 
            // offset_x
            // 
            this.offset_x.Location = new System.Drawing.Point(196, 230);
            this.offset_x.Margin = new System.Windows.Forms.Padding(4);
            this.offset_x.Name = "offset_x";
            this.offset_x.Size = new System.Drawing.Size(79, 29);
            this.offset_x.TabIndex = 47;
            this.offset_x.Text = "59.17";
            // 
            // offset_y
            // 
            this.offset_y.Location = new System.Drawing.Point(322, 230);
            this.offset_y.Margin = new System.Windows.Forms.Padding(4);
            this.offset_y.Name = "offset_y";
            this.offset_y.Size = new System.Drawing.Size(78, 29);
            this.offset_y.TabIndex = 51;
            this.offset_y.Text = "-488.341";
            // 
            // text_green_weight
            // 
            this.text_green_weight.Location = new System.Drawing.Point(314, 424);
            this.text_green_weight.Margin = new System.Windows.Forms.Padding(4);
            this.text_green_weight.Name = "text_green_weight";
            this.text_green_weight.Size = new System.Drawing.Size(56, 29);
            this.text_green_weight.TabIndex = 54;
            this.text_green_weight.Text = "0.587";
            // 
            // text_blue_weight
            // 
            this.text_blue_weight.Location = new System.Drawing.Point(417, 424);
            this.text_blue_weight.Margin = new System.Windows.Forms.Padding(4);
            this.text_blue_weight.Name = "text_blue_weight";
            this.text_blue_weight.Size = new System.Drawing.Size(56, 29);
            this.text_blue_weight.TabIndex = 56;
            this.text_blue_weight.Text = "0.144";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label16.Location = new System.Drawing.Point(384, 424);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(25, 24);
            this.label16.TabIndex = 57;
            this.label16.Text = "B";
            // 
            // numeric_dilateErodeSize
            // 
            this.numeric_dilateErodeSize.Location = new System.Drawing.Point(518, 296);
            this.numeric_dilateErodeSize.Margin = new System.Windows.Forms.Padding(4);
            this.numeric_dilateErodeSize.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.numeric_dilateErodeSize.Name = "numeric_dilateErodeSize";
            this.numeric_dilateErodeSize.Size = new System.Drawing.Size(62, 29);
            this.numeric_dilateErodeSize.TabIndex = 58;
            this.numeric_dilateErodeSize.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.label17.Location = new System.Drawing.Point(339, 296);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(166, 24);
            this.label17.TabIndex = 59;
            this.label17.Text = "擴張/侵蝕 大小";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(567, 22);
            this.button9.Margin = new System.Windows.Forms.Padding(4);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(153, 34);
            this.button9.TabIndex = 60;
            this.button9.Text = "從EEPROM設定";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(567, 64);
            this.button12.Margin = new System.Windows.Forms.Padding(4);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(86, 34);
            this.button12.TabIndex = 61;
            this.button12.Text = "拍照區1";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(662, 64);
            this.button13.Margin = new System.Windows.Forms.Padding(4);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(86, 34);
            this.button13.TabIndex = 62;
            this.button13.Text = "拍照區2";
            this.button13.UseVisualStyleBackColor = true;
            // 
            // check_positioning_enable
            // 
            this.check_positioning_enable.AutoSize = true;
            this.check_positioning_enable.Location = new System.Drawing.Point(57, 160);
            this.check_positioning_enable.Margin = new System.Windows.Forms.Padding(4);
            this.check_positioning_enable.Name = "check_positioning_enable";
            this.check_positioning_enable.Size = new System.Drawing.Size(70, 22);
            this.check_positioning_enable.TabIndex = 63;
            this.check_positioning_enable.Text = "定位";
            this.check_positioning_enable.UseVisualStyleBackColor = true;
            this.check_positioning_enable.CheckedChanged += new System.EventHandler(this.positioning_enable_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(4, 4);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1592, 578);
            this.tabControl1.TabIndex = 8;
            // 
            // Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Control";
            this.Size = new System.Drawing.Size(1652, 722);
            this.tabPage4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.capture_binarization_preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.capture_preview)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_threshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.min_size_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.max_size_numeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_uniqueness_threshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.camera_preview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numeric_dilateErodeSize)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.FlowLayoutPanel recognize_match_puzzleView;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.FlowLayoutPanel roi_puzzleView;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox capture_preview;
        private System.Windows.Forms.PictureBox capture_binarization_preview;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox check_positioning_enable;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown numeric_dilateErodeSize;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox text_blue_weight;
        private System.Windows.Forms.TextBox text_green_weight;
        private System.Windows.Forms.TextBox offset_y;
        private System.Windows.Forms.TextBox offset_x;
        private System.Windows.Forms.TextBox board_file_path;
        private System.Windows.Forms.TextBox modelImage_file_path;
        private System.Windows.Forms.TextBox text_red_weight;
        private System.Windows.Forms.TextBox source_file_path;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox camera_preview;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numericUpDown_uniqueness_threshold;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown max_size_numeric;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown min_size_numeric;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown numericUpDown_threshold;
        private System.Windows.Forms.TabControl tabControl1;
    }
}
