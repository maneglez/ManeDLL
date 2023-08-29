using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Helpers.Common.Interfaces
{
    public interface ITraspaso : IDocumento
    {
        string FromWhs { get; set; }
        string ToWhs { get; set; }
    }
}
