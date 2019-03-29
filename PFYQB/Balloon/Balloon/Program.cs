using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Balloon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Thread thread = new Thread(Move);
            thread.Start();
        }

        static void Move()
        {
            Shar shar = new Shar();
            shar.Load();
            while (true)
            {
                for (int i = 1; i <= 90; i++)
                {
                    Console.Clear();
                    foreach (Point p in shar.body)
                    {
                        p.x++;
                    }
                    Draw(shar);
                    Thread.Sleep(50);
                }

                for (int i = 90; i >= 1; i --)
                {
                    Console.Clear();
                    foreach (Point p in shar.body)
                    {
                        p.x--;
                    }
                    Draw(shar);
                    Thread.Sleep(50);
                }
                    
            }
        }

        static void Draw(Shar shar)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (Point p in shar.body)
            {
                Console.SetCursorPosition(p.x, p.y);
                Console.Write('*');
            }
        }
    }
}
