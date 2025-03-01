using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Boxes
{
    enum Position { Top, Middle, Bottom, End }

    internal class ComboVertical : IBoite
    {
        public int Hauteur { get; set; }
        public int Largeur { get; set; }
        private IBoite TopBoite { get; set; }
        private IBoite BottomBoite { get; set; }

        public ComboVertical(IBoite top, IBoite bottom)
        {
            Largeur = Math.Max(top.Largeur, bottom.Largeur);
            Hauteur = top.Hauteur + bottom.Hauteur + 1; // +1 pour ligne separateur

            TopBoite = top.Clone();
            BottomBoite = bottom.Clone();

            TopBoite.Redimensionner(Largeur, TopBoite.Hauteur);
            BottomBoite.Redimensionner(Largeur, BottomBoite.Hauteur);
        }

        public IEnumerator<string> GetEnumerator() => new ComboVerticalEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IBoite Clone() => new ComboVertical(TopBoite, BottomBoite);

        public void Redimensionner(int width, int height)
        {
            Largeur = width;
            Hauteur = height;
            TopBoite.Redimensionner(Largeur, TopBoite.Hauteur);
            BottomBoite.Redimensionner(Largeur, Hauteur - TopBoite.Hauteur - 1);
        }
        public void Accepter(IVisiteur<IBoite> visitor)
        {
            visitor.Entrer();
            visitor.Visiter(this, () => Console.WriteLine("ComboVertical"));

            // Visit the top and bottom boxes
            if (TopBoite is IVisitable<IBoite> top)
            {
                top.Accepter(visitor);
            }

            if (BottomBoite is IVisitable<IBoite> bottom)
            {
                bottom.Accepter(visitor);
            }

            visitor.Sortir();
        }

        private class ComboVerticalEnumerator : IEnumerator<string>
        {
            private IEnumerator<string> EnumTop { get; set; }
            private IEnumerator<string> EnumBottom { get; set; }
            private Position _Position { get; set; }
            private ComboVertical Cv { get; set; }

            public ComboVerticalEnumerator(ComboVertical cv)
            {
                EnumTop = cv.TopBoite.GetEnumerator();
                EnumBottom = cv.BottomBoite.GetEnumerator();
                Cv = cv;
                _Position = Position.Top;
            }

            public string Current 
            { 
                get 
                {
                    switch (_Position)
                    {
                        case Position.Top:
                            return EnumTop.Current.PadRight(Cv.Largeur, ' ');
                        case Position.Middle:
                            return new string('-', Cv.Largeur);
                        case Position.Bottom:
                            return EnumBottom.Current.PadRight(Cv.Largeur, ' ');
                        case Position.End:
                            return new string(' ', Cv.Largeur);
                        default: return "";
                    }
                } 
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                switch (_Position)
                {
                    case Position.Top:
                        if(!EnumTop.MoveNext())
                            _Position = Position.Middle;
                        return true;
                    case Position.Middle:
                        _Position = Position.Bottom;
                        EnumBottom.MoveNext();
                        return true;
                    case Position.Bottom:
                        return EnumBottom.MoveNext();
                    case Position.End:
                        return false;
                    default: return false;
                }
            }

            public void Reset() { }

            public void Dispose() { }
        }
    }
}
