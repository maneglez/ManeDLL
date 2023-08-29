using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Helpers.Common.Interfaces
{
    public interface IDocumento
    {
        int DocEntry { get; set; }
        int DocNum { get; set; }
        string CardCode { get; set; }
        string CardName { get; set; }
        DateTime DocDate { get; set; }
        string Comments { get; set; }
        List<ILineaDocumento> Lines { get; set; }
    
    }
}
