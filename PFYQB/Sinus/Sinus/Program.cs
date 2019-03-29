using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Sinus
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Thread thread = new Thread(Sinus);
            thread.Start();
            Console.ReadKey();
        }

        static void Sinus()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            double x = 0;
            for (int i = 0; i < 120; i++)
            {
                x += 0.3;
                double y = Math.Sin(x);
                string s = y.ToString();
                char s1;
                int yy;
                if (y < 0 && s.Length > 2)
                {
                    s1 = s[3];
                    yy = (s1 - '0') * (-1);
                }
                else if (s.Length < 2)
                {
                    s1 = '0';
                    yy = s1 - '0';
                }
                else
                {
                    s1 = s[2];
                    yy = s1 - '0';
                }
                Console.SetCursorPosition(i, yy + 10);
                Console.WriteLine('*');
                Thread.Sleep(50);
            }
        }
    }
}
