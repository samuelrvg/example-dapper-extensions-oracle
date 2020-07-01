using System;
using static System.Console;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Linq;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            const int total = 1_000_000;
            var cpf = "111.222.333-44";
            var formatado = "";
            var sw = Stopwatch.StartNew();
            for (var i = 0; i < total; i++)
            {
                formatado = cpf.Substring(0, 3) + cpf.Substring(4, 3) + cpf.Substring(8, 3) + cpf.Substring(12, 2);
            }
            sw.Stop();
            WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            for (var i = 0; i < total; i++)
            {
                formatado = string.Concat(cpf.AsSpan(0, 3), cpf.AsSpan(4, 3), cpf.AsSpan(8, 3), cpf.AsSpan(12, 2));
            }
            sw.Stop();
            WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            var position = GetNumberPosition(cpf);
            var number = int.Parse(cpf.AsSpan(position.Start, position.Length));
            sw.Stop();
            WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            for (var i = 0; i < total; i++)
            {
                formatado = cpf[0..3] + cpf[4..7] + cpf[8..11] + cpf[12..14];
            }
            sw.Stop();
            WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            for (var i = 0; i < total; i++)
            {
                formatado = String.Join("", Regex.Split(cpf, @"[^\d]"));
            }
            sw.Stop();
            WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            for (var i = 0; i < total; i++)
            {
                Regex r = new Regex(@"\d+");
                var result = "";
                foreach (Match m in r.Matches(cpf)) result += m.Value;
                formatado = result;
            }
            sw.Stop();
            WriteLine(sw.ElapsedMilliseconds);
            sw.Restart();
            for (var i = 0; i < total; i++)
            {
                formatado = string.Join("", cpf.ToCharArray().Where(Char.IsDigit));
            }
            sw.Stop();
            WriteLine(sw.ElapsedMilliseconds);

        }

        private static (int Start, int Length) GetNumberPosition(string s)
        {
            var start = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (char.IsDigit(s[i]))
                {
                    start = i;
                    break;
                }
            }

            for (int i = start + 1; i < s.Length; i++)
            {
                if (!char.IsDigit(s[i]))
                {
                    return (start, i - start);
                }
            }

            return (start, s.Length - start);
        }
    }
}
