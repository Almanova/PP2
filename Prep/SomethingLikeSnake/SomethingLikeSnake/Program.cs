using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace SomethingLikeSnake
{
    public class Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public enum Direction
    {
        Right,
        Left,
        Up,
        Down
    }

    public class Car
    {

        public List<Point> body;
        public Direction direction = Direction.Right;

        public Car()
        {
            body = new List<Point>();
        }

        public void Load()
        {
            StreamReader sr = new StreamReader("car.txt");
            string[] car = sr.ReadToEnd().Split('\n');
            for (int i = 0; i < car.Length; i++)
                for (int j = 0; j < car[i].Length; j++)
                    if (car[i][j] == '*')
                        body.Add(new Point(j, i));
        }
    }

    public class Game
    {
        public Car car;
        public Game()
        {
            car = new Car();
        }

        public void Start()
        {
            car.Load();

            Thread thread = new Thread(MoveCar);
            thread.Start();

            while (true)
            {
                ConsoleKeyInfo consoleKey = Console.ReadKey(true);
                if (consoleKey.Key == ConsoleKey.RightArrow)
                    car.direction = Direction.Right;
                else if (consoleKey.Key == ConsoleKey.LeftArrow)
                    car.direction = Direction.Left;
                else if (consoleKey.Key == ConsoleKey.UpArrow)
                    car.direction = Direction.Up;
                else if (consoleKey.Key == ConsoleKey.DownArrow)
                    car.direction = Direction.Down;
            }
        }

        public void MoveCar()
        {
            while (true)
            {
                if (car.direction == Direction.Right)
                {
                    foreach (Point p in car.body)
                        p.x++;
                }
                else if (car.direction == Direction.Left)
                {
                    foreach (Point p in car.body)
                        p.x--;
                }
                else if (car.direction == Direction.Up)
                {
                    foreach (Point p in car.body)
                        p.y--;
                }
                else if (car.direction == Direction.Down)
                {
                    foreach (Point p in car.body)
                        p.y++;
                }
                Console.Clear();
                Draw(car);
                Thread.Sleep(100);
            }
        }

        public void Draw(Car car)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            foreach (Point p in car.body)
            {
                Console.SetCursorPosition(p.x, p.y);
                Console.Write('*');
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Game game = new Game();
            game.Start();
        }
    }
}
