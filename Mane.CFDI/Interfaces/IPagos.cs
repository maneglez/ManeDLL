﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------
using Mane.CFDI.Complementos.Pago;
using System.Collections.Generic;

namespace Mane.CFDI.Interfaces
{
    public interface IPagos
    {
        List<IPago> Pago { get; set; }
        IPagosTotales Totales { get; set; }
        string Version { get; set; }
    }
}