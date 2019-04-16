using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShootingRaindrops
{
    public partial class Form1 : Form
    {
        SolidBrush solid = new SolidBrush(Color.Blue);
        SolidBrush solid2 = new SolidBrush(Color.Orange);
        List<Raindrop> raindrops;
        List<PictureBox> bullets;
        Color[] colors = new Color[] { Color.LightSkyBlue, Color.Bisque, Color.DarkTurquoise, Color.LimeGreen, Color.Orchid };

        public Form1()
        {
            InitializeComponent();
            raindrops = new List<Raindrop>();
            bullets = new List<PictureBox>();
            timer1.Interval = 3000;
            timer3.Enabled = false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            Point location = button1.Location;
            location.X = e.Location.X - button1.Width / 2;
            button1.Location = location;
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(solid, 0, 0, 30, 30);
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(solid2, 0, 0, 10, 10);
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
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            PictureBox picture = new PictureBox();
            picture.Location = new Point(button1.Location.X + button1.Width / 2, button1.Location.Y + 10);
            picture.Size = new Size(10, 10);
            picture.Paint += new PaintEventHandler(pictureBox2_Paint);
            Controls.Add(picture);
            bullets.Add(picture);
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                Point location = bullets[i].Location;
                location.Y -= 3;
                bullets[i].Location = location;

                for (int j = 0; j < raindrops.Count; j++)
                {
                    if (IsCollision(bullets[i], raindrops[j]))
                        raindrops[j].pic.Visible = false;
                }
            }
        }

        private bool IsCollision(PictureBox b, Raindrop r)
        {
            if (b.Location.X > r.pic.Location.X && b.Location.X + 10 < r.pic.Location.X + r.pic.Width && b.Location.Y - 10 < r.pic.Location.Y)
                return true;
            return false;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox picture = new PictureBox();
            picture.Location = new Point(button1.Location.X + button1.Width / 2, button1.Location.Y + 10);
            picture.Size = new Size(10, 10);
            picture.Paint += new PaintEventHandler(pictureBox2_Paint);
            Controls.Add(picture);
            bullets.Add(picture);
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
