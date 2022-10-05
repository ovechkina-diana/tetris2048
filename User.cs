using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris2048
{
    class User
    {
        public User() { }

        public User(string nik, string password)
        {
            Nik = nik;
            Password = password;
        }

        public User(string fn, string ph, string nik, string password)
        {
            FullName = fn;
            Phone = ph;
            Nik = nik;
            Password = password;
        }
        public string FullName { get; init; }
        public string Nik { get; init; }//
        public string Phone { get; init; }//TODO email+
        public string Password { get; init; }


        public virtual Player LogIn()//(string FullN, string Ph, string Nik, string Pass)
        {
            Console.WriteLine("Введите ФИО"); var fullname = Console.ReadLine();
            Console.WriteLine("Введите номер телефона"); var phone = Console.ReadLine();
            Console.WriteLine("Придумайте ник"); var nik = Console.ReadLine();
            Console.WriteLine("Придумайте пароль"); var password = Console.ReadLine();

            var newuser = new Player(fullname, phone, nik, password)
            {
                FullName = fullname,
                Phone = phone,
                Nik = nik,
                Password = password,
                Points = 0,
                Rating = 0
            };
            //var  pl = (Player)newuser;
            return newuser;
        }
    }
}
