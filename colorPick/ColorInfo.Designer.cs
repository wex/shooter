namespace colorPick
{
    partial class ColorInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.r = new System.Windows.Forms.Label();
            this.h = new System.Windows.Forms.Label();
            this.c = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.c)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "RGB";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "HEX";
            // 
            // r
            // 
            this.r.AutoSize = true;
            this.r.Location = new System.Drawing.Point(48, 9);
            this.r.Name = "r";
            this.r.Size = new System.Drawing.Size(73, 13);
            this.r.TabIndex = 2;
            this.r.Text = "255, 255, 255";
            // 
            // h
            // 
            this.h.AutoSize = true;
            this.h.Location = new System.Drawing.Point(48, 22);
            this.h.Name = "h";
            this.h.Size = new System.Drawing.Size(50, 13);
            this.h.TabIndex = 3;
            this.h.Text = "#FFFFFF";
            // 
            // c
            // 
            this.c.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.c.Location = new System.Drawing.Point(127, 9);
            this.c.Name = "c";
            this.c.Size = new System.Drawing.Size(26, 26);
            this.c.TabIndex = 4;
            this.c.TabStop = false;
            // 
            // ColorInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(168, 46);
            this.Controls.Add(this.c);
            this.Controls.Add(this.h);
            this.Controls.Add(this.r);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ColorInfo";
            this.ShowInTaskbar = false;
            this.Text = "Color Info";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ColorInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.c)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label r;
        private System.Windows.Forms.Label h;
        private System.Windows.Forms.PictureBox c;
    }
}