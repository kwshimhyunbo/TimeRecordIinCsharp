﻿namespace App
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.empNum = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioGo = new System.Windows.Forms.RadioButton();
            this.radioBack = new System.Windows.Forms.RadioButton();
            this.radioLate = new System.Windows.Forms.RadioButton();
            this.button4 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MainOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(575, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "관리자 창";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(575, 98);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "종료";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(575, 41);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "확인창";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // empNum
            // 
            this.empNum.Font = new System.Drawing.Font("Gulim", 15F);
            this.empNum.Location = new System.Drawing.Point(191, 403);
            this.empNum.Name = "empNum";
            this.empNum.Size = new System.Drawing.Size(221, 30);
            this.empNum.TabIndex = 3;
            this.empNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.empNum_KeyDown);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::App.Properties.Resources.one1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(84, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(472, 315);
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // radioGo
            // 
            this.radioGo.AutoSize = true;
            this.radioGo.Checked = true;
            this.radioGo.Location = new System.Drawing.Point(212, 464);
            this.radioGo.Name = "radioGo";
            this.radioGo.Size = new System.Drawing.Size(47, 16);
            this.radioGo.TabIndex = 10;
            this.radioGo.TabStop = true;
            this.radioGo.Text = "출근";
            this.radioGo.UseVisualStyleBackColor = true;
            // 
            // radioBack
            // 
            this.radioBack.AutoSize = true;
            this.radioBack.Location = new System.Drawing.Point(267, 464);
            this.radioBack.Name = "radioBack";
            this.radioBack.Size = new System.Drawing.Size(47, 16);
            this.radioBack.TabIndex = 11;
            this.radioBack.Text = "퇴근";
            this.radioBack.UseVisualStyleBackColor = true;
            // 
            // radioLate
            // 
            this.radioLate.AutoSize = true;
            this.radioLate.Location = new System.Drawing.Point(322, 464);
            this.radioLate.Name = "radioLate";
            this.radioLate.Size = new System.Drawing.Size(71, 16);
            this.radioLate.TabIndex = 12;
            this.radioLate.Text = "잔업퇴근";
            this.radioLate.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(575, 69);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 13;
            this.button4.Text = "회원목록";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(127, 412);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "사원번호:";
            // 
            // MainOk
            // 
            this.MainOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.MainOk.Location = new System.Drawing.Point(387, 402);
            this.MainOk.Name = "MainOk";
            this.MainOk.Size = new System.Drawing.Size(75, 29);
            this.MainOk.TabIndex = 5;
            this.MainOk.Text = "확인";
            this.MainOk.UseVisualStyleBackColor = true;
            this.MainOk.Click += new System.EventHandler(this.MainOk_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(675, 536);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.radioLate);
            this.Controls.Add(this.radioBack);
            this.Controls.Add(this.radioGo);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.MainOk);
            this.Controls.Add(this.empNum);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "출퇴근기록기 M-0001";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox empNum;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioGo;
        private System.Windows.Forms.RadioButton radioBack;
        private System.Windows.Forms.RadioButton radioLate;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button MainOk;
    }
}

