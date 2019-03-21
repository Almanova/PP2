using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            MyThread t1 = new MyThread("Thread 1"); //creating objects of "MyThread" class
            MyThread t2 = new MyThread("Thread 2");
            MyThread t3 = new MyThread("Thread 3");

            t1.StartThread(); //calling functions to start threads
            t2.StartThread();
            t3.StartThread();

            Console.ReadKey();
        }
    }
}
