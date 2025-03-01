using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boites
{
    public class Couleureur : IVisiteur<IBoite>
    {
        private int indentation = -1;
        private readonly ConsoleColor[] _couleurs = new[]
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
            indentation++;
        }

        public void Sortir()
        {
            indentation--;
        }

        public void Visiter(IBoite elem, Action? opt = null)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = _couleurs[indentation % _couleurs.Length];

            Console.Write(new string(' ', indentation * 2));

            opt?.Invoke();

            Console.ForegroundColor = originalColor;
        }
    }
}

