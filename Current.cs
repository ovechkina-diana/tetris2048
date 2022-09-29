using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris2048
{
    class Current
    {
        public int X { set; get; }
        public int Y { set; get; }

        public Current() { X = 2; Y = 0; }
    }
}
