using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadedSnakeGame
{
    public class Snake : GameObject
    {
        public char head;
        public int OutOfConsole;
        public Direction direction;

        public Snake() { }

        public enum Direction
        {
            None,
            Right,
            Left,
            Up,
            Down
        }

        public Snake(int x, int y, char sign, ConsoleColor color, char head, int OutOfConsole) : base(x, y, sign, color)
        {
            this.head = head;
            this.OutOfConsole = OutOfConsole;
            direction = Direction.None;
        }

        public void DrawSnake()
        {
            Console.ForegroundColor = color;
            for (int i = 0; i < body.Count; i++)
            {
                Console.SetCursorPosition(body[i].x, body[i].y);
                if (i == 0)
                    Console.Write(head);
                else Console.Write(sign);
            }
        }

        public void ClearSnake()
        {
            foreach (Point p in body)
            {
                Console.SetCursorPosition(p.x, p.y);
                Console.Write(' ');
            }
        }

        public bool CheckDirection(ConsoleKeyInfo consoleKey)
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

        public void Move()
        {
            ClearSnake();

            for (int i = body.Count - 1; i > 0; i--)
            {
                body[i].x = body[i - 1].x;
                body[i].y = body[i - 1].y;
            }

            if (direction == Direction.Up)
            {
                body[0].y--;
                if (body[0].y < 0)
                {
                    body[0].y = Console.WindowHeight - 1;
                    OutOfConsole++;
                }
            }

            if (direction == Direction.Down)
            {
                body[0].y++;
                if (body[0].y == Console.WindowHeight)
                {
                    body[0].y = 0;
                    OutOfConsole++;
                }
            }

            if (direction == Direction.Left)
            {
                body[0].x--;
                if (body[0].x < 0)
                {
                    body[0].x = Console.WindowWidth - 1;
                    OutOfConsole++;
                }
            }

            if (direction == Direction.Right)
            {
                body[0].x++;
                if (body[0].x >= Console.WindowWidth)
                {
                    body[0].x = 0;
                    OutOfConsole++;
                }
            }
        }

        public void ChangeDirection(ConsoleKeyInfo keyInfo)
        {
            if (keyInfo.Key == ConsoleKey.UpArrow)
                direction = Direction.Up;
            if (keyInfo.Key == ConsoleKey.DownArrow)
                direction = Direction.Down;
            if (keyInfo.Key == ConsoleKey.LeftArrow)
                direction = Direction.Left;
            if (keyInfo.Key == ConsoleKey.RightArrow)
                direction = Direction.Right;
        }

        public void ToTheCorner()
        {
            for (int i = 0; i < body.Count(); i++)
            {
                direction = Direction.Right;
                body[i].x = body.Count - i;
                body[i].y = 2;
            }
        }
    }
}
