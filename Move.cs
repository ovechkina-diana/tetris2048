using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris2048
{
    static class Move
    {
        public static bool Lift(ref Current current, int sx, int sy) // движение кубика
        {
            if (Map.Get(current.x + sx, current.y + sy) == 0)
            {
                Map.Set(current.x + sx, current.y + sy, current.value);
                Map.Set(current.x, current.y, 0);
                current.x += sx;
                current.y += sy;
                return true;
            }
            return false;
        }

        public static void Left(ref Current current)
        {
            Lift(ref current, -1, 0);
        }

        public static void Right(ref Current current)
        {
            Lift(ref current, +1, 0);
        }

        public static void Down(ref Current current)
        {
            if (!Lift(ref current, 0, +1))
                current.IsFell = true;
        }
    }
}
