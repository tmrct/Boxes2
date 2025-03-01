using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes
{
    public class Mesureur : IVisiteur<IBoite>
    {
        public IBoite? Smallest { get; private set; }
        public IBoite? Largest { get; private set; }

        public string PlusPetite => Smallest != null ? new Boite(Smallest).ToString() : "No box found";
        public string PlusGrande => Largest != null ? new Boite(Largest).ToString() : "No box found";

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
            int area = elem.Largeur * elem.Hauteur;

            if (area == 0)
                return;

            if (Smallest == null || (area < Smallest.Largeur * Smallest.Hauteur))
            {
                Smallest = elem.Clone();
            }

            if (Largest == null || (area > Largest.Largeur * Largest.Hauteur))
            {
                Largest = elem.Clone();
            }

            opt?.Invoke();
        }
    }
}
