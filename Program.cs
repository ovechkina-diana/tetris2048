using System;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            Model model = new Model();
            model.Start();
            while (!model.GameOver())
            {
                //Console.Clear();

                Show(model, player);

                Thread.Sleep(700);

                bool EndCurrent = false; // если EndCurrent == true значит текущий кубик упал

                if (Console.KeyAvailable)
                {
                    ConsoleKey Key = Console.ReadKey(true).Key;
                    switch (Key)
                    {
                        case ConsoleKey.LeftArrow:
                            model.Left();
                            break;

                        case ConsoleKey.RightArrow:
                            model.Right();
                            break;

                        case ConsoleKey.DownArrow:
                            while (!EndCurrent)
                            {
                                EndCurrent = model.Down();
                                //Console.Clear();
                                Show(model, player);
                                Thread.Sleep(100);
                            }
                            break;

                        case ConsoleKey.Escape:
                            return;
                    }
                }
                EndCurrent = model.Down();
                if (EndCurrent)
                {
                    while (true)
                    {
                        bool JoinIs = model.Join();
                        if (!JoinIs) break;
                        Show(model, player);                 // показываем как объединили
                        Thread.Sleep(100);
                        model.method();
                        Show(model, player);                 // показываем как опустили все элементы
                        Thread.Sleep(100);
                    }
                    model.NewStart();
                }
            }
            Console.Clear();
            Show(model, player);
        }

        static void Show(Model model, Player player)
        {

            for (int y = 0; y < model.map.GetLength(); y++)
            {
                for (int x = 0; x < model.map.GetWidth(); x++)
                {
                    Console.SetCursorPosition(x * 5 + 5, y * 2 + 2);

                    int number = model.map.Get(x, y);
                    if (number == 0)
                    {
                        Console.Write(".");

                    }
                    else
                    {

                        Console.Write(number.ToString());


                    }


                }
                if (y == 1)
                    Console.Write("-------");
            }
            Console.WriteLine();
            if (model.GameOver())
            {
                Console.WriteLine("GameOver");
                player.Points = model.points;
                Thread.Sleep(800);
            }
            else
                Console.WriteLine("Still play");
        }
    }
}