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
        public Player(string fullName, string phone, string nik, string password)
        {
            FullName = fullName;
            Phone = phone;
            Nik = nik;
            Password = password;
        }
        public Player()
        {

        }

        public override void LogIn()
        {
            Console.WriteLine("Введите ник"); var nik = Console.ReadLine();
            Console.WriteLine("Введите пароль"); var password = Console.ReadLine();

            Nik = nik;
            Password = password;
        }
    }
}
