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
	public enum Colors
        {
            Blue = 2,
            Red = 4,
            Yellow = 8,
            Magenta = 16,
            Green = 32,
            Cyan = 64,
            Black = 128,
            DarkBlue = 256,
            DarkRed = 512,
            DarkYellow = 1024,
            DarkMagenta = 2048,
            DarkGreen = 4096,
            DarkCyan = 8192,
            //Grey = 16384,
            //DarkGrey = 32768
        }

        static void Show(Model model)
        {
            for (int y = 0; y < model.map.GetLength(); y++)
            {
                for (int x = 0; x < model.map.GetWidth(); x++)
                {
                    Console.SetCursorPosition(x * 5 + 5, y * 2 + 2);
                    ConsoleColor color;
                    int number = model.map.Get(x, y);
                    if (number != 0)
                    {
                        color = GetColor(number);
                        Out(color, number);
                    }
                    else
                        Console.Write(" . ");
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
        static ConsoleColor GetColor(int x)
        {
            var t = Enum.GetName(typeof(Colors), x);
            int temp = (int)Enum.Parse(typeof(ConsoleColor), t);
            return (ConsoleColor)temp;
        }
        private static void Out(ConsoleColor color, int x)
        {
            Console.ForegroundColor = color;
            Console.Write(x.ToString() + "  ");
            Console.ResetColor();
        }
    }
}