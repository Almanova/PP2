using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrowingStar
{
    public partial class Form1 : Form
    {
        int x = 100;
        int y = 100;
        int i = 1;
        SolidBrush solid = new SolidBrush(Color.Yellow);

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 100;
            timer1.Enabled = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            /*Point[] points = new Point[10]
            {
                new Point(x + i, y + i),
                new Point((x + 30) + i, y + i),
                new Point((x + 40) + i, (y - 30) + i),
                new Point((x + 50) + i, y + i),
                new Point((x + 80) + i, y + i),
                new Point((x + 60) + i, (y + 20) + i),
                new Point((x + 70) - i, (y + 50) - i),
                new Point((x + 40) - i, (y + 30) - i),
                new Point((x + 20) - i, (y + 50) - i),
                new Point((x + 25) - i, (y + 20) - i)
            };*/

            e.Graphics.FillRectangle(solid, x, y, i, i);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            i += 5;
            if (i == 101)
            {
                Random random = new Random();
                x = random.Next(50, Width - 50);
                y = random.Next(50, Height - 50);
                i = 1;
            }
            Refresh();
        }
    }
}
