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

        private int MaxDegree; // максимальная степень

        private Current current; // текущий кубик
        public int points;

        public Model() // конструктор
        {
            map = new Map();
            IsGameOver = false;
            MaxDegree = 1;
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

        void AddCurrent() // рандомное выпадение кубика
        {
            if (IsGameOver)
                return;

            current = new Current(MaxNum);
            map.Set(current.x, current.y, current.value);
        }

        public bool Join() // объединение кубиков
        {
            bool JoinIs = false;
            int value = current.value;
            LiftforJoin(-1, 0, value, ref JoinIs);
            LiftforJoin(+1, 0, value, ref JoinIs);
            LiftforJoin(0, +1, value, ref JoinIs);

            if (JoinIs)
                points += current.value;
            return JoinIs;
        }

        private void LiftforJoin(int sx, int sy, int value, ref bool JoinIs)
        {
            if (map.Get(current.x + sx, current.y + sy) == value)
            {
                JoinIs = true;

                current.value *= 2;

                if (current.value > (int)Math.Pow(2, MaxDegree))
                    MaxDegree++;

                map.Set(current.x, current.y, current.value);

                map.Set(current.x + sx, current.y + sy, 0);
            }
        }

        public void DownforJoin()
        {
            for (int x = 0; x < map.GetWidth(); x++)
                for (int y = map.GetLength(); y >= 0; y--)
                    Down(x, y);
        }

        private void Down(int x, int y)            // перегрузка Down(), вызывается в DownforJoin() 
        {
            if (map.Get(x, y + 1) == 0)
            {
                map.Set(x, y + 1, map.Get(x, y));
                map.Set(x, y, 0);
                if (x == current.x && y == current.y)
                    current.y += 1;
            }
        }

        public bool GameOver() // конец игры
        {
            return IsGameOver;
        }
    }
}