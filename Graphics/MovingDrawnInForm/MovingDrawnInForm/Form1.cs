using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovingDrawnInForm
{
    public partial class Form1 : Form
    {
        int x, y;
        SolidBrush solid = new SolidBrush(Color.Magenta);

        public Form1()
        {
            InitializeComponent();
            x = 50;
            y = 100;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Point[] points = new Point[10]
            {
                new Point(x, y),
                new Point(x + 30, y),
                new Point(x + 40, y - 30),
                new Point(x + 50, y),
                new Point(x + 80, y),
                new Point(x + 60, y + 20),
                new Point(x + 70, y + 50),
                new Point(x + 40, y + 30),
                new Point(x + 20, y + 50),
                new Point(x + 25, y + 20)
            };

            e.Graphics.FillPolygon(solid, points);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            x += 10;
            Refresh();
        }
    }
}
