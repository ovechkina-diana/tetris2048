using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace Game2048
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Создайте свой ник ");
            var player = new Player();
            var user = new User();
            //{
            //    Nik = Console.ReadLine(),
            //};

            var result = ReaderFromFileAsync();
            Console.WriteLine("Вы зарегистрированы? Введите Да - 1/Нет - 2"); var answer = Convert.ToInt32(Console.ReadLine());
            if (answer == 1)
            {
                player = player.LogIn();
                if (!player.CheckInData(result.Result, player)) //Console.WriteLine("Nik not found");
                    throw new Exception();

            }
            else if (answer == 2) player = user.LogIn();
            else Console.WriteLine("Err");//todo
            Console.Clear();
            Start(player);

            FileOfPoints(player, result.Result);
        }

        static bool CheckTask(Task t)
        {
            if (t.Status == TaskStatus.RanToCompletion) return true;
            return false;
        }

        static async Task<List<(int Rating, string Nik, int Points)>> ReaderFromFileAsync()
        {

            // await Task.Delay(5000);
            var result = await Task.Run(() => ReaderFromFile());


            //await Task.Delay(5000);
            return result;
        }

        static List<(int Rating, string Nik, int Points)> ReaderFromFile()
        {
            var ListPlayers = new List<(int Rating, string Nik, int Points)>();

            using (var sr = new StreamReader(@"C:\Users\Татьяна Гриценко\tetris.txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    var line = s.Split('.', '-');
                    string N = line[1];
                    int.TryParse(line[0], out int R);
                    int.TryParse(line[2], out int P);
                    //ListPlayers.Add(new Player { Rating = R, Nik = line[1], Points = P });
                    ListPlayers.Add((R, N, P));
                }
            }
            using (var sw = new StreamWriter(@"C:\Users\Татьяна Гриценко\tetris.txt", false))//check async
            {
                sw.WriteLine(ListPlayers[0].Nik);

            }
            return ListPlayers;
        }
        static void FileOfPoints(Player pl, List<(int Rating, string Nik, int Points)> ListPlayers)
        {

            //ListPlayers.Add(new Player { Rating = pl.Rating, Nik = pl.Nik, Points = pl.Points});
            ListPlayers.Add((pl.Rating, pl.Nik, pl.Points));//add my new player

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

            using (var sw = new StreamWriter(@"C:\Users\Татьяна Гриценко\tetris.txt", false))
            {

                foreach (var item in ListSortPlayers)
                {
                    sw.WriteLine($"{item.Rating}. {item.Nik} - {item.Points}");

                }


            }
            pl.Rating = ListSortPlayers.Find(x => x.Nik == pl.Nik).Rating;
            SizingUp(pl);

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

                    Game.Show(player);                 // показываем как объединили
                    Thread.Sleep(300);

                    Join.Down();

                    Game.Show(player);                 // показываем как опустили все элементы
                    Thread.Sleep(300);
                }

                Game.IsGameOver();
            }

            Console.Clear();
            Game.Show(player);
        }
    }
}