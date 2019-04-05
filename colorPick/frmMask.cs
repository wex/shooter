using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace colorPick
{
    public partial class frmMask : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr window);
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern uint GetPixel(IntPtr dc, int x, int y);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int ReleaseDC(IntPtr window, IntPtr dc);

        private bool isDrawing = false;
        private bool maybeDraw = false;

        private Point drawStart;
        private Point drawEnd;

        private Rectangle shotRect;

        public event EventHandler<Color> ColorSelected;
        public event EventHandler<Bitmap> ShotSelected;

        public static Color GetColorAt(int x, int y)
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr dc = GetWindowDC(desk);
            int a = (int)GetPixel(dc, x, y);
            ReleaseDC(desk, dc);
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }

        public frmMask()
        {
            InitializeComponent();
        }

        private void frmMask_Click(object sender, EventArgs e)
        {
        }

        private void frmMask_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = this.DisplayRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            e.Graphics.DrawRectangle(new Pen(Color.Yellow, 1), rect);

            if (isDrawing)
            {
                e.Graphics.DrawRectangle(new Pen(Color.Blue, 1), ShotArea(drawStart, drawEnd));
            }
        }

        private Rectangle ShotArea(Point a, Point b)
        {
            var r = new Rectangle
            {
                X = (b.X > a.X) ? a.X : b.X,
                Y = (b.Y > a.Y) ? a.Y : b.Y,
                Width = Math.Abs(b.X - a.X),
                Height = Math.Abs(b.Y - a.Y)
            };

            return r;
        }

        private bool IsDraw(Point a, Point b)
        {
            if ((Math.Abs(a.X - b.X) > 2) && (Math.Abs(a.Y - b.Y) > 2))
                return true;

            return false;
        }

        private int ShotSize(Rectangle s)
        {
            return s.Width * s.Height;
        }

        private void frmMask_MouseDown(object sender, MouseEventArgs e)
        {
            maybeDraw = true;   
            isDrawing = false;
            drawStart = e.Location;
        }

        private void frmMask_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                Console.WriteLine("Draw ready");

                drawEnd = e.Location;
                isDrawing = false;
                Refresh();
                shotRect = ShotArea(drawStart, drawEnd);

                // Take screenshot
                Bitmap shot = new Bitmap(shotRect.Width, shotRect.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(shot);
                g.CopyFromScreen(shotRect.X, shotRect.Y, 0, 0, shot.Size, CopyPixelOperation.SourceCopy);

                ShotSelected?.Invoke(this, shot);
            }

            if (maybeDraw)
            {
                maybeDraw = false;
                isDrawing = false;
                Refresh();

                Screen screen = Screen.FromPoint(Cursor.Position);
                Color pixel;
                int x = Cursor.Position.X;
                int y = Cursor.Position.Y;

                pixel = GetColorAt(x, y);
                ColorSelected?.Invoke(this, pixel);
            }
        }

        private void frmMask_MouseHover(object sender, EventArgs e)
        {

        }

        private void frmMask_MouseMove(object sender, MouseEventArgs e)
        {
            if (maybeDraw)
            {
                drawEnd = e.Location;
                if (IsDraw(drawStart, drawEnd))
                {
                    isDrawing = true;
                    maybeDraw = false;
                }
            }
            if (isDrawing)
            {
                Refresh();
                drawEnd = e.Location;
            }
        }
    }
}
