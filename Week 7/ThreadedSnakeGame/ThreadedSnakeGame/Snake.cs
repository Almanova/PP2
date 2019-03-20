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
            for (int i = 0; i < body.Count; i++)
            {
                Console.SetCursorPosition(body[i].x, body[i].y);
                Console.Write(' ');
            }
        }

        public bool CheckDirection(ConsoleKeyInfo consoleKey)
        {
            if (body.Count > 1 && direction == Direction.Down && (consoleKey.Key == ConsoleKey.UpArrow || consoleKey.Key == ConsoleKey.W))
                return false;
            if (body.Count > 1 && direction == Direction.Up && (consoleKey.Key == ConsoleKey.DownArrow || consoleKey.Key == ConsoleKey.S))
                return false;
            if (body.Count > 1 && direction == Direction.Left && (consoleKey.Key == ConsoleKey.RightArrow || consoleKey.Key == ConsoleKey.D))
                return false;
            if (body.Count > 1 && direction == Direction.Right && (consoleKey.Key == ConsoleKey.LeftArrow || consoleKey.Key == ConsoleKey.A))
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

        public void ChangeDirection(ConsoleKeyInfo consoleKey)
        {

            if (consoleKey.Key == ConsoleKey.UpArrow || consoleKey.Key == ConsoleKey.W)
                direction = Direction.Up;
            if (consoleKey.Key == ConsoleKey.DownArrow || consoleKey.Key == ConsoleKey.S)
                direction = Direction.Down;
            if (consoleKey.Key == ConsoleKey.LeftArrow || consoleKey.Key == ConsoleKey.A)
                direction = Direction.Left;
            if (consoleKey.Key == ConsoleKey.RightArrow || consoleKey.Key == ConsoleKey.D)
                direction = Direction.Right;
        }

        public void ToTheLeftCorner()
        {
            for (int i = 0; i < body.Count(); i++)
            {
                direction = Direction.Right;
                body[i].x = body.Count - i;
                body[i].y = 2;
            }
        }

        public void ToTheRightCorner()
        {
            for (int i = 0; i < body.Count(); i++)
            {
                direction = Direction.Left;
                body[i].x = 118 - body.Count + i;
                body[i].y = 2;
            }
        }
    }
}
