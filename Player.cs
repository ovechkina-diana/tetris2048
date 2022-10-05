using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris2048
{
    class Player : User
    {
        public int Points { get; set; }
        public int Rating { get; set; }
        public Player(string fn, string ph, string nik, string password) : base(fn, ph, nik, password)
        {
            //FullName = fn;
            //Phone = ph;
            //Nik = nik;
            //Password = password;
            Points = 0;
            Rating = 0;
        }

        public Player() { }

        public Player(string nik, string password) : base(nik, password)
        {
            //Nik = nik;
            //Password = password;
            Points = 0;
            Rating = 0;
        }

        //public (string Nik, string ) CheckOf

        public override Player LogIn()
        {
            Console.WriteLine("Введите ник"); var nik = Console.ReadLine();
            Console.WriteLine("Введите пароль"); var password = Console.ReadLine();

            var player = new Player(nik, password)
            {
                FullName = FullName,
                Phone = Phone,
                Nik = nik,
                Password = password,
                Points = 0,
                Rating = 0
            };
            // pl = player;
            return player;
        }

        public bool CheckInData(List<(int Rating, string Nik, int Points)> ListPlayers, Player player)
        {
            // ListPlayers.Exists(x => x.Nik == player.Nik);

            var resultPl = ListPlayers.Contains((player.Rating, player.Nik, player.Points));
            if (resultPl) return true;
            //else Console.WriteLine("Nik not found");
            return false;
        }
    }
}
