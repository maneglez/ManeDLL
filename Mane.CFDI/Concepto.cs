using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml;
using Mane.CFDI.Interfaces;


namespace Mane.CFDI
{
    public class Concepto : IConcepto
    {
        public IComprobanteConceptoACuentaTerceros ACuentaTerceros { get; set; }
        public decimal Cantidad { get; set; }
        public string ClaveProdServ { get; set; }
        public string ClaveUnidad { get; set; }
        public List<IComprobanteConceptoCuentaPredial> CuentaPredial { get; set; }
        public string Descripcion { get; set; }
        public decimal Descuento { get; set; }
        public bool DescuentoSpecified { get; set; }
        public decimal Importe { get; set; }
        public IComprobanteConceptoImpuestos Impuestos { get; set; }
        public List<IComprobanteConceptoInformacionAduanera> InformacionAduanera { get; set; }
        public string NoIdentificacion { get; set; }
        public string ObjetoImp { get; set; }
        public List<IComprobanteConceptoParte> Parte { get; set; }
        public string Unidad { get; set; }
        public decimal ValorUnitario { get; set; }
        public Concepto()
        {
            ACuentaTerceros = new CuentaATerceros();
            CuentaPredial = new List<IComprobanteConceptoCuentaPredial>();
            Impuestos = new ImpuestosConcepto();
            InformacionAduanera = new List<IComprobanteConceptoInformacionAduanera>();
            Parte = new List<IComprobanteConceptoParte>();
            

        }
    }
}

