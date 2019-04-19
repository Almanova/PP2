using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrawingHouseOnItsOwn
{
    public partial class Form1 : Form
    {
        int i = 0;
        int x;
        int y;
        Graphics graphics;
        Bitmap bitmap;
        Point prevPoint;
        Point curPoint;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;
            x = Width / 2;
            y = 50;
            prevPoint = new Point(Width / 2, 50);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            x += 5;
            y += 5;
            curPoint = new Point(x, y);
            graphics.DrawLine(new Pen(Color.Magenta, 2), prevPoint, curPoint);
            prevPoint = curPoint;
            pictureBox1.Refresh();

            if (i == 40)
            {
                timer1.Enabled = false;
                timer2.Enabled = true;
            }

            i++;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            y += 5;
            curPoint = new Point(x, y);
            graphics.DrawLine(new Pen(Color.Magenta, 2), prevPoint, curPoint);
            prevPoint = curPoint;
            pictureBox1.Refresh();

            if (i == 70)
            {
                timer2.Enabled = false;
                timer3.Enabled = true;
            }

            i++;
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            x -= 5;
            curPoint = new Point(x, y);
            graphics.DrawLine(new Pen(Color.Magenta, 2), prevPoint, curPoint);
            prevPoint = curPoint;
            pictureBox1.Refresh();

            if (i == 130)
            {
                timer3.Enabled = false;
                timer4.Enabled = true;
            }

            i++;
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            y -= 5;
            curPoint = new Point(x, y);
            graphics.DrawLine(new Pen(Color.Magenta, 2), prevPoint, curPoint);
            prevPoint = curPoint;
            pictureBox1.Refresh();

            if (i == 160)
            {
                timer4.Enabled = false;
                timer5.Enabled = true;
            }

            i++;
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            y -= 5;
            x += 5;
            curPoint = new Point(x, y);
            graphics.DrawLine(new Pen(Color.Magenta, 2), prevPoint, curPoint);
            prevPoint = curPoint;
            pictureBox1.Refresh();

            if (i == 200)
                timer5.Enabled = false;

            i++;
        }
    }
}
