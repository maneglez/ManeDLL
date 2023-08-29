using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Helpers.Common
{
    public interface ILineaTraspaso : ILineaDocumento
    {
        string FromWhs { get; set; }
        string ToWhs { get; set; }
    }
}
