using Mane.CFDI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CFDI
{
    public partial class Cfdi : ICfdi
    {
        public Cfdi()
        {
            Addenda = new Addenda();
            CfdiRelacionados = new List<ICfdiRelacionados>();
            Complemento = new Complemento();
            Conceptos = new List<IConcepto>();
            Emisor = new Emisor();
            Impuestos = new Impuestos();
            InformacionGlobal = new InformacionGlobal();
            Receptor = new Receptor();
            Version = CfdiVersion.Desconocida;
        }
        public Addenda Addenda { get; set; }
        public string Certificado { get; set; }
        public List<ICfdiRelacionados> CfdiRelacionados { get; set; }
        public IComplemento Complemento { get; set; }
        public List<IConcepto> Conceptos { get; set; }
        public string CondicionesDePago { get; set; }
        public string Confirmacion { get; set; }
        public decimal Descuento { get; set; }
        public bool DescuentoSpecified { get; set; }
        public IEmisor Emisor { get; set; }
        public string Exportacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Folio { get; set; }
        public string FormaPago { get; set; }
        public IComprobanteImpuestos Impuestos { get; set; }
        public IComprobanteInformacionGlobal InformacionGlobal { get; set; }
        public string LugarExpedicion { get; set; }
        public string MetodoPago { get; set; }
        public string Moneda { get; set; }
        public string NoCertificado { get; set; }
        public IReceptor Receptor { get; set; }
        public string Sello { get; set; }
        public string Serie { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TipoCambio { get; set; }
        public bool TipoCambioSpecified { get; set; }
        public string TipoDeComprobante { get; set; }
        public decimal Total { get; set; }
        public CfdiVersion Version { get; set; }
    }
}
