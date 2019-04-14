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
            RecurringTextBox.Size = new Size(296, 26);
            RecurringTextBox.TextAlign = HorizontalAlignment.Right;
            RecurringTextBox.Enabled = false;
            Controls.Add(RecurringTextBox);

            MainTextBox.Font = new Font("Microsoft Sans Serif", 30F, FontStyle.Regular, GraphicsUnit.Point, 204);
            MainTextBox.BackColor = SystemColors.ActiveBorder;
            MainTextBox.BorderStyle = BorderStyle.None;
            MainTextBox.Location = new Point(12, 72);
            MainTextBox.Size = new Size(296, 55);
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
