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
    enum FilterSubjectsBy
    {
        SubjectID,
        SubjectName,
        Teacher,
        Room
    }

    public partial class Subjects : Form
    {
        Database database = new Database();
        FilterSubjectsBy filterSubjects = FilterSubjectsBy.SubjectName;
        string SubjectID = "";

        public Subjects()
        {
            InitializeComponent();
        }

        public void DataBind(string condition)
        {
            database = new Database();
            string query = "SELECT * FROM Subjects";
            if (condition != "")
            {
                switch (filterSubjects)
                {
                    case FilterSubjectsBy.SubjectID:
                        query += " WHERE SubjectID LIKE '%" + condition + "%'";
                        break;
                    case FilterSubjectsBy.SubjectName:
                        query += " WHERE SubjectName LIKE '%" + condition + "%'";
                        break;
                    case FilterSubjectsBy.Teacher:
                        query += " WHERE Teacher LIKE '%" + condition + "%'";
                        break;
                    case FilterSubjectsBy.Room:
                        query += " WHERE Room LIKE '%" + condition + "%'";
                        break;
                }
            }
            dataGridView1.DataSource = database.GetDataTable(query, 0);
        }

        private void Subjects_Load(object sender, EventArgs e)
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
                    filterSubjects = FilterSubjectsBy.SubjectID;
                else if (columnIndex == 1)
                    filterSubjects = FilterSubjectsBy.SubjectName;
                else if (columnIndex == 2)
                    filterSubjects = FilterSubjectsBy.Teacher;
                else if (columnIndex == 3)
                    filterSubjects = FilterSubjectsBy.Room;
                SubjectID = dataGridView1[0, rowIndex].Value.ToString();
                dataGridView1.CurrentCell = dataGridView1[columnIndex, rowIndex];
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.CurrentCell.RowIndex;
            SubjectID = dataGridView1[0, i].Value.ToString();
            UpdateData(SubjectID);
        }

        public void UpdateData(string StudentID)
        {
            string query = "UPDATE Subjects SET ";
            switch (filterSubjects)
            {
                case FilterSubjectsBy.SubjectName:
                    query += "SubjectName = '" + dataGridView1.CurrentCell.Value.ToString() + "'" +
                        " WHERE SubjectID = " + SubjectID;
                    break;
                case FilterSubjectsBy.Teacher:
                    query += "Teacher = '" + dataGridView1.CurrentCell.Value.ToString() + "'" +
                        " WHERE SubjectID = " + SubjectID;
                    break;
                case FilterSubjectsBy.Room:
                    query += "Room = '" + dataGridView1.CurrentCell.Value.ToString() + "'" +
                        " WHERE SubjectID = " + SubjectID;
                    break;

            }
            database.ExecuteSQL(query);
            MessageBox.Show("Data has been successfully updated");
            DataBind("");
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            InsertSubject insertSubject = new InsertSubject();
            insertSubject.ShowDialog();
        }

        public void DeleteSubject()
        {
            string query = "DELETE FROM Subjects WHERE SubjectID = " + SubjectID;
            database.ExecuteSQL(query);
            MessageBox.Show("Subject has been deleted");
            DataBind("");
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            DeleteSubject();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataBind("");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            DataBind(textBox1.Text);
        }
    }
}
