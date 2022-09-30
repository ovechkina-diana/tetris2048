using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


/*Проблемы*/
/*1) Не очищается буфер! TmpKey использует неактуальные данные */
/*2) Перед посадкой идет пауза*/
/**/
namespace tetris2048
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }

        void Start()
        {
            Model model = new Model(5, 8);
            model.Start();
            Current current = new Current();
            bool IsStillPlay = true;
            ConsoleKey Key;
            ConsoleKey TmpKey;
            int sp = 0;
            while (IsStillPlay)
            {
                Console.Clear();
                Show(model);
                Thread.Sleep(1000);
                Key = ConsoleKey.DownArrow;
                TmpKey = ConsoleKey.Q; // в буфере q, а не команда
                if (Console.KeyAvailable)
                {
                    TmpKey = Console.ReadKey(true).Key;
                }
                if (TmpKey == ConsoleKey.LeftArrow || TmpKey == ConsoleKey.RightArrow || TmpKey == ConsoleKey.DownArrow)
                {
                    Key = TmpKey;
                    sp = 1;
                    if (TmpKey == ConsoleKey.DownArrow)
                        sp = 2;
                }
                if (TmpKey == ConsoleKey.Backspace)
                    IsStillPlay = false;
                Move(Key, model, current, ref sp);  
                if (current.Y == 7 || model.map.Get(current.X, current.Y + 1) != 0 )
                {
                    model.map.Set(current.X, current.Y, model.num);
                    model.NewStart(current);
                }
                sp = 0;
            }
        }
        static public void Move(ConsoleKey Key, Model model, Current current, ref int sp)
        {
            switch (Key)
            {
                case ConsoleKey.LeftArrow:
                    model.Left(current);
                    sp = 0;
                    break;
                case ConsoleKey.RightArrow:
                    model.Right(current);
                    sp = 0;
                    break;
                case ConsoleKey.DownArrow:
                    if (sp == 0)
                    {
                        model.Down(current);
                    }
                    else
                        for (int i = 0; i < 8 - current.Y + 1; i++) // not enought
                        {
                            model.Down(current);
                            Thread.Sleep(500);
                            Console.Clear();
                            Show(model);
                        }
                    break;
            }
        }

        static void Show(Model model)
        {
            for (int y = 0; y < model.length; y++)
            {
                for (int x = 0; x < model.width; x++)
                {
                    Console.SetCursorPosition(x * 5 + 5, y * 2 + 2);

                    int number = model.GetMap(x, y);

                    Console.Write(number == 0 ? " . " : number.ToString() + "  ");
                }
                if (y == 1)
                    Console.Write("-------");
            }
            Console.WriteLine();
            if (model.GameOver())
                Console.WriteLine("Game Over");
            else
                Console.WriteLine("Still play");
        }
    }
}
