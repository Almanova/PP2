using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangingCarSpeed
{
    public enum Direction
    {
        Right,
        Left
    }

    public enum Speed
    {
        Zero,
        Average,
        Fast
    }

    public partial class Form1 : Form
    {
        int x = 10;
        int y = 100;
        Direction direction = Direction.Right;
        Speed speed = Speed.Zero;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(Color.Magenta), x, y, 80, 30);
            e.Graphics.FillRectangle(new SolidBrush(Color.Magenta), x + 20, y - 25, 40, 25);
            e.Graphics.FillEllipse(new SolidBrush(Color.Black), x + 15, y + 30, 20, 20);
            e.Graphics.FillEllipse(new SolidBrush(Color.Black), x + 45, y + 30, 20, 20);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (direction == Direction.Right)
            {
                x += 5;
                if (x > Width)
                    x = 0;
            }
            else
            {
                x -= 5;
                if (x < 0)
                    x = Width - 80;
            }
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            timer1.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Interval = 50;
            timer1.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (direction == Direction.Right)
                direction = Direction.Left;
            else
                direction = Direction.Right;
        }
    }
}
