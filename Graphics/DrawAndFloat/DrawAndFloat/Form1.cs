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

namespace DrawAndFloat
{
    public partial class Form1 : Form
    {
        Point prevPoint;
        Bitmap bitmap;
        Graphics graphics;
        Pen pen = new Pen(Color.BlueViolet, 2);
        List<int> dx;
        List<int> dy;
        PictureBox pictureBox2;
        int xx = 10;
        int yy = 10;

        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;
            dx = new List<int>();
            dy = new List<int>();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            prevPoint = e.Location;
            dx.Add(e.Location.X);
            dy.Add(e.Location.Y);
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                graphics.DrawLine(pen, prevPoint, e.Location);
                dx.Add(e.Location.X);
                dy.Add(e.Location.Y);
                prevPoint = e.Location;
                pictureBox1.Refresh();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int minX = Width;
            for (int i = 0; i < dx.Count; i++)
            {
                if (dx[i] < minX)
                    minX = dx[i];
            }
            int minY = Height;
            for (int i = 0; i < dy.Count; i++)
            {
                if (dy[i] < minY)
                    minY = dy[i];
            }
            int maxX = 0;
            for (int i = 0; i < dx.Count; i++)
            {
                if (dx[i] > maxX)
                    maxX = dx[i];
            }
            int maxY = 0;
            for (int i = 0; i < dy.Count; i++)
            {
                if (dy[i] > maxY)
                    maxY = dy[i];
            }

            RectangleF cloneRect = new RectangleF(minX, minY, maxX - minX, maxY - minY);
            PixelFormat format = bitmap.PixelFormat;
            Bitmap cloneBitmap = bitmap.Clone(cloneRect, format);

            pictureBox2 = new PictureBox();
            pictureBox2.Location = new Point(minX, minY);
            pictureBox2.Size = new Size(maxX - minX, maxY - minY);
            pictureBox2.BackColor = Color.Transparent;
            pictureBox2.Image = cloneBitmap;

            Controls.Add(pictureBox2);
            pictureBox1.Visible = false;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            /*Point location = pictureBox1.Location;
            location.X += xx;
            if (location.X + pictureBox2.Width > Width || location.X < 0)
                xx *= -1;
            pictureBox1.Location = location;*/

            Point location = pictureBox2.Location;
            location.X += xx;
            pictureBox2.Location = location;

            if (location.X + pictureBox2.Width + 30 > Width)
            {
                timer1.Enabled = false;
                timer2.Enabled = true;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Point location = pictureBox2.Location;
            location.Y += yy;
            pictureBox2.Location = location;

            if (location.Y + pictureBox2.Height + 60 > Height)
            {
                timer2.Enabled = false;
                timer3.Enabled = true;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Point location = pictureBox2.Location;
            location.X -= xx;
            pictureBox2.Location = location;

            if (location.X - 30 < 0)
            {
                timer3.Enabled = false;
                timer4.Enabled = true;
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            Point location = pictureBox2.Location;
            location.Y -= yy;
            pictureBox2.Location = location;

            if (location.Y - 30 < 0)
            {
                timer4.Enabled = false;
                timer1.Enabled = true;
            }
        }

        /*private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0, bitmap.Width, bitmap.Height);
            e.Graphics.FillEllipse(pen.Brush, 0, 0, 100, 100);
        }*/
    }
}
