﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using System;

namespace Mane.CFDI.Interfaces
{
    public interface ITimbreFiscalDigital
    {
        DateTime FechaTimbrado { get; set; }
        string Leyenda { get; set; }
        string NoCertificadoSAT { get; set; }
        string RfcProvCertif { get; set; }
        string SelloCFD { get; set; }
        string SelloSAT { get; set; }
        string UUID { get; set; }
        string Version { get; set; }
    }
}