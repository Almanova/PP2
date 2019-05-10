using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SaveAnswerToFile
{
    public partial class Form1 : Form
    {
        int cnt = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int firstNum = int.Parse(textBox1.Text);
            int secondNum = int.Parse(textBox2.Text);
            if (firstNum < secondNum)
            {
                int temp = firstNum;
                firstNum = secondNum;
                secondNum = temp;
            }
            for (int i = secondNum + 1; i < firstNum; i++)
            {
                if (i % 2 == 0)
                    cnt++;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists("num.txt"))
                File.Delete("num.txt");
            File.Create("num.txt").Close();
            StreamWriter sw = new StreamWriter("num.txt");
            sw.Write(cnt);
            sw.Close();
            cnt = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("num.txt");
            string s = sr.ReadToEnd();
            sr.Close();
            if (int.Parse(s) % 2 == 0)
                button3.BackColor = Color.Green;
            else
                button3.BackColor = Color.Red;
        }
    }
}
