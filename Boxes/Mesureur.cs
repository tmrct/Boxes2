using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boites
{
    public class Mesureur : IVisiteur<IBoite>
    {
        public IBoite? BoiteLaPlusPetite { get; private set; }
        public IBoite? BoiteLaPlusGrande { get; private set; }

        public string PlusPetite => BoiteLaPlusPetite != null ? new Boite(BoiteLaPlusPetite).ToString() : "No box found";
        public string PlusGrande => BoiteLaPlusGrande != null ? new Boite(BoiteLaPlusGrande).ToString() : "No box found";

        public void Entrer()
        {
            // Nothing to do when entering
        }

        public void Sortir()
        {
            // Nothing to do when exiting
        }

        public void Visiter(IBoite elem, Action? opt = null)
        {
            int surface = elem.Largeur * elem.Hauteur;

            if (surface == 0)
                return;

            if (BoiteLaPlusPetite == null || (surface < BoiteLaPlusPetite.Largeur * BoiteLaPlusPetite.Hauteur))
            {
                BoiteLaPlusPetite = elem.Clone();
            }

            if (BoiteLaPlusGrande == null || (surface > BoiteLaPlusGrande.Largeur * BoiteLaPlusGrande.Hauteur))
            {
                BoiteLaPlusGrande = elem.Clone();
            }

            //opt?.Invoke();
        }
    }
}
