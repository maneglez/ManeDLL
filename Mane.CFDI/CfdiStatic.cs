using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using Mane.CFDI.Interfaces;

namespace Mane.CFDI
{
    public enum CfdiVersion
    {
        Desconocida,
        Cfdi3_3,
        Cfdi4_0
    }
    public partial class Cfdi
    {
        public static CfdiVersion GetVersionCfdi(string xmlPath)
        {
            try
            {
                var xml = new XmlDocument();
                xml.Load(xmlPath);
                var node = xml.FirstChild;
                if (node?.Name != "cfdi:Comprobante")
                {
                    node = node?.NextSibling;
                    if (node?.Name != "cfdi:Comprobante")
                    {
                        return CfdiVersion.Desconocida;
                    }
                }
               var version = node.Attributes["Version"].Value;
                switch (version)
                {
                    case "3.3": return CfdiVersion.Cfdi3_3;
                    case "4.0": return CfdiVersion.Cfdi4_0;
                    default: return CfdiVersion.Desconocida;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CfdiVersion.Desconocida;
        }

        public static ICfdi Load(string xmlPath)
        {
            if (!System.IO.File.Exists(xmlPath))
                throw new Exception($"No se encontró el archivo: {xmlPath}");
            switch (GetVersionCfdi(xmlPath))
            {
                case CfdiVersion.Cfdi4_0:
                    #region Convertir a CFDI4
                    var c = new Cfdi();
                    var c2 = Extensiones.CargarDesdeXml<v4.Comprobante>(xmlPath);
                    if (c2 == null) throw new Exception("No se logró deserializar el archivo xml");
                    c.CopiarValoresDePropiedades(c2);
                    if(c2.Complemento != null)
                    {
                        c.Complemento.Any = c2.Complemento.Any;
                        c.Complemento.CargarComplementos();
                    }
                    c.Emisor.CopiarValoresDePropiedades(c2.Emisor);
                    c.Receptor.CopiarValoresDePropiedades(c2.Receptor);
                    c.Version = CfdiVersion.Cfdi4_0;

                    if(c2.CfdiRelacionados != null)
                    foreach (var cdr2 in c2.CfdiRelacionados)
                    {
                        var cdr = new CfdiRelacionados();
                        cdr.TipoRelacion = cdr2.TipoRelacion;
                        if(cdr2.CfdiRelacionado != null)
                        foreach(var dr2 in cdr2.CfdiRelacionado)
                        {
                            var dr = new CfdiRelacionado();
                            dr.UUID = dr2.UUID;
                            cdr.CfdiRelacionado.Add(dr);
                        }
                        c.CfdiRelacionados.Add(cdr);
                    }
                    //Impuestos
                    c.Impuestos.CopiarValoresDePropiedades(c2.Impuestos);
                    if(c2.Impuestos?.Retenciones != null)
                    foreach (var r2 in c2.Impuestos?.Retenciones)
                    {
                        var r = new Retencion();
                        r.CopiarValoresDePropiedades(r2);
                        c.Impuestos.Retenciones.Add(r);
                    }
                    if(c2.Impuestos?.Traslados != null)
                    foreach (var t2 in c2.Impuestos.Traslados)
                    {
                        var t = new Traslado();
                        t.CopiarValoresDePropiedades(t2);
                        c.Impuestos.Traslados.Add(t);
                    }
                    //Conceptos
                    if(c2.Conceptos != null)
                    foreach(var con2 in c2.Conceptos)
                    {
                        var con = new Concepto();
                        con.CopiarValoresDePropiedades(con2);
                        con.ACuentaTerceros.CopiarValoresDePropiedades(con2.ACuentaTerceros);
                        if(con2.CuentaPredial != null)
                        foreach(var cp2 in con2.CuentaPredial)
                        {
                            con.CuentaPredial.Add(new ConceptoCuentaPredial
                            {
                                Numero = cp2.Numero
                            });
                        };
                        if(con2.Impuestos?.Retenciones != null)
                        foreach(var r2 in con2.Impuestos.Retenciones)
                        {
                            var r = new ConceptoRetencion();
                            r.CopiarValoresDePropiedades(r2);
                            con.Impuestos.Retenciones.Add(r);
                        }
                        if(con2.Impuestos?.Traslados != null)
                        foreach (var t2 in con2.Impuestos.Traslados)
                        {
                            var t = new ConceptoTraslado();
                            t.CopiarValoresDePropiedades(t2);
                            con.Impuestos.Traslados.Add(t);
                        }
                        if(con2.InformacionAduanera != null)
                        foreach(var ia2 in con2.InformacionAduanera)
                        {
                            con.InformacionAduanera.Add(new ConceptoInformacionAduanera
                            {
                                NumeroPedimento = ia2.NumeroPedimento
                            });
                        }
                        if(con2.Parte != null)
                        foreach(var cp2 in con2.Parte)
                        {
                            var cp = new ConceptoParte();
                            cp.CopiarValoresDePropiedades(cp2);
                            if(cp2.InformacionAduanera != null)
                            foreach(var ia2 in cp2.InformacionAduanera)
                            {
                                cp.InformacionAduanera.Add(new ConceptoParteInformacionAduanera
                                {
                                    NumeroPedimento = ia2.NumeroPedimento
                                });
                            }
                            con.Parte.Add(cp);
                        }
                        c.Conceptos.Add(con);
                    }
                    return c;
                #endregion
                default: throw new Exception("Versión de CFDI no soportada");
            }
        }
    }
}
