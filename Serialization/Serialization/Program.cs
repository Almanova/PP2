using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Serialization
{
    class Program
    {
        public static void In()
        {
            Student Student1 = new Student();
            StudentInfo studentInfo = new StudentInfo("Madina", "Almanova", "18BD133333");
            Student1.StudentInfo = studentInfo;

            SubjectInfo Subject1 = new SubjectInfo();
            Subject1.name = "Programming Principles 2";
            Subject1.room = "414";
            Teacher Teacher1 = new Teacher("Askar", "Akshabayev", "a_akshabayev@kbtu.com");
            Subject1.teacher = Teacher1;

            SubjectInfo Subject2 = new SubjectInfo();
            Subject2.name = "Calculus 2";
            Subject2.room = "444";
            Teacher Teacher2 = new Teacher("Vladimir", "Ten", "v_ten@kbtu.com");
            Subject2.teacher = Teacher2;

            Student1.Subjects.Add(Subject1);
            Student1.Subjects.Add(Subject2);

            FileStream fs = new FileStream("student.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(Student));
            xs.Serialize(fs, Student1);
            fs.Close();
        }

        public static void Out()
        {
            FileStream fs = new FileStream("student.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(Student));
            Student Student1 = xs.Deserialize(fs) as Student;

            Console.WriteLine(Student1.StudentInfo.FirstName + " " + Student1.StudentInfo.LastName);
            Console.WriteLine(Student1.StudentInfo.id);
            Console.WriteLine(Student1.Subjects[0].name + " - " + Student1.Subjects[0].teacher.LastName + ", " + Student1.Subjects[0].room);
            Console.WriteLine(Student1.Subjects[1].name + " - " + Student1.Subjects[1].teacher.LastName + ", " + Student1.Subjects[1].room);

            fs.Close();
        }

        static void Main(string[] args)
        {
            In();
            Out();

            Console.ReadKey();
        }
    }
}