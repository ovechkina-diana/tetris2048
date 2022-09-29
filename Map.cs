using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris2048
{
    class Map
    {
        public int width { get; private set; }
        public int length { get; private set; }
        int[,] map;

        public Map(int width, int length)
        {
            this.width = width;
            this.length = length;
            map = new int[width, length];
        }

        public int Get(int x, int y)
        {
            if (OnMap(x, y))
                return map[x, y];
            return -1;
        }

        public void Set(int x, int y, int number)
        {
            if (OnMap(x, y))
                map[x, y] = number;
        }

        private bool OnMap(int x, int y)
        {
            return x >= 0 && x < width &&
                   y >= 0 && y < length;
        }

        public void Show()
        {
            for (int i = 0; i < width; i++)
                for (int j = 0; j < length; j++)
                    Console.Write(map[i, j]);
        }
    }
}
