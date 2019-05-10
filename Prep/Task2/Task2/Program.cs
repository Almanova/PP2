using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Threading;

namespace Task2
{

    public class Employee
    {
        public string name;
        public string wage;

        public Employee() { }

        public Employee(string name, string wage)
        {
            this.name = name;
            this.wage = wage;
        }

        public override string ToString()
        {
            return name + wage;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> empl = new List<Employee>();
            Employee e1 = new Employee("Maddie", "10000");
            Employee e2 = new Employee("Kate", "12000");
            empl.Add(e1);
            empl.Add(e2);
            if (File.Exists("file.xml"))
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey();
                if (consoleKey.Key == ConsoleKey.UpArrow)
                    AddNew();
                if (consoleKey.Key == ConsoleKey.DownArrow)
                    Show();
            }
            else
            {
                FileStream fs = new FileStream("file.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                XmlSerializer xs = new XmlSerializer(typeof(List<Employee>));
                xs.Serialize(fs, empl);
                fs.Close();
            }

            Console.ReadLine();
        }

        static void AddNew()
        {
            string name = Console.ReadLine();
            string wage = Console.ReadLine();

            Employee e3 = new Employee(name, wage);

            FileStream fs = new FileStream("file.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(List<Employee>));
            List<Employee> empl2 = xs.Deserialize(fs) as List<Employee>;
            fs.Close();

            empl2.Add(e3);

            File.Delete("file.xml");
            FileStream fs2 = new FileStream("file.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs2 = new XmlSerializer(typeof(List<Employee>));
            xs2.Serialize(fs2, empl2);
            fs2.Close();
        }

        static void Show()
        {
            FileStream fs = new FileStream("file.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(List<Employee>));
            List<Employee> empl2 = xs.Deserialize(fs) as List<Employee>;
            fs.Close();

            empl2.ForEach(Console.WriteLine);
        }
    }
}
