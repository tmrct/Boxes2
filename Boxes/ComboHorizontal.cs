using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Boites
{
    internal class ComboHorizontal : IBoite
    {
        public int Hauteur { get; set; }
        public int Largeur { get; set; }
        private IBoite BoiteGauche { get; set; }
        private IBoite BoiteDroite { get; set; }

        public ComboHorizontal(IBoite gauche, IBoite droite)
        {
            Largeur = gauche.Largeur + droite.Largeur + 1;
            Hauteur = Math.Max(gauche.Hauteur, droite.Hauteur);

            BoiteGauche = gauche.Clone();
            BoiteDroite = droite.Clone();

            BoiteGauche.Redimensionner(BoiteGauche.Largeur, Hauteur);
            BoiteDroite.Redimensionner(BoiteDroite.Largeur, Hauteur);
        }

        public IEnumerator<string> GetEnumerator() => new ComboHorizontalEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IBoite Clone() => new ComboHorizontal(BoiteGauche, BoiteDroite);

        public void Redimensionner(int largeur, int hauteur)
        {
            Largeur = largeur;
            Hauteur = hauteur;
            BoiteGauche.Redimensionner(BoiteGauche.Largeur, Hauteur);
            BoiteDroite.Redimensionner(Largeur - BoiteGauche.Largeur - 1, Hauteur);
        }
        public void Accepter(IVisiteur<IBoite> visiteur)
        {
            visiteur.Entrer();
            visiteur.Visiter(this, () => Console.WriteLine("ComboHorizontal"));

            // Visit the gauche and droite boxes
            if (BoiteGauche is IVisitable<IBoite> gauche)
            {
                gauche.Accepter(visiteur);
            }

            if (BoiteDroite is IVisitable<IBoite> droite)
            {
                droite.Accepter(visiteur);
            }

            visiteur.Sortir();
        }

        private class ComboHorizontalEnumerator : IEnumerator<string>
        {
            private IEnumerator<string> GaucheBoxEnum;
            private IEnumerator<string> DroiteBoxEnum;
            private ComboHorizontal Ch { get; set; }

            public ComboHorizontalEnumerator(ComboHorizontal ch)
            {
                GaucheBoxEnum = ch.BoiteGauche.GetEnumerator();
                DroiteBoxEnum = ch.BoiteDroite.GetEnumerator();
                Ch = ch;
            }

            public string Current
            {
                get
                {
                    string gauche = GaucheBoxEnum.Current ?? new string(' ', Ch.BoiteGauche.Largeur);
                    string droite = DroiteBoxEnum.Current ?? new string(' ', Ch.BoiteDroite.Largeur);
                    gauche = gauche.PadRight(Ch.BoiteGauche.Largeur);
                    if (gaucheFini)
                        gauche = new string(' ', Ch.BoiteGauche.Largeur);
                    if (droiteFini)
                        droite = new string(' ', Ch.BoiteDroite.Largeur);
                    return gauche + "|" + droite;
                }
            }


            object IEnumerator.Current => Current;
            private bool gaucheFini = false;
            private bool droiteFini = false;
            private int compteur = 0;
            public bool MoveNext()
            {
                gaucheFini = !GaucheBoxEnum.MoveNext();
                droiteFini = !DroiteBoxEnum.MoveNext();

                compteur++;

                return compteur <= Ch.Hauteur;
            }

            public void Reset()
            {
                GaucheBoxEnum.Reset();
                DroiteBoxEnum.Reset();
            }

            public void Dispose() { }
        }
    }
}