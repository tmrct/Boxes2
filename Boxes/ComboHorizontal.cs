using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Boxes
{
    internal class ComboHorizontal : IBoite
    {
        public int Hauteur { get; set; }
        public int Largeur { get; set; }
        private IBoite LeftBoite { get; set; }
        private IBoite RightBoite { get; set; }

        public ComboHorizontal(IBoite left, IBoite right)
        {
            Largeur = left.Largeur + right.Largeur + 1;
            Hauteur = Math.Max(left.Hauteur, right.Hauteur);

            LeftBoite = left.Clone();
            RightBoite = right.Clone();

            LeftBoite.Redimensionner(LeftBoite.Largeur, Hauteur);
            RightBoite.Redimensionner(RightBoite.Largeur, Hauteur);
        }

        public IEnumerator<string> GetEnumerator() => new ComboHorizontalEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IBoite Clone() => new ComboHorizontal(LeftBoite, RightBoite);

        public void Redimensionner(int width, int height)
        {
            Largeur = width;
            Hauteur = height;
            LeftBoite.Redimensionner(LeftBoite.Largeur, Hauteur);
            RightBoite.Redimensionner(Largeur - LeftBoite.Largeur - 1, Hauteur);
        }
        public void Accepter(IVisiteur<IBoite> visitor)
        {
            visitor.Entrer();
            visitor.Visiter(this, () => Console.WriteLine("ComboHorizontal"));

            // Visit the left and right boxes
            if (LeftBoite is IVisitable<IBoite> left)
            {
                left.Accepter(visitor);
            }

            if (RightBoite is IVisitable<IBoite> right)
            {
                right.Accepter(visitor);
            }

            visitor.Sortir();
        }

        private class ComboHorizontalEnumerator : IEnumerator<string>
        {
            private IEnumerator<string> LeftBoxEnum;
            private IEnumerator<string> RightBoxEnum;
            private ComboHorizontal Ch { get; set; }

            public ComboHorizontalEnumerator(ComboHorizontal ch)
            {
                LeftBoxEnum = ch.LeftBoite.GetEnumerator();
                RightBoxEnum = ch.RightBoite.GetEnumerator();
                Ch = ch;
            }

            public string Current
            {
                get
                {
                    string left = LeftBoxEnum.Current ?? new string(' ', Ch.LeftBoite.Largeur);
                    string right = RightBoxEnum.Current ?? new string(' ', Ch.RightBoite.Largeur);
                    left = left.PadRight(Ch.LeftBoite.Largeur);
                    if (gaucheFini)
                        left = new string(' ', Ch.LeftBoite.Largeur);
                    if (droiteFini)
                        right = new string(' ', Ch.RightBoite.Largeur);
                    return left + "|" + right;
                }
            }


            object IEnumerator.Current => Current;
            private bool gaucheFini = false;
            private bool droiteFini = false;
            private int counter = 0;
            public bool MoveNext()
            {
                gaucheFini = !LeftBoxEnum.MoveNext();
                droiteFini = !RightBoxEnum.MoveNext();

                counter++;

                return counter <= Ch.Hauteur;
            }

            public void Reset()
            {
                LeftBoxEnum.Reset();
                RightBoxEnum.Reset();
            }

            public void Dispose() { }
        }
    }
}