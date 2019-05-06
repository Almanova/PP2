using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Intranet
{
    enum FilterBy
    {
        StudentID,
        FirstName,
        LastName
    }

    public partial class Students : Form
    {
        Database database = new Database();
        FilterBy filter = FilterBy.LastName;
        string StudentID = "";

        public Students()
        {
            InitializeComponent();
        }

        public void DataBind(string condition)
        {
            database = new Database();
            string query = "SELECT * FROM Students";
            if (condition != "")
            {
                switch (filter)
                {
                    case FilterBy.StudentID:
                        query += " WHERE StudentID LIKE '%" + condition + "%'";
                        break;
                    case FilterBy.FirstName:
                        query += " WHERE FirstName LIKE '%" + condition + "%'";
                        break;
                    case FilterBy.LastName:
                        query += " WHERE LastName LIKE '%" + condition + "%'";
                        break;
                }
            }
            dataGridView1.DataSource = database.GetDataTable(query, 0);
        }

        private void Students_Load(object sender, EventArgs e)
        {
            DataBind("");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                int columnIndex = cell.ColumnIndex;
                int rowIndex = cell.RowIndex;
                if (columnIndex == 0)
                    filter = FilterBy.StudentID;
                else if (columnIndex == 1)
                    filter = FilterBy.FirstName;
                else if (columnIndex == 2)
                    filter = FilterBy.LastName;
                StudentID = dataGridView1[0, rowIndex].Value.ToString();
                dataGridView1.CurrentCell = dataGridView1[columnIndex, rowIndex];
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataBind(textBox1.Text);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentCell.RowIndex;
            StudentID = dataGridView1[0, i].Value.ToString();
            UpdateData(StudentID);
        }

        public void UpdateData(string StudentID)
        {
            string query = "UPDATE Students SET ";
            switch (filter)
            {
                case FilterBy.FirstName:
                    query += "FirstName = '" + dataGridView1.CurrentCell.Value.ToString() +  "'" +
                        " WHERE StudentID = " + StudentID;
                    break;
                case FilterBy.LastName:
                    query += "LastName = '" + dataGridView1.CurrentCell.Value.ToString() + "'" +
                        " WHERE StudentID = " + StudentID;
                    break;
            }
            database.ExecuteSQL(query);
            MessageBox.Show("Data has been successfully updated");
            DataBind("");
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            InsertStudent insertStudent = new InsertStudent();
            insertStudent.ShowDialog();
        }

        public void DeleteStudent()
        {
            string query = "DELETE FROM Students WHERE StudentID = " + StudentID;
            database.ExecuteSQL(query);
            MessageBox.Show("Student has been deleted");
            DataBind("");
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteStudent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataBind("");
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddSubjects addSubjects = new AddSubjects(StudentID, dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString(),
                dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString());
            addSubjects.ShowDialog();
        }

        private void buttonView_Click(object sender, EventArgs e)
        {
            ViewSubjects viewSubjects = new ViewSubjects(StudentID, dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString(), 
                dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString());
            viewSubjects.ShowDialog();
        }
    }
}
