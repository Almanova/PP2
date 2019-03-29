using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Timer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            string s = Console.ReadLine();

            Console.SetCursorPosition(1, 1);
            string ss = DateTime.Now.ToString();
            Console.WriteLine(ss);

            Thread thread = new Thread(() => Print(s));
            thread.Start();

            Console.ReadKey();
        }

        static void Print(string s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                Console.SetCursorPosition(1, i + 3);
                Console.WriteLine(s.Substring(i));
                Thread.Sleep(100);
            }
        }
    }
}
