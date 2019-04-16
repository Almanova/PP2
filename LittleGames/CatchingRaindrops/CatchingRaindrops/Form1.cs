using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CatchingRaindrops
{
    public partial class Form1 : Form
    {
        SolidBrush solid = new SolidBrush(Color.Blue);
        List<Raindrop> raindrops;
        Color[] colors = new Color[] { Color.Blue, Color.LightSkyBlue, Color.RoyalBlue, Color.Black };
        int score = 0;

        public Form1()
        {
            InitializeComponent();
            raindrops = new List<Raindrop>();
            timer1.Interval = 4000;
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            string pressed_btn = e.KeyCode + " ";
            Point location = button1.Location;
            if (pressed_btn == "D " && location.X + button1.Width + 10 < Width)
                location.X += 10;
            else if (pressed_btn == "A " && location.X > 0)
                location.X -= 10;

            button1.Location = location;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(solid, 0, 0, 30, 30);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Random random = new Random();
            PictureBox pic = new PictureBox();
            solid.Color = colors[random.Next(0, colors.Length)];
            pic.Location = new Point(random.Next(0, Width - 40), 0);
            pic.Size = new Size(30, 30);
            pic.Paint += new PaintEventHandler(pictureBox_Paint);
            Controls.Add(pic);
            Raindrop raindrop = new Raindrop(pic, solid.Color);
            raindrops.Add(raindrop);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            foreach (Raindrop r in raindrops)
            {
                Point location = r.pic.Location;
                location.Y += 5;
                r.pic.Location = location;
                if (IsCollision(r))
                {
                    r.pic.Visible = false;

                    if (r.rainColor == Color.Black)
                    {
                        timer1.Enabled = false;
                        Label label = new Label();
                        label.Location = new Point(Width / 2 - 45, 160);
                        label.Text = "Game Over!";
                        Controls.Add(label);
                    }
                    else
                    {
                        score += 1;
                        textBox1.Text = string.Format("Your Score: {0}", score);
                    }
                }
            }
        }

        private bool IsCollision(Raindrop raindrop)
        {
            if (raindrop.pic.Location.X > button1.Location.X && raindrop.pic.Location.X + 30 < button1.Location.X + button1.Width && raindrop.pic.Location.Y + 30 == button1.Location.Y)
                return true;
            return false;
        }
    }

    public class Raindrop
    {
        public PictureBox pic;
        public Color rainColor;

        public Raindrop(PictureBox pic, Color rainColor)
        {
            this.pic = pic;
            this.rainColor = rainColor;
        }
    }
}
