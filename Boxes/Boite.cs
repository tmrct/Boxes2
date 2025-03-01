using System.Collections;
using System.Collections.Generic;

namespace Boxes
{
    class Boite : IBoite
    {
        public int Hauteur { get; set; }
        public int Largeur { get; set; }

        public IBoite? _Box;
        public Boite()
        {
            _Box = new MonoBoite();
            Hauteur = _Box.Hauteur;
            Largeur = _Box.Largeur;
        }

        public Boite(string text)
        {
            _Box = new MonoBoite(text);
            Hauteur = _Box.Hauteur;
            Largeur = _Box.Largeur;
        }

        public Boite(Boite boite)
        {
            _Box = boite._Box.Clone();
            Hauteur = boite.Hauteur;
            Largeur = boite.Largeur;
        }


        public Boite(IBoite boite)
        {
            _Box = boite.Clone();
            Hauteur = _Box.Hauteur;
            Largeur = _Box.Largeur;
        }

        public override string ToString()
        {
            string res = $"+{new string('-', Largeur)}+\r\n";

            foreach (string s in _Box)
            {
                if (s != "") 
                    res += $"|{s.PadRight(Largeur)}|\n";
            }

            res += $"+{new string('-', Largeur)}+";

            return res;
        }
        public IBoite Clone() => new Boite(this);

        public IEnumerator<string> GetEnumerator() => _Box.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _Box.GetEnumerator();

        public void Redimensionner(int width, int height)
        {
            Largeur = width;
            Hauteur = height;
            _Box.Redimensionner(width, height);
        }
        public void Accepter(IVisiteur<IBoite> visitor)
        {
            visitor.Entrer();
            visitor.Visiter(this, () => Console.WriteLine($"Boite"));

            if (_Box is IVisitable<IBoite> visitable)
            {
                visitable.Accepter(visitor);
            }

            visitor.Sortir();
        }
    }
}

