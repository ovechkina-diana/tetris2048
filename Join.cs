using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris2048
{
    static class Join
    {
        public static bool MergeCubes()
        {
            bool JoinIs = false;

            for (int x = Map.width - 1; x >= 0; x--)
                for (int y = Map.length - 1; y > 0; y--)
                    if (Map.Get(x, y) != 0)
                    {
                        Merge(x, y, ref JoinIs);

                        if (JoinIs)
                            return JoinIs;
                    }

            return JoinIs;
        }

        public static void Merge(int x, int y, ref bool JoinIs) // объединение кубиков
        {
            int value = Map.Get(x, y);

            MergewithLift(x, y, -1, 0, value, ref JoinIs);
            //MergewithLift(x, y, +1, 0, value, ref JoinIs);
            //MergewithLift(x, y, 0, +1, value, ref JoinIs);
            MergewithLift(x, y, 0, -1, value, ref JoinIs);

            if (JoinIs)
                Game.Points += Map.Get(x, y);
        }

        private static void MergewithLift(int x, int y, int sx, int sy, int value, ref bool JoinIs)
        {
            if (Map.Get(x + sx, y + sy) == value)
            {
                JoinIs = true;

                int NewValue = Map.Get(x, y) * 2;

                if (NewValue > (int)Math.Pow(2, Game.MaxDegree))
                    Game.MaxDegree++;

                Map.Set(x, y, NewValue);

                Map.Set(x + sx, y + sy, 0);
            }
        }

        public static void Down()
        {
            for (int x = Map.width - 1; x >= 0; x--)
                for (int y = Map.length - 1; y >= 0; y--)
                    if (Map.Get(x, y + 1) == 0)
                    {
                        Map.Set(x, y + 1, Map.Get(x, y));
                        Map.Set(x, y, 0);
                    }
        }
    }
}
