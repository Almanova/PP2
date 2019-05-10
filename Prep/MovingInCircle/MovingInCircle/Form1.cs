using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovingInCircle
{
    public partial class Form1 : Form
    {
        List<PointF> circles;
        List<double> x = new List<double>();
        public Form1()
        {
            InitializeComponent();
            circles = new List<PointF>();
            timer1.Interval = 3000;
            timer2.Interval = 100;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            PointF point = new PointF(50, 200);
            circles.Add(point);
            x.Add(0);
            timer2.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < x.Count; i++)
            {
                PointF location = circles[i];
                location.X += (float)(Math.Cos(x[i]) * 5);
                location.Y += (float)(Math.Sin(x[i]) * 5);
                circles[i] = location;
                x[i] += 0.1;
            }
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach(PointF p in circles)
            {
                e.Graphics.FillEllipse(new SolidBrush(Color.Magenta), p.X, p.Y, 30, 30);
            }
        }
    }
}
