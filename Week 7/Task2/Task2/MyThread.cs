using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Task2
{
    class MyThread //creating a new class "MyThread"
    {
        public Thread threadField; //declaring class property

        public MyThread() { } //declaring empty constructor

        public MyThread(string name) //creating constructor
        {
            threadField = new Thread(Method); //creating new thread, assigning function "Method" to it
            threadField.Name = name; //assigning name to the thread

            void Method() //function to print the output
            {
                for (int i = 1; i <= 4; i++)
                {
                    if (i == 4)
                        Console.WriteLine(Thread.CurrentThread.Name + " prints " + i + "\n" + Thread.CurrentThread.Name + " has completed");
                    else
                        Console.WriteLine(Thread.CurrentThread.Name + " prints " + i);
                }
            }
        } 

        public void StartThread() //method to start a thread
        {
            threadField.Start(); //starting a thread
        }

        /*public void Method()
        {
            for (int i = 1; i <= 4; i++)
            {
                if (i == 4)
                    Console.WriteLine(Thread.CurrentThread.Name + " prints " + i + "\n" + Thread.CurrentThread.Name + " has completed");
                else
                    Console.WriteLine(Thread.CurrentThread.Name + " prints " + i);
            }
        }*/
    }
}
