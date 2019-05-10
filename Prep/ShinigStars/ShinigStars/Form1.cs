using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShinigStars
{
    public partial class Form1 : Form
    {
        List<Star> stars = new List<Star>();
        SolidBrush solid = new SolidBrush(Color.Yellow);

        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (Star star in stars)
            {
                solid.Color = star.color;
                e.Graphics.FillPolygon(solid, star.body);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            int x = random.Next(1, Width - 70);
            int y = random.Next(1, Height - 70);
            Star star = new Star(Color.Yellow, x, y);
            stars.Add(star);
            Refresh();
            foreach (Star s in stars)
            {
                if (s.color == Color.Yellow)
                    s.color = Color.LightYellow;
                else
                    s.color = Color.Yellow;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
    }

    public class Star
    {
        public Point[] body;
        public Color color;
        public int x, y;

        public Star(Color color, int x, int y)
        {
            this.x = x;
            this.y = y;
            body = new Point[10]
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
            this.color = color;
        }
    }
}
