using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.IO;

namespace colorPick
{
    public partial class frmPicker : Form
    {
        private Screen screen;
        private List<frmMask> masks = new List<frmMask>();

        private Rectangle workspace = new Rectangle(0, 0, 0, 0);

        private void resizeMask()
        {
            screen = Screen.FromControl(this);
        }

        public frmPicker()
        {
            InitializeComponent();

            foreach (Screen t in Screen.AllScreens)
            {
                Console.WriteLine(t.WorkingArea.Location);
                var mask = new frmMask
                {
                    Left = t.WorkingArea.X,
                    Top = t.WorkingArea.Y,
                    Width = t.Bounds.Width,
                    Height = t.Bounds.Height
                };
                
                mask.ColorSelected += Mask_ColorSelected;
                mask.ShotSelected += Mask_ShotSelected;
                mask.Show();
                mask.Visible = false;

                masks.Add(mask);
            }
        }

        private void CleanUp()
        {
            foreach (frmMask mask in masks)
            {
                mask.Visible = false;
            }

            Application.Exit();
        }

        private void Mask_ShotSelected(object sender, Bitmap e)
        {
            if ((ModifierKeys  & Keys.Shift) == Keys.Shift)
            {
                dlgSave.ShowDialog();
                if (dlgSave.FileName.Length > 0)
                {
                    e.Save(dlgSave.FileName, ImageFormat.Png);
                }
            } else
            {
                Clipboard.SetImage(e);
            }

            CleanUp();
        }

        private void Mask_ColorSelected(object sender, Color e)
        {
            Clipboard.SetText("#" + e.R.ToString("X2") + e.G.ToString("X2") + e.B.ToString("X2"));

            CleanUp();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void frmPicker_Move(object sender, EventArgs e)
        {

        }

        private void frmPicker_Load(object sender, EventArgs e)
        {
            foreach (frmMask mask in masks)
            {
                mask.Visible = true;
            }
        }

        private void frmPicker_Resize(object sender, EventArgs e)
        {

        }
    }
}
