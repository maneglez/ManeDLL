using Mane.CFDI.Interfaces;
using System.Collections.Generic;

namespace Mane.CFDI
{

    public class CfdiRelacionados : ICfdiRelacionados
    {
        public List<ICfdiRelacionado> CfdiRelacionado { get; set; }
        public string TipoRelacion { get; set; }
        public CfdiRelacionados()
        {
            CfdiRelacionado = new List<ICfdiRelacionado>();
        }
    }

    public class CfdiRelacionado : ICfdiRelacionado
    {
        public string UUID { get; set; }
    }
}

