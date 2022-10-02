using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace tetris2048
{
    class Model
    {
        public Map map { get; } // игровое поле

        private bool IsGameOver; // продолжение игры

        private int MaxNum; // максимальное число

        private Current current; // текущий кубик

        public Model() // конструктор
        {
            map = new Map();
            IsGameOver = false;
            MaxNum = 2;
        }

        public void Start()
        {
            for (int x = 0; x < map.GetWidth(); x++)
                for (int y = 0; y < map.GetLength(); y++)
                    map.Set(x, y, 0);

            AddCurrent();
        }

        public void NewStart()
        {
            for (int i = 0; i < map.GetWidth(); i++)
            {
                if (map.Get(i, 1) != 0)
                    IsGameOver = true;
            }

            AddCurrent();
        }

        void Lift(int sx, int sy) // движение кубика
        {
            if (map.Get(current.x + sx, current.y + sy) == 0)
            {
                map.Set(current.x + sx, current.y + sy, map.Get(current.x, current.y));
                map.Set(current.x, current.y, 0);
                current.x += sx;
                current.y += sy;
            }
        }

        void Lift(int sx, int sy, ref bool EndCurrent) // перегрузка Lift(), вызывается только в Down()
        {
            if (map.Get(current.x + sx, current.y + sy) == 0)
            {
                map.Set(current.x + sx, current.y + sy, map.Get(current.x, current.y));
                map.Set(current.x, current.y, 0);
                current.x += sx;
                current.y += sy;
            }
            else
                EndCurrent = true;
        }

        public void Left()
        {
            Lift(-1, 0);
        }

        public void Right()
        {
            Lift(+1, 0);
        }

        public bool Down()
        {
            bool EndCurrent = false;
            Lift(0, +1, ref EndCurrent);
            return EndCurrent;
        }

        void AddCurrent() // рандомное выпадение кубика
        {
            if (IsGameOver)
                return;

            current = new Current(MaxNum);
            map.Set(current.x, current.y, current.value);
        }

        void Join() // объединение кубиков
        {

        }

        public bool GameOver() // конец игры
        {
            return IsGameOver;
        }
    }
}
