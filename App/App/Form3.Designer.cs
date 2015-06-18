namespace App
{
    partial class CHECKADMIN
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checktextbox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.checktextbox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // checktextbox1
            // 
            this.checktextbox1.Location = new System.Drawing.Point(88, 163);
            this.checktextbox1.Name = "checktextbox1";
            this.checktextbox1.Size = new System.Drawing.Size(166, 21);
            this.checktextbox1.TabIndex = 0;
            this.checktextbox1.Text = "admin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 166);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(16, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(288, 166);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "확인";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
      
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::App.Properties.Resources.one;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(373, 121);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // checktextbox2
            // 
            this.checktextbox2.Location = new System.Drawing.Point(88, 187);
            this.checktextbox2.Name = "checktextbox2";
            this.checktextbox2.PasswordChar = '*';
            this.checktextbox2.Size = new System.Drawing.Size(166, 21);
            this.checktextbox2.TabIndex = 4;
            this.checktextbox2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.checktextbox2_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "PASSWORD";
            // 
            // CHECKADMIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 220);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.checktextbox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checktextbox1);
            this.Name = "CHECKADMIN";
           
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox checktextbox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox checktextbox2;
        private System.Windows.Forms.Label label2;
    }
}