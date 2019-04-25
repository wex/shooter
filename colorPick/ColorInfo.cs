using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace colorPick
{
    public partial class ColorInfo : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // turn on WS_EX_TOOLWINDOW style bit
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        public ColorInfo()
        {
            InitializeComponent();

            Bitmap icon = Properties.Resources.icon_pipette;
            Icon = Icon.FromHandle(icon.GetHicon());
        }

        internal void SetColor(Color px)
        {
            r.Text = $"{px.R}, {px.G}, {px.B}";
            h.Text = "#" + px.R.ToString("X2") + px.G.ToString("X2") + px.B.ToString("X2");
            c.BackColor = px;
        }

        private void ColorInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
