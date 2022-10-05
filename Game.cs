using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace tetris2048
{
    static class Game
    {
        public static bool Over { get; set; } = false; // конец игры

        public static int MaxDegree { get; set; } = 1; // максимальная степень

        public static int Points { get; set; }

        private static int MinDegree;

        public static void Start() // начало игры
        {
            for (int x = 0; x < Map.width; x++)
                for (int y = 0; y < Map.length; y++)
                    Map.Set(x, y, 0);
        }

        public static Current AddCurrent() // выпадение нового кубика
        {
            Current current = new Current(MaxDegree);
            Map.Set(current.x, current.y, current.value);
            return current;
        }

        public static void IsGameOver() // проверка на конец игры
        {
            for (int i = 0; i < Map.width; i++)
            {
                if (Map.Get(i, 1) != 0)
                    Over = true;
            }
        }

        public static void Show() // вывод поля
        {
            Map.Show();
            Console.WriteLine();
            if (Game.Over)
            {
                Console.WriteLine("Game Over");
                player.Points = Points;
            }
            else
                Console.WriteLine("Still play");
        }
    }
}
