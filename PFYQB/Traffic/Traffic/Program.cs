using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Traffic
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Thread thread = new Thread(Light);
            thread.Start();
        }

        static void Light()
        {
            TrafficLight traffic = new TrafficLight();
            traffic.Load();

            while (true)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (i == 0)
                    {
                        traffic.Draw(ConsoleColor.Red, 5);
                        Thread.Sleep(1000);
                    }
                    if (i == 1)
                    {
                        traffic.Draw(ConsoleColor.Yellow, 10);
                        Thread.Sleep(200);
                    }
                    if (i == 2)
                    {
                        traffic.Draw(ConsoleColor.Green, 15);
                        Thread.Sleep(1000);
                    }
                }
            }
        }
    }
}
