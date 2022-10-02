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
            Program program = new Program();
            program.Start();
        }

        void Start()
        {
            Model model = new Model();

            model.Start();

            while (!model.GameOver())
            {
                //Console.Clear();

                Show(model);

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
                                Show(model);
                                Thread.Sleep(100);
                            }
                            break;

                        case ConsoleKey.Backspace:
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

                        Show(model);                 // показываем как объединили

                        Thread.Sleep(300);

                        model.DownforJoin();

                        Show(model);                 // показываем как опустили все элементы

                        Thread.Sleep(300);
                    }

                    model.NewStart();
                }
            }

            Console.Clear();
            Show(model);
        }

        static void Show(Model model)
        {
            for (int y = 0; y < model.map.GetLength(); y++)
            {
                for (int x = 0; x < model.map.GetWidth(); x++)
                {
                    Console.SetCursorPosition(x * 5 + 5, y * 2 + 2);

                    int number = model.map.Get(x, y);

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
