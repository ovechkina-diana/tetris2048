using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris2048
{
    class User
    {
        public string FullName { get; set; }
        public string Nik { get; set; }//
        public string Phone { get; set; }//TODO email+
        public string Password { get; set; }
        public User()
        {

        }
        public virtual void LogIn()
        {
            var rndNik = Guid.NewGuid().ToString().Split('-');
            // Regex regex = new Regex(@"^[A-ЯЁ][а-яё]+\s[A-ЯЁ][а-яё]+$");
            Console.WriteLine("Введите ФИО"); var fullName = Console.ReadLine();// string name = string.Format((IFormatProvider)regex,fullName);
            Console.WriteLine("Введите номер телефона"); var number = Convert.ToInt64(Console.ReadLine()); string phone = string.Format("{0:# (###) ###-##-##}", number);
            Console.WriteLine("Ваш уникальный ник "); var nik = rndNik[0].ToString().ToUpper(); Console.WriteLine(nik);
            Console.WriteLine("Ваш уникальный пароль"); var password = rndNik[1].ToString(); Console.WriteLine(password);

            FullName = fullName;
            Phone = phone;
            Nik = nik;
            Password = password;

        }

    }
}
