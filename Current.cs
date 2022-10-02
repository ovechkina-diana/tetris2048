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

        public Current(int MaxNum)
        {
            x = 2;
            y = 0;
            value = random.Next(2, MaxNum);
        }
    }
}
