using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace Class
{
    public class Student
    {
        public string name;
        public string surname;

        public Student() { }

        public Student(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public override string ToString()
        {
            return name + " " + surname;
        }
    }

    public class Classs
    {
        public List<Student> students;
        public string subject;

        public Classs()
        {
            students = new List<Student>();
        }

        public Classs(string subject)
        {
            students = new List<Student>();
            this.subject = subject;
        }

        public void Serialize()
        {
            FileStream fs = new FileStream("file.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(Classs));
            xs.Serialize(fs, this);
            fs.Close();
        }

        public Classs Deserialize()
        {
            FileStream fs = new FileStream("file.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(Classs));
            Classs class2 = xs.Deserialize(fs) as Classs;
            fs.Close();
            return class2;
        }

        public void Print()
        {
            Console.WriteLine(subject + ":");
            students.ForEach(Console.WriteLine);
        }

        public void Add(Student s)
        {
            students.Add(s);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Classs class1 = new Classs("PP2");
            Student st1 = new Student("Madina", "Almanova");
            Student st2 = new Student("aaa", "bbb");
            class1.Add(st1);
            class1.Add(st2);
            class1.Serialize();
            Classs class2 = class1.Deserialize();
            class2.Print();
            Console.ReadKey();
        }
    }
}
