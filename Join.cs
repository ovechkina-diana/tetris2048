using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris2048
{
    static class Join
    {
        public static bool MergerIs() // проверяет, есть ли слияние
        {
            for (int y = Map.length - 1; y > 0; y--)
                for (int x = Map.width - 1; x >= 0; x--)

                    if (Map.Get(x, y) != 0)
                    {
                        if (Equality(x, y, +1, 0) || Equality(x, y, -1, 0) || Equality(x, y, 0, +1) || Equality(x, y, 0, -1))
                            return true;
                    }

            return false;
        }

        private static bool Equality(int x, int y, int sx, int sy)
        {
            return Map.Get(x + sx, y + sy) == Map.Get(x, y);
        }


        private static bool Equality(int x, int y, int sx, int sy, int value)
        {
            return Map.Get(x + sx, y + sy) == value;
        }

        public static void Mergering()
        {
            for (int y = 1; y < Map.length; y++)
                for (int x = 0; x < Map.width; x++)
                {
                    int value = Map.Get(x, y);
                    if (value == 0)
                        continue;

                    if (Equality(x, y, -1, 0, value))
                    {
                        if (!Equality(x - 1, y, 0, -1, value) && !Equality(x - 1, y, 0, +1, value) && !Equality(x - 1, y, -1, 0, value))
                            NewValue(x, y, -1, 0);
                    }

                    if (Equality(x, y, +1, 0, value))
                    {
                        if (!Equality(x + 1, y, 0, -1, value) && !Equality(x + 1, y, 0, +1, value) && !Equality(x + 1, y, +1, 0, value))
                            NewValue(x, y, +1, 0);
                    }

                    if (Equality(x, y, 0, -1, value))
                    {
                        if (!Equality(x, y - 1, -1, 0, value) && !Equality(x, y - 1, +1, 0, value) && !Equality(x, y - 1, 0, -1, value))
                            NewValue(x, y, 0, -1);
                    }

                    if (Equality(x, y, 0, +1, value))
                    {
                        if (!Equality(x, y + 1, -1, 0, value) && !Equality(x, y + 1, +1, 0, value) && !Equality(x, y + 1, 0, +1, value))
                            NewValue(x, y, 0, +1);
                    }

                    if (Map.Get(x, y) != value)
                    {
                        Game.Points += Map.Get(x, y);
                        return;
                    }
                }
        }

        private static void NewValue(int x, int y, int sx, int sy)
        {
            int NewValue = Map.Get(x, y) * 2;

            if (NewValue > (int)Math.Pow(2, Game.MaxDegree))
                Game.MaxDegree++;

            Map.Set(x, y, NewValue);

            Map.Set(x + sx, y + sy, 0);
        }

        public static void Down()
        {
            for (int y = Map.length - 1; y >= 0; y--)
                for (int x = Map.width - 1; x >= 0; x--)
                    if (Map.Get(x, y + 1) == 0)
                    {
                        Map.Set(x, y + 1, Map.Get(x, y));
                        Map.Set(x, y, 0);
                    }
        }
    }
}
