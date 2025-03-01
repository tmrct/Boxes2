using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes
{
    public class Couleureur : IVisiteur<IBoite>
    {
        private int _indentLevel = -1;
        private readonly ConsoleColor[] _colors = new[]
        {
            ConsoleColor.White,
            ConsoleColor.Red,
            ConsoleColor.Green,
            ConsoleColor.Yellow,
            ConsoleColor.Blue,
            ConsoleColor.Magenta,
            ConsoleColor.Cyan,
        };

        public void Entrer()
        {
            _indentLevel++;
        }

        public void Sortir()
        {
            _indentLevel--;
        }

        public void Visiter(IBoite elem, Action? opt = null)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = _colors[_indentLevel % _colors.Length];

            Console.Write(new string(' ', _indentLevel * 2));

            opt?.Invoke();

            Console.ForegroundColor = originalColor;
        }
    }
}

