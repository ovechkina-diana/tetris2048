using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tetris2048
{
    static class TaskAsync
    {
        static List<Task> tasks = new List<Task>();

        public static Player HelloUser(List<User> ListUsers)
        {
            var player = new Player();
            Console.WriteLine("Вы зарегистрированы? Введите Да - 1/Нет - 2"); var answer = Convert.ToInt32(Console.ReadLine());
            if (answer == 1)
            {
                player.LogIn();
                var resultFindIndex = ListUsers.FindIndex(x => x.Nik == player.Nik && x.Password == player.Password);

                if (resultFindIndex == -1)
                    throw new Exception();
                else
                {
                    player.FullName = ListUsers[resultFindIndex].FullName;
                    player.Phone = ListUsers[resultFindIndex].Phone;
                }

            }
            else if (answer == 2)//add user to DataBase
            {
                var user = new User();
                user.LogIn();
                player = new Player(user.FullName, user.Phone, user.Nik, user.Password);

                WriterUserToDataBase(player, ListUsers.Count);
                ListUsers.Add(player);
                Thread.Sleep(1500);
            }
            else throw new Exception();
            Console.Clear();
            return player;
        }

        public static (List<Player>, List<User>) ResultAllTask()
        {
            var result1 = ReaderFromFileAsync();
            var result2 = ReaderAllUsersInDataBaseAsync();
            return (result1.Result, result2.Result);
        }
        static async Task<List<Player>> ReaderFromFileAsync()
        {          
            var result = await Task.Run(() => ReaderFromFile());
            return result;
        }
        static async Task<List<User>> ReaderAllUsersInDataBaseAsync()
        {
            var result = await Task.Run(() => ReaderAllUsersInDataBase());
            return result;
        }
        public static List<User> ReaderAllUsersInDataBase()
        {
            var ListUsers = new List<User>();
            using (var sr = new StreamReader(@"C:\Users\myasn\Downloads\DataBase.txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    var line = s.Split('.', ',', '|', ',');
                    string fullName = line[1].Replace(" ", "");
                    string phone = line[2].Replace(" ", "");
                    string nik = line[3].Replace(" ", "");
                    string pass = line[4].Replace(" ", "");
                    ListUsers.Add(new User { FullName = fullName, Phone = phone, Nik = nik, Password = pass });
                }
            }
            return ListUsers;
        }
        public static void WriterUserToDataBase(Player player, int countUsers)
        {
            using (var tw = File.AppendText(@"C:\Users\myasn\Downloads\DataBase.txt"))
            {
                tw.WriteLine("");
                tw.Write($"{countUsers + 1}. {player.FullName}, {player.Phone} | {player.Nik}, {player.Password}");
            }
        }


        public static List<Player> ReaderFromFile()
        {
            var ListPlayers = new List<Player>();

            using (var sr = new StreamReader(@"C:\Users\myasn\Downloads\tetris.txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    var line = s.Split('.', '-');
                    string N = line[1];
                    int.TryParse(line[0], out int R);
                    int.TryParse(line[2], out int P);
                    ListPlayers.Add(new Player { Rating = R, Nik = N.Trim(), Points = P });
                }
            }
            return ListPlayers;
        }
        public static void FileOfPoints(Player player, List<Player> ListPlayers)
        {
            var resultIndex = ListPlayers.FindIndex(x => x.Nik == player.Nik);
            if (resultIndex == -1)
                ListPlayers.Add(new Player { Rating = player.Rating, Nik = player.Nik, Points = player.Points });//add my new player
            else
            {
                ListPlayers[resultIndex] = player;
            }
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
            player.Rating = ListSortPlayers.Find(x => x.Nik == player.Nik).Rating;
            SizingUp(player);

        }
        static void SizingUp(Player newplayer)
        {
            Console.Clear();
            string GameOver = "\n\t\tGame Over";
            Console.WriteLine(GameOver);

            Console.WriteLine("\n\t\t Вы набрали {0} очков", newplayer.Points);
            Console.WriteLine("\n\t Ваше место в рейтинге -  {0} ", newplayer.Rating);
        }
    }
}
