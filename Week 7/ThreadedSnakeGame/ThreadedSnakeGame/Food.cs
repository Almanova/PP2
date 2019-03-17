using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedSnakeGame
{
    public class Food : GameObject
    {
        public Food() { }

        public Food(int x, int y, char sign, ConsoleColor color) : base(x, y, sign, color) { }

        public void Generate()
        {
            Random random = new Random();
            int x = random.Next(1, 119);
            int y = random.Next(1, 22);
            body[0].x = x;
            body[0].y = y;
        }

        public void GenerateByCoordinates(int x, int y)
        {
            body[0].x = x;
            body[0].y = y;
        }

        public void DrawFood()
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(body[0].x, body[0].y);
            Console.Write(sign);
        }
    }
}
