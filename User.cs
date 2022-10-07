using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace tetris2048
{
    class User
    {
        public string FullName { get; set; }
        public string Nik { get; set; }//
        public string Phone { get; set; }//TODO email+
        public string Password { get; set; }
        public User() { }
        public virtual void LogIn()
        {
            string patternForFullName = @"[A-ZА-Я][a-zа-я]+\s[A-ZА-Я][a-zа-я]+\s[A-ZА-Я][a-zа-я]+";
            string patternForPhone = @"[8|+7]\d{10}";
            Regex rgFullName = new Regex(patternForFullName);
            Regex rgPhone = new Regex(patternForPhone);

            var rndNikAndPass = Guid.NewGuid().ToString().Split('-');

            Console.WriteLine("Введите ФИО"); var fullName = Console.ReadLine();
            Match match = rgFullName.Match(fullName); if (!match.Success) throw new Exception("Неверный ввод ФИО");

            Console.WriteLine("Введите номер телефона"); var number = Convert.ToInt64(Console.ReadLine());
            match = rgPhone.Match(number.ToString()); if (!match.Success) throw new Exception();

            var phone = string.Format("{0:# (###) ###-##-##}", number);


            Console.WriteLine("Ваш уникальный ник "); var nik = rndNikAndPass[0].ToString().ToUpper(); Console.WriteLine(nik);
            Console.WriteLine("Ваш уникальный пароль"); var password = rndNikAndPass[1].ToString(); Console.WriteLine(password);

            FullName = fullName;
            Phone = phone;
            Nik = nik;
            Password = password;
        }
    }
}
