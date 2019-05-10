using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StrangeTraectory
{
    public partial class Form1 : Form
    {
        List<PointF> points;
        int i = 0;
        public Form1()
        {
            InitializeComponent();
            points = new List<PointF>();
            timer1.Enabled = true;
            GetCoordinates();
        }


        public void GetCoordinates()
        {
            for (double i = 0.7; i < 6; i += 0.1)
            {
                double x = i;
                double y = Math.Cos(x) * 100 + 200;
                x = x * 100 + 70;
                PointF point = new PointF((float)x, (float)y);
                points.Add(point);
            }
            for (double i = 0.7; i < 6; i += 0.1)
            {
                double x = i;
                double y = Math.Sin(x) * 100 + 200;
                x = x * 100 + 70;
                PointF point = new PointF((float)x, (float)y);
                points.Add(point);
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            i++;
            if (i >= points.Count - 1)
                timer1.Enabled = false;
            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(Color.Magenta), points[i].X, points[i].Y, 60, 60);
        }
    }
}
