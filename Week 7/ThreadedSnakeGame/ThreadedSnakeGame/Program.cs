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
            Snake snake = new Snake(3, 2, 'o', ConsoleColor.Magenta, 'O', 0);
            Food food = new Food(0, 0, '*', ConsoleColor.Cyan);
            Wall wall = new Wall('#', ConsoleColor.DarkYellow);
            string nickname = "Maddie";
            Game game = new Game(nickname, snake, food, wall);
            game.Start();
        }
    }
}
