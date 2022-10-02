using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris2048
{
    class Current
    {
        static Random random = new Random();

        public int x { set; get; }
        public int y { set; get; }
        public int value { set; get; }

        public Current(int MaxDegree, int x = 2, int y = 0)
        {
            this.x = x;
            this.y = y;
            int Degree = random.Next(1, MaxDegree); // выбираем рандомную степень
            value = (int)Math.Pow(2, Degree);
        }
    }
}
