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

        static int FULL_ZOOM = 128;
        static int HALF_ZOOM = FULL_ZOOM / 2;
        static int ZOOM_MAGN = 4;

        private bool isZooming = false;

        private bool isDrawing = false;
        private bool maybeDraw = false;

        private Bitmap zoom = new Bitmap(32, 32, PixelFormat.Format32bppArgb);
        private Bitmap zoomed = new Bitmap(128, 128, PixelFormat.Format32bppArgb);
        private Graphics gZoom;

        private ColorInfo colorInfo;

        private Point drawStart;
        private Point drawEnd;

        private Point localStart;
        private Point localEnd;

        private Pen maskPen = new Pen(Color.Yellow, 1);

        private Color px;

        internal void AddInfo(ref ColorInfo colorInfo)
        {
            this.colorInfo = colorInfo;
        }

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

            Bitmap icon = Properties.Resources.icon_mask;
            Icon = Icon.FromHandle(icon.GetHicon());

            gZoom = Graphics.FromImage(zoom);
            gZoom.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            gZoom.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            gZoom.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        }

        private void frmMask_Click(object sender, EventArgs e)
        {
        }

        private void frmMask_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = this.DisplayRectangle;
            rect.Width -= 1;
            rect.Height -= 1;

            e.Graphics.DrawRectangle(maskPen, rect);

            if (isZooming)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

                gZoom.CopyFromScreen(drawEnd.X - 16, drawEnd.Y - 16, 0, 0, zoom.Size, CopyPixelOperation.SourceCopy);
                zoomed = new Bitmap(zoom, 160, 160);

                e.Graphics.DrawImage(zoomed, ZoomPoint(drawStart, drawEnd));
                e.Graphics.DrawRectangle(new Pen(Color.Black, 1), new Rectangle(ZoomPoint(drawStart, drawEnd), zoomed.Size));
            }

            if (isDrawing)
            {
                e.Graphics.DrawRectangle(new Pen(Color.Blue, 1), ShotArea(localStart, localEnd));
            }

        }

        private Point ZoomPoint(Point a, Point b)
        {
            Point r = new Point();
            r.X = (a.X < b.X) ? (b.X + 10) : (b.X - 10 - FULL_ZOOM);
            r.Y = (a.Y < b.Y) ? (b.Y + 10) : (b.Y - 10 - FULL_ZOOM);

            return r;
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
            drawStart = Cursor.Position;
            localStart = e.Location;
        }

        private void frmMask_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                drawEnd = Cursor.Position;
                localEnd = e.Location;
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

        private void frmMask_MouseMove(object sender, MouseEventArgs e)
        {
            isZooming = ((ModifierKeys & Keys.Alt) == Keys.Alt);

            if (!maybeDraw && !isDrawing)
            {
                colorInfo.Left = Cursor.Position.X + 10;
                colorInfo.Top = Cursor.Position.Y + 10;
                px = GetColorAt(Cursor.Position.X, Cursor.Position.Y);
                colorInfo.SetColor(px);
                colorInfo.Refresh();
            }

            if (maybeDraw)
            {
                drawEnd = Cursor.Position;
                localEnd = e.Location;
                if (IsDraw(drawStart, drawEnd))
                {
                    isDrawing = true;
                    maybeDraw = false;
                    colorInfo.Visible = false;
                    maskPen = new Pen(Color.Green, 2);
                }
            }

            if (isDrawing)
            {
                Refresh();
                drawEnd = Cursor.Position;
                localEnd = e.Location;
            }
        }

        private void frmMask_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void frmMask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit();
            }
        }
    }
}
