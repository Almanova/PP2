using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserCRUD
{
    public partial class CRUD : Form
    {
        Database database;

        public CRUD()
        {
            InitializeComponent();
        }

        public void DataBind()
        {
            database = new Database();
            string query = "SELECT * FROM Users";
            dataGridView1.DataSource = database.GetDataTable(query);
        }

        public void InsertData()
        {
            if (textFirstName.Text == "")
            {
                MessageBox.Show("Error");
            }
            else
            {
                string query = "INSERT INTO Users (FirstName, LastName, Gender, PhoneNumber, Email) VALUES (" +
                    "'" + textFirstName.Text + "'," +
                    "'" + textLastName.Text + "'," +
                    "'" + comboGender.Text + "'," +
                    "'" + textPhoneNumber.Text + "'," +
                    "'" + textEmail.Text + "')";

                int cnt = database.ExecuteSql(query);
                MessageBox.Show("Insert action completed");
                DataBind();
            }
        }

        public void UpdateData()
        {
            if (textFirstName.Text == "")
            {
                MessageBox.Show("Error");
            }
            else
            {
                string query = "UPDATE Users SET " +
                    "FirstName='" + textFirstName.Text + "'," +
                    "LastName='" + textLastName.Text + "'," +
                    "Gender='" + comboGender.Text + "'," +
                    "PhoneNumber='" + textPhoneNumber.Text + "'," +
                    "Email='" + textEmail.Text + "' " +
                    "WHERE UserID=" + label1.Text;

                int cnt = database.ExecuteSql(query);
                MessageBox.Show("Update action completed");
                DataBind();
            }
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            if (label1.Text == "-")
                InsertData();
            else
                UpdateData();
        }

        private void CRUD_Load(object sender, EventArgs e)
        {
            Database database = new Database();
            DataBind();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                label1.Text = row.Cells[0].Value.ToString();
                textFirstName.Text = row.Cells[1].Value.ToString();
                textLastName.Text = row.Cells[2].Value.ToString();
                comboGender.Text = row.Cells[3].Value.ToString();
                textPhoneNumber.Text = row.Cells[4].Value.ToString();
                textEmail.Text = row.Cells[5].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (label1.Text != "-")
            {
                string query = "DELETE FROM Users WHERE UserID = " + label1.Text;
                database.ExecuteSql(query);
                MessageBox.Show("Row deleted");
                DataBind();
            }
        }
    }
}
