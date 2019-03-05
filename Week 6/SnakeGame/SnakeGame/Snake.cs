using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{

    public class Snake:GameObject
    {
        public int OutOfConsole;

        public Snake() { }

        public Snake(int x, int y, char sign, ConsoleColor color, int OutOfConsole) : base(x, y, sign, color)
        {
            this.OutOfConsole = OutOfConsole;
        }

        enum Direction
        {
            None,
            Right,
            Left,
            Up,
            Down
        }

        Direction direction = Direction.None;

        public bool SnakeDirection(ConsoleKeyInfo consoleKey)
        {
            if (body.Count > 1 && direction == Direction.Down && consoleKey.Key == ConsoleKey.UpArrow)
                return false;
            if (body.Count > 1 && direction == Direction.Up && consoleKey.Key == ConsoleKey.DownArrow)
                return false;
            if (body.Count > 1 && direction == Direction.Left && consoleKey.Key == ConsoleKey.RightArrow)
                return false;
            if (body.Count > 1 && direction == Direction.Right && consoleKey.Key == ConsoleKey.LeftArrow)
                return false;
            return true;
        }

        public void Move(ConsoleKeyInfo consoleKey)
        {
            for (int i = body.Count - 1; i > 0; i--)
            {
                body[i].x = body[i - 1].x;
                body[i].y = body[i - 1].y;
            }

            if (consoleKey.Key == ConsoleKey.UpArrow)
            {
                body[0].y--;
                direction = Direction.Up;
                if (body[0].y < 0)
                {
                    body[0].y = Console.WindowHeight - 1;
                    OutOfConsole++;
                }
            }

            if (consoleKey.Key == ConsoleKey.DownArrow)
            {
                body[0].y++;
                direction = Direction.Down;
                if (body[0].y == Console.WindowHeight)
                {
                    body[0].y = 0;
                    OutOfConsole++;
                }
            }

            if (consoleKey.Key == ConsoleKey.LeftArrow)
            {
                body[0].x--;
                direction = Direction.Left;
                if (body[0].x < 0)
                {
                    body[0].x = Console.WindowWidth - 1;
                    OutOfConsole++;
                }
            }

            if (consoleKey.Key == ConsoleKey.RightArrow)
            {
                direction = Direction.Right;
                body[0].x++;
                if (body[0].x >= Console.WindowWidth)
                {
                    body[0].x = 0;
                    OutOfConsole++;
                }
            }
        }

        public void ToTheCorner()
        {
            for (int i = 0; i < body.Count(); i++)
            {
                direction = Direction.Right;
                body[i].x = body.Count - i;
                body[i].y = 1;
            }
        }
    }
}
