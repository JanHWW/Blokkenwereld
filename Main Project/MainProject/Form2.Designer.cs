namespace WindowsFormsApp1
{
    partial class Nieuwewereld
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
            this.TrackBar6 = new System.Windows.Forms.TrackBar();
            this.TrackBar5 = new System.Windows.Forms.TrackBar();
            this.TrackBar4 = new System.Windows.Forms.TrackBar();
            this.TrackBar3 = new System.Windows.Forms.TrackBar();
            this.TrackBar2 = new System.Windows.Forms.TrackBar();
            this.TrackBar1 = new System.Windows.Forms.TrackBar();
            this.Nieuw = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.BM = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BM)).BeginInit();
            this.SuspendLayout();
            // 
            // TrackBar6
            // 
            this.TrackBar6.LargeChange = 10;
            this.TrackBar6.Location = new System.Drawing.Point(68, 235);
            this.TrackBar6.Maximum = 200;
            this.TrackBar6.Name = "TrackBar6";
            this.TrackBar6.Size = new System.Drawing.Size(212, 45);
            this.TrackBar6.TabIndex = 18;
            // 
            // TrackBar5
            // 
            this.TrackBar5.LargeChange = 10;
            this.TrackBar5.Location = new System.Drawing.Point(68, 196);
            this.TrackBar5.Maximum = 50;
            this.TrackBar5.Minimum = -20;
            this.TrackBar5.Name = "TrackBar5";
            this.TrackBar5.Size = new System.Drawing.Size(212, 45);
            this.TrackBar5.TabIndex = 17;
            // 
            // TrackBar4
            // 
            this.TrackBar4.LargeChange = 10;
            this.TrackBar4.Location = new System.Drawing.Point(68, 157);
            this.TrackBar4.Maximum = 200;
            this.TrackBar4.Name = "TrackBar4";
            this.TrackBar4.Size = new System.Drawing.Size(212, 45);
            this.TrackBar4.TabIndex = 16;
            // 
            // TrackBar3
            // 
            this.TrackBar3.LargeChange = 10;
            this.TrackBar3.Location = new System.Drawing.Point(68, 106);
            this.TrackBar3.Maximum = 200;
            this.TrackBar3.Name = "TrackBar3";
            this.TrackBar3.Size = new System.Drawing.Size(212, 45);
            this.TrackBar3.TabIndex = 15;
            // 
            // TrackBar2
            // 
            this.TrackBar2.LargeChange = 10;
            this.TrackBar2.Location = new System.Drawing.Point(68, 75);
            this.TrackBar2.Maximum = 200;
            this.TrackBar2.Name = "TrackBar2";
            this.TrackBar2.Size = new System.Drawing.Size(212, 45);
            this.TrackBar2.TabIndex = 14;
            // 
            // TrackBar1
            // 
            this.TrackBar1.LargeChange = 10;
            this.TrackBar1.Location = new System.Drawing.Point(68, 40);
            this.TrackBar1.Minimum = 1;
            this.TrackBar1.Name = "TrackBar1";
            this.TrackBar1.Size = new System.Drawing.Size(212, 45);
            this.TrackBar1.TabIndex = 13;
            this.TrackBar1.Value = 1;
            // 
            // Nieuw
            // 
            this.Nieuw.Location = new System.Drawing.Point(99, 286);
            this.Nieuw.Name = "Nieuw";
            this.Nieuw.Size = new System.Drawing.Size(75, 23);
            this.Nieuw.TabIndex = 12;
            this.Nieuw.Text = "Maken";
            this.Nieuw.UseVisualStyleBackColor = true;
            this.Nieuw.Click += new System.EventHandler(this.Nieuw_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(5, 8);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(90, 247);
            this.Label1.TabIndex = 11;
            this.Label1.Text = "Boomkans\r\n\r\n\r\nSteilheid\r\n\r\n\r\nWaterhoogte\r\n\r\n\r\nMin\r\n\r\n\r\nMax\r\n\r\n\r\nTemperatuur\r\n\r\n\r\n" +
    "Heuvelachtigheid";
            // 
            // BM
            // 
            this.BM.LargeChange = 10;
            this.BM.Location = new System.Drawing.Point(68, 8);
            this.BM.Maximum = 100;
            this.BM.Name = "BM";
            this.BM.Size = new System.Drawing.Size(212, 45);
            this.BM.TabIndex = 10;
            this.BM.TickFrequency = 2;
            // 
            // Nieuwewereld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 316);
            this.Controls.Add(this.TrackBar6);
            this.Controls.Add(this.TrackBar5);
            this.Controls.Add(this.TrackBar4);
            this.Controls.Add(this.TrackBar3);
            this.Controls.Add(this.TrackBar2);
            this.Controls.Add(this.TrackBar1);
            this.Controls.Add(this.Nieuw);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.BM);
            this.Name = "Nieuwewereld";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TrackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TrackBar TrackBar6;
        internal System.Windows.Forms.TrackBar TrackBar5;
        internal System.Windows.Forms.TrackBar TrackBar4;
        internal System.Windows.Forms.TrackBar TrackBar3;
        internal System.Windows.Forms.TrackBar TrackBar2;
        internal System.Windows.Forms.TrackBar TrackBar1;
        internal System.Windows.Forms.Button Nieuw;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TrackBar BM;
    }
}