using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mane.CFDI.CFDI4
{

    [Serializable]
    public class CFDI : Cfdi4Base
    {
        private bool isValid;
        private string rutaXml;
        private string errorMsg;

        public bool IsValid() => isValid;
        protected override void construct()
        {
            Emisor = new Emisor();
            Receptor = new Receptor();
            Conceptos = new Conceptos();
            Impuestos = new Impuestos();
            Complemento = new Complemento();
        }
        public CFDI()
        {
            construct();
        }
        public CFDI(string xmlPath)
        {
            Load(xmlPath);
        }
        public void Load(string xmlPath)
        {
            construct();
            try
            {
                rutaXml = xmlPath;
                var xml = new XmlDocument();
                xml.Load(xmlPath);
                node = xml.FirstChild;
                if (node?.Name != "cfdi:Comprobante")
                {
                    node = node?.NextSibling;
                    if (node?.Name != "cfdi:Comprobante")
                        throw new Exception("El xml no contiene el nodo cfdi:Comprobante");
                }
                nsmgr = new XmlNamespaceManager(xml.NameTable);
                nsmgr.AddNamespace("cfdi", GetAttribute("xmlns:cfdi"));
                SetAttributes();
                isValid = true;

            }
            catch (Exception e)
            {
                isValid = false;
                errorMsg = e.Message;
            }
            GC.Collect();
        }
        public string Version { get; set; }
        public DateTime Fecha { get; set; }
        public string Sello { get; set; }
        public string FormaPago { get; set; }
        public string Folio { get; set; }
        public string NoCertificado { get; set; }
        public string Certificado { get; set; }
        public string Serie { get; set; }
        [DisplayFormat]
        public double SubTotal { get; set; }
        public string Moneda { get; set; }
        [DisplayFormat]
        public double Total { get; set; }
        public string TipoDeComprobante { get; set; }
        public string Exportacion { get; set; }
        public string MetodoPago { get; set; }
        public string LugarExpedicion { get; set; }
        public Emisor Emisor { get; set; }
        public Receptor Receptor { get; set; }
        public Conceptos Conceptos { get; set; }
        public Impuestos Impuestos { get; set; }
        public Complemento Complemento { get; set; }
        

        public string XmlFileName() => rutaXml;
        public string ErrorMsg() => errorMsg;

        #region Propiedades Personalizadas
        public string NombreArchivo => Path.GetFileName(rutaXml);
        #endregion

    }

    public class Emisor : CFDI4SubObj
    {
        public Emisor()
        {
            
        }

        public Emisor(XmlNode node, XmlNamespaceManager nsmgr) : base(node, nsmgr)
        {

        }
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string RegimenFiscal { get; set; }
    }
    public class Receptor : CFDI4SubObj
    {
        public Receptor()
        {
        }

        public Receptor(XmlNode node, XmlNamespaceManager nsmgr) : base(node, nsmgr)
        {

        }
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string DomicilioFiscalReceptor { get; set; }
        public string RegimenFiscalReceptor { get; set; }
        public string UsoCfdi { get; set; }

    }
    public class Concepto : CFDI4SubObj
    {
        protected override void construct()
        {
            Impuestos = new ImpuestosConcepto();
        }
        public Concepto()
        {
            construct();
        }

        public Concepto(XmlNode node, XmlNamespaceManager nsmgr) : base(node, nsmgr)
        {
        }

        public string ClaveProdServ { get; set; }
        public double Cantidad { get; set; }
        public string ClaveUnidad { get; set; }
        public string Unidad { get; set; }
        public string Descripcion { get; set; }
        [DisplayFormat]
        public double ValorUnitario { get; set; }
        [DisplayFormat]
        public double Importe { get; set; }
        public string ObjetoImp { get; set; }
        public ImpuestosConcepto Impuestos { get; set; }

        #region props personalizadas
        [DisplayFormat]
        public double TotalTraslados
        {
            get
            {
                double total = 0;
                foreach (Traslado t in Impuestos.Traslados)
                {
                    total += t.Importe;
                }
                return total;
            }
        }
        [DisplayFormat]
        public double TotalRetenciones
        {
            get
            {
                double total = 0;
                foreach (Retencion r in Impuestos.Retenciones)
                {
                    total += r.Importe;
                }
                return total;

            }
        }
        #endregion

    }
    //clase impuesto de concepto cfdi
    public class ImpuestoBase : CFDI4SubObj
    {
        public ImpuestoBase()
        {

        }
        public ImpuestoBase(XmlNode node, XmlNamespaceManager nsmgr) : base(node, nsmgr)
        {

        }
        [DisplayFormat]
        public double Base { get; set; }
        public string Impuesto { get; set; }
        public string TipoFactor { get; set; }
        public double TasaOCuota { get; set; }
        
        [DisplayFormat]
        public double Importe { get; set; }
    }
    //clase traslado
    public class Traslado : ImpuestoBase
    {
        public Traslado()
        {

        }
        public Traslado(XmlNode node, XmlNamespaceManager nsmgr) : base(node, nsmgr)
        {
        }
    }
    //clase retencion
    public class Retencion : ImpuestoBase
    {
        public Retencion()
        {

        }
        public Retencion(XmlNode node, XmlNamespaceManager nsmgr) : base(node, nsmgr)
        {
        }
    }
    public class ImpuestosConcepto : CFDI4SubObj
    {
        protected override void construct()
        {
            Traslados = new Traslados();
            Retenciones = new Retenciones();
        }
        public ImpuestosConcepto()
        {
            construct();
        }

        public ImpuestosConcepto(XmlNode node, XmlNamespaceManager nsmgr) : base(node, nsmgr)
        {
        }

        public Traslados Traslados { get; set; }
        public Retenciones Retenciones { get; set; }

        
        }
    //clase impuestos
    public class Impuestos : CFDI4SubObj
    {
        protected override void construct()
        {
            Traslados = new Traslados();
            Retenciones = new Retenciones();
        }
        public Impuestos()
        {
            construct();
        }

        public Impuestos(XmlNode node, XmlNamespaceManager nsmgr) : base(node, nsmgr)
        {
        }
        [DisplayFormat]
        public double TotalImpuestosTrasladados { get; set; }
        [DisplayFormat]
        public double TotalImpuestosRetenidos { get; set; }
        public Traslados Traslados { get; set; }
        public Retenciones Retenciones { get; set; }
    }

    public class Complemento : CFDI4SubObj
    {
        protected override void construct()
        {
            TimbreFiscalDigital = new TimbreFiscalDigital();
            Pagos = new Pagos();
        }
        public Complemento()
        {
            construct();
        }

        public Complemento(XmlNode node, XmlNamespaceManager nsmgr) : base(node, nsmgr)
        {
        }

        public TimbreFiscalDigital TimbreFiscalDigital { get; set; }
        public Pagos Pagos { get; set; }
    }
    public class TimbreFiscalDigital : CFDI4SubObj
    {
        public TimbreFiscalDigital()
        {
        }

        public TimbreFiscalDigital(XmlNode node, XmlNamespaceManager nsmgr) : base(node, nsmgr)
        {
        }

        public string Version { get; set; }
        public string UUID { get; set; }
        public DateTime FechaTimbrado { get; set; }
        public string RfcProvCertif { get; set; }
       // public string SelloCFD { get; set; }
        public string NoCertificadoSAT { get; set; }
       // public string SelloSAT { get; set; }
    }

    public class Pago : CFDI4SubObj
    {
        public Pago()
        {
        }

        public Pago(XmlNode node, XmlNamespaceManager nsmgr) : base(node, nsmgr)
        {
        }

        public DateTime FechaPago { get; set; }
        public string FormaDePagoP { get; set; }
        public string MonedaP { get; set; }
        public double Monto { get; set; }

    }

    public class DoctoRelacionado
    {
        public string Folio { get; set; }
        public string IdDocumento { get; set; }
        public double ImpPagado { get; set; }
        public double ImpSaldoAnt { get; set; }
        public double ImpSaldoInsoluto { get; set; }
        public string MonedaDR { get; set; }
        public double MetodoDePagoDR { get; set; }
        public int NumParcialdad { get; set; }
        public string Serie { get; set; }

    }

    public class Traslados : CFDI4SubObjList<Traslado>
    {
        public Traslados()
        {
        }

        public Traslados(XmlNodeList list, XmlNamespaceManager nsmgr) : base(list, nsmgr)
        {

        }
    }

    public class Retenciones : CFDI4SubObjList<Retencion>
    {
        public Retenciones()
        {
        }

        public Retenciones(XmlNodeList list, XmlNamespaceManager nsmgr) : base(list, nsmgr)
        {

        }
    }

    public class Conceptos : CFDI4SubObjList<Concepto>
    {
        public Conceptos()
        {
        }

        public Conceptos(XmlNodeList list, XmlNamespaceManager nsmgr) : base(list, nsmgr)
        {
        }
    }
    public class Pagos: CFDI4SubObjList<Pago>
    {
        public Pagos()
        {
        }

        public Pagos(XmlNodeList list, XmlNamespaceManager nsmgr) : base(list, nsmgr)
        {
        }
    }


}
