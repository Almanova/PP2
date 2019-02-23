using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialization
{
    public class Student
    {
        public StudentInfo StudentInfo;
        public List<SubjectInfo> Subjects;

        public Student()
        {
            Subjects = new List<SubjectInfo>();
        }

        public Student(StudentInfo StudentInfo)
        {
            this.StudentInfo = StudentInfo;
            Subjects = new List<SubjectInfo>();
        }
    }

    public class StudentInfo
    {
        public string FirstName;
        public string LastName;
        public string id;

        public StudentInfo(string FirstName, string LastName, string id)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.id = id;
        }

        public StudentInfo() { }
    }

    public class SubjectInfo
    {
        public string name;
        public string room;
        public Teacher teacher;

        public SubjectInfo(string name, string room, Teacher teacher)
        {
            this.name = name;
            this.room = room;
            this.teacher = teacher;
        }

        public SubjectInfo() { }
    }

    public class Teacher
    {
        public string FirstName;
        public string LastName;
        public string email;

        public Teacher(string FirstName, string LastName, string email)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.email = email;
        }

        public Teacher() { }
    }
}
