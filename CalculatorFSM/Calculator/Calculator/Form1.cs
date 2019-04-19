using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        Brain brain;
        TextBox MainTextBox = new TextBox();
        TextBox RecurringTextBox = new TextBox();
        ComboBox comboBox = new ComboBox();

        public Form1()
        {
            InitializeComponent();
            brain = new Brain(new ChangeTextDelegate(ChangeText), new ChangeTextDelegate(ChangeRecurringText));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Label label = new Label();
            label.Size = new Size(140, 40);
            label.Font = new Font("Adagio script", 23F, FontStyle.Bold, GraphicsUnit.Point, 204);
            label.Text = "Standard";
            Controls.Add(label);

            RecurringTextBox.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Regular, GraphicsUnit.Point, 204);
            RecurringTextBox.ForeColor = SystemColors.GrayText;
            RecurringTextBox.BackColor = SystemColors.ActiveBorder;
            RecurringTextBox.BorderStyle = BorderStyle.None;
            RecurringTextBox.Location = new Point(12, 40);
            RecurringTextBox.Size = new Size(380, 26);
            RecurringTextBox.TextAlign = HorizontalAlignment.Right;
            RecurringTextBox.Enabled = false;
            Controls.Add(RecurringTextBox);

            MainTextBox.Font = new Font("Microsoft Sans Serif", 30F, FontStyle.Regular, GraphicsUnit.Point, 204);
            MainTextBox.BackColor = SystemColors.ActiveBorder;
            MainTextBox.BorderStyle = BorderStyle.None;
            MainTextBox.Location = new Point(12, 72);
            MainTextBox.Size = new Size(380, 55);
            MainTextBox.TextAlign = HorizontalAlignment.Right;
            MainTextBox.ForeColor = Color.FromArgb(0, 0, 0);
            MainTextBox.Text = "0";
            Controls.Add(MainTextBox);

            int x = MainTextBox.Location.X - 10;
            int y = MainTextBox.Location.Y + MainTextBox.Height + 22;

            comboBox.Location = new Point(52 * 5 + x, y + 6);
            comboBox.Size = new Size(52, 30);
            comboBox.Text = "M";
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.Items.AddRange(new object[] { });
            Controls.Add(comboBox);

            string[] MemoryOperations = { "MC", "MR", "M+", "M-", "MS" };

            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button();
                btn.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, 204);
                btn.Text = MemoryOperations[i];
                btn.Size = new Size(52, 30);
                btn.BackColor = SystemColors.ActiveBorder;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 91, 91);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(137, 88, 142);
                btn.Location = new Point(i * 52 + x, y);
                btn.Click += new EventHandler(BtnClick);
                Controls.Add(btn);
            }

            string[] SpecialOperations = { "%", "√", "x^2", "1/x" };
       
            y = MainTextBox.Location.Y + MainTextBox.Height + 52;

            for (int i = 0; i < 4; i++)
            {
                Button btn = new Button();
                btn.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
                btn.Text = SpecialOperations[i];
                btn.Size = new Size(79, 55);
                btn.BackColor = Color.FromArgb(219, 219, 219);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 91, 91);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(137, 88, 142);
                btn.Location = new Point(i * 79 + x, y);
                btn.Click += new EventHandler(BtnClick);
                Controls.Add(btn);
            }

            string[] SettingButtons = { "CE", "C", "<<" };

            y = MainTextBox.Location.Y + MainTextBox.Height + 107;

            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button();
                btn.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
                btn.Text = SettingButtons[i];
                btn.Size = new Size(79, 55);
                btn.BackColor = Color.FromArgb(219, 219, 219);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 91, 91);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(137, 88, 142);
                btn.Location = new Point(i * 79 + x, y);
                btn.Click += new EventHandler(BtnClick);
                Controls.Add(btn);
            }

            string[] Operations = { "÷", "x", "-", "+", "="};

            for (int i = 0; i < 5; i++)
            {
                Button btn = new Button();
                btn.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
                btn.Text = Operations[i];
                btn.Size = new Size(79, 55);
                btn.BackColor = Color.FromArgb(219, 219, 219);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(137, 88, 142);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(91, 91, 91);
                btn.Location = new Point(237 + x, i * 55 + y);
                btn.Click += new EventHandler(BtnClick);
                Controls.Add(btn);
            }

            y = MainTextBox.Location.Y + MainTextBox.Height + 162;

            int num = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    num++;
                    Button btn = new Button();
                    btn.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
                    btn.Text = num.ToString();
                    btn.Size = new Size(79, 55);
                    btn.BackColor = Color.FromArgb(145, 145, 145);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 91, 91);
                    btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(137, 88, 142);
                    btn.Location = new Point(j * 79 + x, i * 55 + y);
                    btn.Click += new EventHandler(BtnClick);
                    Controls.Add(btn);
                }
            }

            string[] LastRow = { "+-", "0", "," };

            y = MainTextBox.Location.Y + MainTextBox.Height + 327;

            for (int i = 0; i < 3; i++)
            {
                Button btn = new Button();
                btn.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
                btn.Text = LastRow[i];
                btn.Size = new Size(79, 55);
                if (i == 1)
                    btn.BackColor = Color.FromArgb(145, 145, 145);
                else
                    btn.BackColor = Color.FromArgb(219, 219, 219);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 91, 91);
                btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(137, 88, 142);
                btn.Location = new Point(i * 79 + x, y);
                btn.Click += new EventHandler(BtnClick);
                Controls.Add(btn);
            }

            y = MainTextBox.Location.Y + MainTextBox.Height + 382;

            Button bttn = new Button();
            bttn.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
            bttn.Text = "sin";
            bttn.Size = new Size(79, 55);
            bttn.BackColor = Color.FromArgb(219, 219, 219);
            bttn.FlatStyle = FlatStyle.Flat;
            bttn.FlatAppearance.BorderSize = 1;
            bttn.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            bttn.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 91, 91);
            bttn.FlatAppearance.MouseDownBackColor = Color.FromArgb(137, 88, 142);
            bttn.Location = new Point(x, y);
            bttn.Click += new EventHandler(BtnClick);
            Controls.Add(bttn);

            Button button = new Button();
            button.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button.Text = "cos";
            button.Size = new Size(79, 55);
            button.BackColor = Color.FromArgb(219, 219, 219);
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 91, 91);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(137, 88, 142);
            button.Location = new Point(x + 79, y);
            button.Click += new EventHandler(BtnClick);
            Controls.Add(button);

            Button button2 = new Button();
            button2.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button2.Text = "Mod";
            button2.Size = new Size(79, 55);
            button2.BackColor = Color.FromArgb(219, 219, 219);
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 1;
            button2.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 91, 91);
            button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(137, 88, 142);
            button2.Location = new Point(x + 79 * 2, y);
            button2.Click += new EventHandler(BtnClick);
            Controls.Add(button2);

            Button button3 = new Button();
            button3.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button3.Text = "log";
            button3.Size = new Size(79, 55);
            button3.BackColor = Color.FromArgb(219, 219, 219);
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 1;
            button3.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 91, 91);
            button3.FlatAppearance.MouseDownBackColor = Color.FromArgb(137, 88, 142);
            button3.Location = new Point(x + 79 * 3, y);
            button3.Click += new EventHandler(BtnClick);
            Controls.Add(button3);

            y = MainTextBox.Location.Y + MainTextBox.Height + 52;

            Button button4 = new Button();
            button4.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button4.Text = "x^y";
            button4.Size = new Size(79, 55);
            button4.BackColor = Color.FromArgb(219, 219, 219);
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 1;
            button4.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            button4.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 91, 91);
            button4.FlatAppearance.MouseDownBackColor = Color.FromArgb(137, 88, 142);
            button4.Location = new Point(x + 79 * 4, y);
            button4.Click += new EventHandler(BtnClick);
            Controls.Add(button4);

            Button button5 = new Button();
            button5.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button5.Text = "10^x";
            button5.Size = new Size(79, 55);
            button5.BackColor = Color.FromArgb(219, 219, 219);
            button5.FlatStyle = FlatStyle.Flat;
            button5.FlatAppearance.BorderSize = 1;
            button5.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            button5.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 91, 91);
            button5.FlatAppearance.MouseDownBackColor = Color.FromArgb(137, 88, 142);
            button5.Location = new Point(x + 79 * 4, y + 55);
            button5.Click += new EventHandler(BtnClick);
            Controls.Add(button5);

            Button button6 = new Button();
            button6.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button6.Text = "n!";
            button6.Size = new Size(79, 55);
            button6.BackColor = Color.FromArgb(219, 219, 219);
            button6.FlatStyle = FlatStyle.Flat;
            button6.FlatAppearance.BorderSize = 1;
            button6.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            button6.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 91, 91);
            button6.FlatAppearance.MouseDownBackColor = Color.FromArgb(137, 88, 142);
            button6.Location = new Point(x + 79 * 4, y + 55 * 2);
            button6.Click += new EventHandler(BtnClick);
            Controls.Add(button6);

            Button button7 = new Button();
            button7.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button7.Text = "yrootx";
            button7.Size = new Size(79, 55);
            button7.BackColor = Color.FromArgb(219, 219, 219);
            button7.FlatStyle = FlatStyle.Flat;
            button7.FlatAppearance.BorderSize = 1;
            button7.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            button7.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 91, 91);
            button7.FlatAppearance.MouseDownBackColor = Color.FromArgb(137, 88, 142);
            button7.Location = new Point(x + 79 * 4, y + 55 * 3);
            button7.Click += new EventHandler(BtnClick);
            Controls.Add(button7);

            Button button8 = new Button();
            button8.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Point, 204);
            button8.Text = " ";
            button8.Size = new Size(79, 55);
            button8.BackColor = Color.FromArgb(219, 219, 219);
            button8.FlatStyle = FlatStyle.Flat;
            button8.FlatAppearance.BorderSize = 1;
            button8.FlatAppearance.BorderColor = SystemColors.ActiveBorder;
            button8.FlatAppearance.MouseOverBackColor = Color.FromArgb(91, 91, 91);
            button8.FlatAppearance.MouseDownBackColor = Color.FromArgb(137, 88, 142);
            button8.Location = new Point(x + 79 * 4, y + 55 * 4);
            button8.Click += new EventHandler(BtnClick);
            Controls.Add(button8);
        }

        private void ChangeText(string text)
        {
            MainTextBox.Text = text;   
        }

        private void ChangeRecurringText(string text)
        {
            RecurringTextBox.Text = text;
        }

        private void ChangeComboBox(object[] items)
        {
            comboBox.Items.AddRange(items);
        }

        private void BtnClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            brain.Process(button.Text);
        }
    }
}
