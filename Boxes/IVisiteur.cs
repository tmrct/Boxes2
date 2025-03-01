﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes
{
    public interface IVisiteur<T>
    {
        void Entrer();
        void Sortir();
        void Visiter(T elem, Action opt);
    }
}
