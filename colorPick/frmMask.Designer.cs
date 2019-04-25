namespace colorPick
{
    partial class frmMask
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
            this.SuspendLayout();
            // 
            // frmMask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMask";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Capture Mask";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Red;
            this.Click += new System.EventHandler(this.frmMask_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMask_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmMask_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmMask_KeyPress);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmMask_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmMask_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmMask_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}