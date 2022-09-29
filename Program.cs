using System;
using System.Collections.Generic;
using System.Linq;
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
            Model model = new Model(5, 8);
            model.Start();
            Current current = new Current();
            while (true)
            {
                Show(model);
                switch (Console.ReadKey(false).Key)
                {
                    case ConsoleKey.LeftArrow: model.Left(current); break;
                    case ConsoleKey.RightArrow: model.Right(current); break;
                    case ConsoleKey.DownArrow: model.Down(current); break;
                }
            }
        }

        void Show(Model model)
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
