using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boxes
{
    internal interface IBox : IEnumerable<string>
    {
        int Height { get; set; }
        int Width { get; set; }
    }
}
