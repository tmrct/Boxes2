using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes
{
    public interface IVisitable<T>
    {
        void Accepter(IVisiteur<T> viz);
    }
}
