using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedSnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            mainMenu.StartMenu();
        }
    }
}
