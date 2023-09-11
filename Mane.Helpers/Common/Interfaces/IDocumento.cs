using System;
using System.Collections.Generic;

namespace Mane.Helpers.Common
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
