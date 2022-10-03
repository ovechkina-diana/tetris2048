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
            Game.Start();                                              // начинаем игру

            while (!Game.Over)                                         // пока не проиграли
            {
                Current current = Game.AddCurrent();                   // добавляем новый кубик

                while (!current.IsFell)                                // пока кубик не упал
                {
                    Game.Show();
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

                                    Game.Show();
                                    Thread.Sleep(100);
                                }
                                break;

                            case ConsoleKey.Spacebar:
                                return;
                        }
                    }

                    Move.Down(ref current);
                }

                while (true)
                {
                    bool JoinIs = Join.MergeCubes();

                    if (!JoinIs) break;

                    Game.Show();                 // показываем как объединили
                    Thread.Sleep(300);

                    Join.Down();

                    Game.Show();                 // показываем как опустили все элементы
                    Thread.Sleep(300);
                }

                Game.IsGameOver();
            }

            Console.Clear();
            Game.Show();
        }
    }
}
