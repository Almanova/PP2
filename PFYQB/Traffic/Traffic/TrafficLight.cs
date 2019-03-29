using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Traffic
{
    public class TrafficLight
    {
        List<Point> body;

        public TrafficLight()
        {
            body = new List<Point>();
        }

        public void Load()
        {
            StreamReader sr = new StreamReader("traffic.txt");
            string[] circle = sr.ReadToEnd().Split('\n');
            for (int i = 0; i < circle.Length; i++)
                for (int j = 0; j < circle[i].Length; j++)
                    if (circle[i][j] == '*')
                        body.Add(new Point(j, i));
        }

        public void Draw(ConsoleColor color, int n)
        {
            Console.Clear();
            Console.ForegroundColor = color;
            foreach(Point p in body)
            {
                Console.SetCursorPosition(p.x + 50, p.y + n);
                Console.Write('*');
            }
        }
    }
}
