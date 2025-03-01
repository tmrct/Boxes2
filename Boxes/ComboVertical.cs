using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Boites
{
    enum Position { Top, Middle, Bottom, End }

    internal class ComboVertical : IBoite
    {
        public int Hauteur { get; set; }
        public int Largeur { get; set; }
        private IBoite BoiteHaut { get; set; }
        private IBoite BoiteBas { get; set; }

        public ComboVertical(IBoite boiteHaut, IBoite boiteBas)
        {
            Largeur = Math.Max(boiteHaut.Largeur, boiteBas.Largeur);
            Hauteur = boiteHaut.Hauteur + boiteBas.Hauteur + 1;

            BoiteHaut = boiteHaut.Clone();
            BoiteBas = boiteBas.Clone();

            BoiteHaut.Redimensionner(Largeur, BoiteHaut.Hauteur);
            BoiteBas.Redimensionner(Largeur, BoiteBas.Hauteur);
        }

        public IEnumerator<string> GetEnumerator() => new ComboVerticalEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IBoite Clone() => new ComboVertical(BoiteHaut, BoiteBas);

        public void Redimensionner(int largeur, int hauteur)
        {
            Largeur = largeur;
            Hauteur = hauteur;
            BoiteHaut.Redimensionner(Largeur, BoiteHaut.Hauteur);
            BoiteBas.Redimensionner(Largeur, Hauteur - BoiteHaut.Hauteur - 1);
        }
        public void Accepter(IVisiteur<IBoite> visiteur)
        {
            visiteur.Entrer();
            visiteur.Visiter(this, () => Console.WriteLine("ComboVertical"));

            // Visit the top and bottom boxes
            if (BoiteHaut is IVisitable<IBoite> top)
            {
                top.Accepter(visiteur);
            }

            if (BoiteBas is IVisitable<IBoite> bottom)
            {
                bottom.Accepter(visiteur);
            }

            visiteur.Sortir();
        }

        private class ComboVerticalEnumerator : IEnumerator<string>
        {
            private IEnumerator<string> EnumTop { get; set; }
            private IEnumerator<string> EnumBottom { get; set; }
            private Position _Position { get; set; }
            private ComboVertical Cv { get; set; }

            public ComboVerticalEnumerator(ComboVertical cv)
            {
                EnumTop = cv.BoiteHaut.GetEnumerator();
                EnumBottom = cv.BoiteBas.GetEnumerator();
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
