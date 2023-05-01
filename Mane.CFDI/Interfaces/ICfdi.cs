using System;
using System.Collections.Generic;

namespace Mane.CFDI.Interfaces
{
    public interface ICfdi
    {
        Addenda Addenda { get; set; }
        string Certificado { get; set; }
        List<ICfdiRelacionados> CfdiRelacionados { get; set; }
        IComplemento Complemento { get; set; }
        List<IConcepto> Conceptos { get; set; }
        string CondicionesDePago { get; set; }
        string Confirmacion { get; set; }
        decimal Descuento { get; set; }
        bool DescuentoSpecified { get; set; }
        IEmisor Emisor { get; set; }
        string Exportacion { get; set; }
        DateTime Fecha { get; set; }
        string Folio { get; set; }
        string FormaPago { get; set; }
        IComprobanteImpuestos Impuestos { get; set; }
        IComprobanteInformacionGlobal InformacionGlobal { get; set; }
        string LugarExpedicion { get; set; }
        string MetodoPago { get; set; }
        string Moneda { get; set; }
        string NoCertificado { get; set; }
        IReceptor Receptor { get; set; }
        string Sello { get; set; }
        string Serie { get; set; }
        decimal SubTotal { get; set; }
        decimal TipoCambio { get; set; }
        bool TipoCambioSpecified { get; set; }
        string TipoDeComprobante { get; set; }
        decimal Total { get; set; }
        CfdiVersion Version { get; set; }
    }
}