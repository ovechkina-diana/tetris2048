using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris2048
{
    class Model
    {
        Map map; // игровое поле

        static Random random = new Random();
        bool isGameOver = false; // пересечение границы

        public int width
        {
            get { return map.width; }
        } // ширина поля
        public int length
        {
            get { return map.length; }
        } // длина поля

        private int MaxNumber = 2; // максимальное число
        //private uint Points; // очки

        public Model(int width, int length) // конструктор (старт игры)
        {
            map = new Map(width, length);
        }

        public void Start()
        {
            for (int x = 0; x < width; x++)
                for (int y = 0; y < length; y++)
                    map.Set(x, y, 0);

            AddRandomNumber();
        }

        void Lift(Current current, int sx, int sy) // движение кубика
        {
            if (map.Get(current.X + sx, current.Y + sy) == 0)
            {
                map.Set(current.X + sx, current.Y + sy, map.Get(current.X, current.Y));
                map.Set(current.X, current.Y, 0);
                current.X += sx;
                current.Y += sy;
            }
        }

        public void Left(Current current)
        {
            Lift(current, -1, 0);
        }

        public void Right(Current current)
        {
            Lift(current, +1, 0);
        }

        public void Down(Current current) // нужно зациклить, чтобы падал не прерываясь
        {
            Lift(current, 0, +1);
        }

        public void FallCube(Current current) // падение кубика, независимое от пользователя
        {

        }

        public int GetMap(int x, int y)
        {
            return map.Get(x, y);
        }

        void AddRandomNumber() // рандомное выпадение кубика (пока неверно работает)
        {
            if (isGameOver)
                return;

            map.Set(2, 0, random.Next(1, MaxNumber + 1) * 2); // работает для поля 5, 7
        }

        void Join() // объединение кубиков
        {

        }

        public bool GameOver() // конец игры
        {
            return isGameOver;
        }

    }
}
