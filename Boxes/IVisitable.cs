using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boites
{
    public interface IVisitable<T>
    {
        void Accepter(IVisiteur<T> visiteur);
    }
}
