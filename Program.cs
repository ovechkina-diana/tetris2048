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
            Game.Start();                                              // �������� ����

            while (!Game.Over)                                         // ���� �� ���������
            {
                Current current = Game.AddCurrent();                   // ��������� ����� �����

                while (!current.IsFell)                                // ���� ����� �� ����
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

                    Game.Show();                 // ���������� ��� ����������
                    Thread.Sleep(300);

                    Join.Down();

                    Game.Show();                 // ���������� ��� �������� ��� ��������
                    Thread.Sleep(300);
                }

                Game.IsGameOver();
            }

            Console.Clear();
            Game.Show();
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