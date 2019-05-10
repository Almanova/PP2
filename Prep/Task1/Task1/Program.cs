using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Threading;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Thread thread = new Thread(Show);
            thread.Start();
        }

        static void Show()
        {
            while (true)
            {
                int y = 5;
                int x = 30;
                for (int i = 1; i <= 3; i++)
                {
                    Draw();
                    y++;
                    x += 3;
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(i);
                    Thread.Sleep(1000);
                }

                for (int i = 4; i <= 6; i++)
                {
                    Draw();
                    y++;
                    x -= 3;
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(i);
                    Thread.Sleep(1000);
                }

                for (int i = 7; i <= 9; i++)
                {
                    Draw();
                    y--;
                    x -= 3;
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(i);
                    Thread.Sleep(1000);
                }

                for (int i = 10; i <= 12; i++)
                {
                    Draw();
                    y--;
                    x += 3;
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(i);
                    Thread.Sleep(1000);
                }
            }
        }

        static void Draw()
        {
            int y = 5;
            int x = 30;
            for (int i = 1; i <= 3; i++)
            {
                y++;
                x += 3;
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(i);
            }

            for (int i = 4; i <= 6; i++)
            {
                y++;
                x -= 3;
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(i);
            }

            for (int i = 7; i <= 9; i++)
            {
                y--;
                x -= 3;
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(i);
            }

            for (int i = 10; i <= 12; i++)
            {
                y--;
                x += 3;
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(i);
            }
        }
    }
}
