using System;
using System.IO;

namespace Examples
{
    class Program
    {
        public static void PrintSpaces(int level)
        {
            for (int i = 0; i < level; i++)
                Console.Write("     ");
        }

        public static void MainDirectory()
        {
            DirectoryInfo directory = new DirectoryInfo("/Users/Asus/Documents/Directory");
            Console.WriteLine(directory.Name);
            Console.WriteLine();
        }

        public static void Tree(DirectoryInfo dir, int level)
        {
            foreach (FileInfo f in dir.GetFiles())
            {
                PrintSpaces(level);
                Console.WriteLine(f.Name);
            }
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                PrintSpaces(level);
                Console.WriteLine(d.Name);
                Tree(d, level + 1);
            }
        }

        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo("/Users/Asus/Documents/Directory");
            MainDirectory();
            Tree(dir, 0);
            Console.ReadKey();
        }
    }
}