using System.Collections;
using System.Collections.Generic;

namespace Boites
{
    class Boite : IBoite
    {
        public int Hauteur { get; set; }
        public int Largeur { get; set; }

        public IBoite? _Boite;
        public Boite()
        {
            _Boite = new MonoBoite();
            Hauteur = _Boite.Hauteur;
            Largeur = _Boite.Largeur;
        }

        public Boite(string texte)
        {
            _Boite = new MonoBoite(texte);
            Hauteur = _Boite.Hauteur;
            Largeur = _Boite.Largeur;
        }

        public Boite(Boite boite)
        {
            _Boite = boite._Boite.Clone();
            Hauteur = boite.Hauteur;
            Largeur = boite.Largeur;
        }


        public Boite(IBoite boite)
        {
            _Boite = boite.Clone();
            Hauteur = _Boite.Hauteur;
            Largeur = _Boite.Largeur;
        }

        public override string ToString()
        {
            string res = $"+{new string('-', Largeur)}+\r\n";

            foreach (string s in _Boite)
            {
                if (s != "") 
                    res += $"|{s.PadRight(Largeur)}|\n";
            }

            res += $"+{new string('-', Largeur)}+";

            return res;
        }
        public IBoite Clone() => new Boite(this);

        public IEnumerator<string> GetEnumerator() => _Boite.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _Boite.GetEnumerator();

        public void Redimensionner(int largeur, int hauteur)
        {
            Largeur = largeur;
            Hauteur = hauteur;
            _Boite.Redimensionner(largeur, hauteur);
        }
        public void Accepter(IVisiteur<IBoite> visiteur)
        {
            visiteur.Entrer();
            visiteur.Visiter(this, () => Console.WriteLine($"Boite"));

            if (_Boite is IVisitable<IBoite> visitable)
            {
                visitable.Accepter(visiteur);
            }

            visiteur.Sortir();
        }
    }
}

