using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread[] threads = new Thread[3]; //creating an array for 3 different threads
            for (int i = 0; i < 3; i++) //dynamically adding threads to the array
            {
                threads[i] = new Thread(PrintName); //creating new thread and assigning function "PrintName" to it
                threads[i].Name = (i + 1).ToString(); //declaring thread's name
                threads[i].Start(); //starting the thread
            }
            Console.ReadKey();
        }

        static void PrintName() //function to print threas's name
        {
            for (int i = 0; i < 3; i++) //dynamically printing current thread name
                Console.WriteLine("Current Thread: " + Thread.CurrentThread.Name);
        }
    }
}
