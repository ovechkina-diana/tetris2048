using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris2048
{
    static class Color
    {
        public static ConsoleColor GetColor(int x)
        {
            var t = Enum.GetName(typeof(Colors), x);
            int temp = (int)Enum.Parse(typeof(ConsoleColor), t);
            return (ConsoleColor)temp;
        }

        public static void Out(ConsoleColor color, int x)
        {
            Console.ForegroundColor = color;
            Console.Write(x.ToString() + "  ");
            Console.ResetColor();
        }
    }
}
