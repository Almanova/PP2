using System;
using System.IO;

namespace Task3
{
    class Program
    {
        public static void PrintSpaces(int level) //creating a method to iterate through the files and directories
        {
            for (int i = 0; i < level; i++)
                Console.Write("     ");
        }

        public static void MainDirectory() //accessing main directory
        {
            DirectoryInfo directory = new DirectoryInfo("/Users/Asus/Documents/Directory");
            Console.WriteLine(directory.Name); //printing it's name
            Console.WriteLine();
        }

        public static void Tree(DirectoryInfo dir, int level) //creating a method to go through directories and files
        {
            foreach (FileInfo f in dir.GetFiles()) //going through every file
            {
                PrintSpaces(level); //iterating
                Console.WriteLine(f.Name); //printing file's name
            }
            foreach (DirectoryInfo d in dir.GetDirectories()) //going through every directory
            {
                PrintSpaces(level); //iterating
                Console.WriteLine(d.Name); //printing directory's name
                Tree(d, level + 1); //recursively checking all subdirectories of a main directory
            }
        }

        static void Main(string[] args)
        {
            DirectoryInfo dir = new DirectoryInfo("/Users/Asus/Documents/Directory"); //path to reach a directory
            MainDirectory(); //calling functions
            Tree(dir, 0);
            Console.ReadKey(); //quiting program by pressing any key (or otherwise it closes immediately)
        }
    }
}