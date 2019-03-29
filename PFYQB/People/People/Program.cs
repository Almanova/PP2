using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace People
{
    public class Person
    {
        public string name;
        public List<string> messages;

        public Person() { }

        public Person(string name)
        {
            this.name = name;
            messages = new List<string>();
        }

        public void Get(Person person, string text)
        {
            string s = person.name + ": " + text;
            messages.Add(s);
            Send(this, text);
        }

        public void Send(Person person, string text)
        {
            string ss = "Me: " + text;
            messages.Add(ss);
        }

        public void Serialize()
        {
            FileStream fs = new FileStream("file2.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(Person));
            xs.Serialize(fs, this);
            fs.Close();
        }

        public Person Deserialize()
        {
            FileStream fs = new FileStream("file2.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(Person));
            Person person2 = xs.Deserialize(fs) as Person;
            fs.Close();
            return person2;
        }

        public void Print()
        {
            Console.WriteLine(name);
            messages.ForEach(Console.WriteLine);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("Maddie");
            Person person2 = new Person("Alice");
            person.Get(person2, "Hi, how are you?");
            person.Get(person, "Everything's great, thanks");
            person.Serialize();
            Person person1 = person.Deserialize();
            person1.Print();
            Console.ReadKey();
        }
    }
}
