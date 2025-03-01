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

        public string PlusPetite => BoiteLaPlusPetite != null ? new Boite(BoiteLaPlusPetite).ToString() : "Pas de boite trouvée";
        public string PlusGrande => BoiteLaPlusGrande != null ? new Boite(BoiteLaPlusGrande).ToString() : "Pas de boite trouvée";

        public void Entrer() { }

        public void Sortir() { }

        public void Visiter(IBoite elem, Action? opt = null)
        {
            int surface = elem.Largeur * elem.Hauteur;

            if (BoiteLaPlusPetite == null || (surface < BoiteLaPlusPetite.Largeur * BoiteLaPlusPetite.Hauteur))
            {
                BoiteLaPlusPetite = elem.Clone();
            }

            if (BoiteLaPlusGrande == null || (surface > BoiteLaPlusGrande.Largeur * BoiteLaPlusGrande.Hauteur))
            {
                BoiteLaPlusGrande = elem.Clone();
            }
        }
    }
}
