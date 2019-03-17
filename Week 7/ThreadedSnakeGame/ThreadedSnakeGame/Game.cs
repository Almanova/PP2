using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ThreadedSnakeGame
{
    public class Game
    {
        //public List<GameObject> GameObjects;
        public bool isAlive;
        public string nickname;
        public Snake snake;
        public Food food;
        public Wall wall;

        public Game() { }

        public Game(string nickname, Snake snake, Food food, Wall wall)
        {
            //GameObjects = new List<GameObject>();
            isAlive = true;
            this.nickname = nickname;
            this.snake = snake;
            this.food = food;
            this.wall = wall;
            //GameObjects.Add(snake);
            //GameObjects.Add(food);
            //GameObjects.Add(wall);
        }

        public void Start()
        {
            wall.DrawWall();
            food.GenerateByCoordinates(60, 6);
            food.DrawFood();
            snake.DrawSnake();
            Labels();

            Thread thread = new Thread(MoveSnake);
            thread.Start();

            while (isAlive)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (snake.CheckDirection(keyInfo))
                    snake.ChangeDirection(keyInfo);
            }
        }

        public void MoveSnake()
        {
            while (isAlive)
            {
                snake.Move();

                if (snake.CollisionWithObject(food))
                {
                    snake.body.Add(new Point(snake.body[snake.body.Count - 1].x - 1, snake.body[snake.body.Count - 1].y));

                    if (snake.body.Count < 11 && snake.body.Count % 3 == 0)
                    {
                        snake.ToTheCorner();
                        wall.NextLevel();
                        wall.DrawWall();
                        food.Clear();
                        food.GenerateByCoordinates(60, 2);
                    }

                    else
                    {
                        while (food.CollisionWithObject(snake) || food.CollisionWithObject(wall))
                            food.Generate();
                    }

                    food.DrawFood();
                }

                if (snake.CollisionWithObject(wall))
                    isAlive = false;
                if (snake.OutOfConsole > 3)
                    isAlive = false;
                if (snake.CollisionWithSnake(snake))
                    isAlive = false;

                snake.ClearSnake();
                snake.DrawSnake();

                Thread.Sleep(150);
            }
        }

        public void Labels()
        {
            string[] labels = new string[]
            {
                "SNAKE GAME > > > Author : Almanova Madina",
                "| " + nickname + " |",
                "Press ESC to go back to Menu"
            };

            ConsoleColor[] colors = new ConsoleColor[]
            {
                ConsoleColor.Cyan,
                ConsoleColor.Magenta,
                ConsoleColor.DarkGray
            };

            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition((Console.WindowWidth - labels[i].Length) / 2, 23 + 2 * i);
                Console.ForegroundColor = colors[i];
                Console.WriteLine(labels[i]);
            }
        }
    }
}
