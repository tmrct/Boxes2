using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes
{
    public interface IBoite : IEnumerable<string>, IVisitable<IBoite>
    {
        int Hauteur { get; set; }
        int Largeur { get; set; }
        IBoite Clone();
        void Redimensionner(int width, int height);
    }
}
