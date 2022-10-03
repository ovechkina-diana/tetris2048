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
            Console.WriteLine("Создайте свой ник ");
            var newplayer = new Player
            {
                Nik = Console.ReadLine(),
            };
            Console.Clear();

            var result = ReaderFromFileAsync(newplayer);
            Start(newplayer, result.Result);
            SizingUp(newplayer);
        }

        static async Task<List<(int Rating, string Nik, int Points)>> ReaderFromFileAsync(Player pl)
        {
            var result = await Task.Run(() => ReaderFromFile(pl));
            return result;
        }
        static List<(int Rating, string Nik, int Points)> ReaderFromFile(Player pl)
        {
            var ListPlayers = new List<(int Rating, string Nik, int Points)>();
            using (var sr = new StreamReader(@"C:\Users\myasn\Downloads\tetris.txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    var line = s.Split('.', '-');
                    int.TryParse(line[0], out int R);
                    int.TryParse(line[2], out int P);
                   
                    ListPlayers.Add((R, line[1], P));
                }
            }
            using (var sw = new StreamWriter(@"C:\Users\myasn\Downloads\tetris.txt", false))//check async
            {
                sw.WriteLine(pl.Nik);

            }
            return ListPlayers;
        }
        static int FileOfPoints(Player pl, List<(int Rating, string Nik, int Points)> ListPlayers)
        {
            
            
            ListPlayers.Add((pl.Rating, pl.Nik, pl.Points));

            var ListSortPlayers = (from p in ListPlayers
                                   orderby p.Points descending
                                   select p).ToList();
            
            int position = 1;
            for (var i = 0; i < ListSortPlayers.Count; i++)//change Rating
            {
                var ms = ListSortPlayers[i];
                ms.Rating = position;
                ListSortPlayers[i] = ms;
                position++;
            }

            using (var sw = new StreamWriter(@"C:\Users\myasn\Downloads\tetris.txt", false))
            {

                foreach (var item in ListSortPlayers)
                {
                    sw.WriteLine($"{item.Rating}. {item.Nik} - {item.Points}");

                }
                

            }
            return ListSortPlayers.Find(x => x.Nik == pl.Nik).Rating;
        }

        static void SizingUp(Player newplayer)
        {
            Console.Clear();
            string GameOver = "\n\t\tGame Over";
            Console.WriteLine(GameOver);
            //Console.ForegroundColor = ConsoleColor.Red;
            ////Console.ResetColor();
            Console.WriteLine("\n\t Вы набрали {0} очков", newplayer.Points);
            Console.WriteLine("\n\t Ваше место в рейтинге -  {0} ", newplayer.Rating);
        }

        static void Start(Player player, List<(int Rating, string Nik, int Points)> ListPlayers)
        {
            Model model = new Model();
            model.Start();
            while (!model.GameOver())
            {
                //Console.Clear();

                Show(model, player, ListPlayers);

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
                                Show(model, player, ListPlayers);
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
                        Show(model, player, ListPlayers);                 // показываем как объединили
                        Thread.Sleep(100);
                        model.method();
                        Show(model, player, ListPlayers);                 // показываем как опустили все элементы
                        Thread.Sleep(100);
                    }
                    model.NewStart();
                }
            }
            Console.Clear();
            Show(model, player, ListPlayers);
        }


        static void Show(Model model, Player player, List<(int Rating, string Nik, int Points)> ListPlayers)
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
                        //Console.ForegroundColor = (ConsoleColor)15;
                    }
                    else
                    {
                        //Console.ForegroundColor = (ConsoleColor)rnd.Next(1, 14);
                        Console.Write(number.ToString());

                        // Console.ForegroundColor = (ConsoleColor)(15);
                    }
                    //Console.Write(number == 0 ? "." : number.ToString() + "  ");


                }
                if (y == 1)
                    Console.Write("-------");
            }
            Console.WriteLine();
            if (model.GameOver())
            {
                Console.WriteLine("GameOver");
                player.Points = model.points;
                var result = FileOfPoints(player, ListPlayers);
                player.Rating = result;
                SizingUp(player);
            }

            else
                Console.WriteLine("Still play");
        }


    }
}