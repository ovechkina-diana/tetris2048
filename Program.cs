using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace tetris2048
{
    class Program
    {
        static void Main(string[] args)
        {
            var Results = TaskAsync.ResultAllTask();  //ValueTuple
            var player = TaskAsync.HelloUser(Results.Item2);
            Start(player);

            TaskAsync.FileOfPoints(player, Results.Item1);
        }

        static void Start(Player player)
        {
            Game.Start();                                              // начинаем игру

            while (!Game.Over)                                         // пока не проиграли
            {
                Current current = Game.AddCurrent();                   // добавляем новый кубик

                while (!current.IsFell)                                // пока кубик не упал
                {
                    Game.Show(player);
                    Thread.Sleep(700);

                    if (Console.KeyAvailable)
                    {
                        ConsoleKey Key = Console.ReadKey(true).Key;

                        switch (Key)
                        {
                            case ConsoleKey.LeftArrow:
                                Move.Left(ref current);
                                break;

                            case ConsoleKey.RightArrow:
                                Move.Right(ref current);
                                break;

                            case ConsoleKey.DownArrow:
                                while (!current.IsFell)
                                {
                                    Move.Down(ref current);

                                    Game.Show(player);
                                    Thread.Sleep(100);
                                }
                                break;

                            case ConsoleKey.Enter:     // пауза
                                Console.ReadLine();
                                break;

                            case ConsoleKey.Backspace:

                                Game.Over = true;
                                Game.Show(player);
                                return;
                        }
                    }

                    Move.Down(ref current);
                }

                while (true)
                {
                    if (!Join.MergerIs()) break;       // проверяем, есть ли объединение

                    Join.Mergering();                  // объединяем

                    Game.Show(player);                 // показываем как объединили
                    Thread.Sleep(300);

                    Join.Down();                       // опускаем кубики

                    Game.Show(player);                 // показываем как опустили все элементы
                    Thread.Sleep(300);
                }

                Game.IsGameOver();
            }

            Game.Show(player);
        }
    }
}
