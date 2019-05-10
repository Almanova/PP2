using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace UniversityClass
{
    public class University
    {
        public string UniversityName;
        public int NumberOfStudents;

        public University()
        {
        }

        public University(string UniversityName, int NumberOfStudents)
        {
            this.UniversityName = UniversityName;
            this.NumberOfStudents = NumberOfStudents;
        }

        public void Increment(University u)
        {
            u.NumberOfStudents++;
        }

        public override string ToString()
        {
            return UniversityName + " " + NumberOfStudents;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<University> list;
            if (File.Exists("unis.xml"))
            {
                list = Deserialize();
            }
            else
                list = new List<University>();

            while (true)
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey(true);

                if (consoleKey.Key == ConsoleKey.C)
                {
                    string UniversityName = Console.ReadLine();
                    string NumberOfStudents = Console.ReadLine();
                    University uni = new University(UniversityName, int.Parse(NumberOfStudents));
                    list.Add(uni);
                    Serialize(list);
                }

                else if (consoleKey.Key == ConsoleKey.A)
                {
                    string UniName = Console.ReadLine();
                    foreach (University u in list)
                    {
                        if (u.UniversityName == UniName)
                            u.Increment(u);
                    }
                    Serialize(list);
                }

                else if (consoleKey.Key == ConsoleKey.UpArrow)
                {
                    list = Deserialize();
                    list.ForEach(Console.WriteLine);
                }
            }
        }

        public static void Serialize(List<University> unis)
        {
            if (File.Exists("unis.xml"))
                File.Delete("unis.xml");
            FileStream fs = new FileStream("unis.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(List<University>));
            xs.Serialize(fs, unis);
            fs.Close();
        }

        public static List<University> Deserialize()
        {
            FileStream fs = new FileStream("unis.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(List<University>));
            List<University> unis = xs.Deserialize(fs) as List<University>;
            fs.Close();
            return unis;
        }
    }
}
