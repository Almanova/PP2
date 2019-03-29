using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Balloon
{
    class Shar
    {
        public List<Point> body;

        public Shar()
        {
            body = new List<Point>();  
        }

        public void Load()
        {
            StreamReader sr = new StreamReader("shar.txt");
            string[] shar = sr.ReadToEnd().Split('\n');
            for (int i = 0; i < shar.Length; i++)
                for (int j = 0; j < shar[i].Length; j++)
                    if (shar[i][j] == '*')
                        body.Add(new Point(j, i));
        }
    }
}
