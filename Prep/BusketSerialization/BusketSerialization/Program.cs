using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace BusketSerialization
{
    public class Item
    {
        public string ItemName;
        public int Quantity;

        public Item()
        {

        }

        public Item(string ItemName, int Quantity)
        {
            this.ItemName = ItemName;
            this.Quantity = Quantity;
        }
    }

    public class Customer
    {
        public string CustomerName;
        public List<Item> Busket;

        public Customer()
        {

        }

        public Customer(string CustomerName)
        {
            this.CustomerName = CustomerName;
            Busket = new List<Item>();
        }

        public void AddItems(Customer customer, string ItemName, int Quantity)
        {
            Item item = new Item(ItemName, Quantity);
            customer.Busket.Add(item);
        }

        public override string ToString()
        {
            string result = CustomerName + ": ";
            foreach (Item i in Busket)
            {
                result += i.ItemName;
                result += ", ";
            }
            return result;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            List<Customer> list;
            if (File.Exists("customers.xml"))
            {
                list = Deserialize();
            }
            else
                list = new List<Customer>();

            while(true)
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey(true);

                if (consoleKey.Key == ConsoleKey.C)
                {
                    string CustomerName = Console.ReadLine();
                    Customer customer = new Customer(CustomerName);
                    list.Add(customer);
                    Serialize(list);
                }

                if (consoleKey.Key == ConsoleKey.A)
                {
                    string CustomerName = Console.ReadLine();
                    string ItemName = Console.ReadLine();
                    string Quantity = Console.ReadLine();
                    foreach (Customer c in list)
                    {
                        if (c.CustomerName == CustomerName)
                            c.AddItems(c, ItemName, int.Parse(Quantity));
                    }
                    Serialize(list);
                }

                if (consoleKey.Key == ConsoleKey.UpArrow)
                {
                    list = Deserialize();
                    list.ForEach(Console.WriteLine);
                }
            }
        }

        public static void Serialize(List<Customer> unis)
        {
            if (File.Exists("customers.xml"))
                File.Delete("customers.xml");
            FileStream fs = new FileStream("customers.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(List<Customer>));
            xs.Serialize(fs, unis);
            fs.Close();
        }

        public static List<Customer> Deserialize()
        {
            FileStream fs = new FileStream("customers.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer xs = new XmlSerializer(typeof(List<Customer>));
            List<Customer> customers = xs.Deserialize(fs) as List<Customer>;
            fs.Close();
            return customers;
        }
    }
}
