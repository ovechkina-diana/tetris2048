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

        public int x { get; set; }
        public int y { get; set; }
        public int value { get; set; }
        public bool IsFell { get; set; } // если EndCurrent == true значит текущий кубик упал

        public Current(int MaxDegree, int x = 2, int y = 0)
        {
            this.x = x;
            this.y = y;
            int Degree = random.Next(1, MaxDegree); // выбираем рандомную степень
            value = (int)Math.Pow(2, Degree);
            IsFell = false;
        }
    }
}
