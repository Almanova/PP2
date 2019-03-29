using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Factorial
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Console.ReadLine();
            int n = int.Parse(s);
            Thread thread = new Thread(() => Fact(n));
            Thread thread2 = new Thread(() => Sum(n));
            thread.Start();
            thread2.Start();
            Console.ReadKey();
        }

        static void Fact(int n)
        {
            long x = 1;
            for (int i = n; i > 0; i--)
            {
                x *= i;
                Console.WriteLine("Counting factorial");
            }
            Console.WriteLine("Counted factorial: " + x);
        }

        static void Sum(int n)
        {
            int x = 0;
            for (int i = 1; i <= n; i++)
            {
                x += i;
                if (i == n)
                    Console.WriteLine("Counted sum: " + x);
                else Console.WriteLine("Counting sum");
            }
        }
    }
}
