﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Collections.Generic;

namespace Mane.CFDI.Interfaces
{
    public interface IConcepto
    {
        IComprobanteConceptoACuentaTerceros ACuentaTerceros { get; set; }
        decimal Cantidad { get; set; }
        string ClaveProdServ { get; set; }
        string ClaveUnidad { get; set; }
      //  ComprobanteConceptoComplementoConcepto ComplementoConcepto { get; set; }
        List<IComprobanteConceptoCuentaPredial> CuentaPredial { get; set; }
        string Descripcion { get; set; }
        decimal Descuento { get; set; }
        bool DescuentoSpecified { get; set; }
        decimal Importe { get; set; }
        IComprobanteConceptoImpuestos Impuestos { get; set; }
        List<IComprobanteConceptoInformacionAduanera> InformacionAduanera { get; set; }
        string NoIdentificacion { get; set; }
        string ObjetoImp { get; set; }
        List<IComprobanteConceptoParte> Parte { get; set; }
        string Unidad { get; set; }
        decimal ValorUnitario { get; set; }
    }
}