using Mane.CFDI.Complementos.Pago;
using Mane.CFDI.Interfaces;
using Mane.CFDI.v4;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Mane.CFDI
{
    public static class Extensiones
    {

        #region XMLElement
        public static T ToObject<T>(this XmlElement element) where T : class
        {
            T result = null;
            try
            {
                var ser = new XmlSerializer(typeof(T));
                using (var s = GenerateStreamFromString(element.OuterXml))
                {
                   result = (T)ser.Deserialize(s);
                }
            }
            catch (Exception)
            {

            }
            return result;
        }
        private static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        #endregion

        #region IComplemento
        public static Tuple<XmlElement,string> VersionComplemento(this IComplemento c,ComplementosCompatibles tipoComplemento)
        {
            var nombreComplemento = tipoComplemento.ToString();
            var versionComplemento = "";
            XmlElement elementoComplemento = null;
            foreach (XmlElement element in c.Any)
            {
                if (element.Name.EndsWith(nombreComplemento))
                {
                    if (element.HasAttribute("Version"))
                        versionComplemento = element.GetAttribute("Version");
                    else if (element.HasAttribute("version"))
                        versionComplemento = element.GetAttribute("version");
                    elementoComplemento = element;
                    break;
                }
            }
            return new Tuple<XmlElement, string>(elementoComplemento,versionComplemento);
        }
        public static void CargarComplementos(this IComplemento c)
        {
            var tipoEnum = typeof(ComplementosCompatibles);
            var nombres = Enum.GetNames(tipoEnum);
            var valores = Enum.GetValues(tipoEnum);
            var i = 0;
            foreach (ComplementosCompatibles tipoComplemento in valores)
            {
                var versionComplemento = c.VersionComplemento(tipoComplemento);

                    switch (tipoComplemento)
                    {
                        case ComplementosCompatibles.Pagos:
                        c.Pagos.PagoDesdeXml(versionComplemento.Item1,versionComplemento.Item2);
                        break;
                        case ComplementosCompatibles.TimbreFiscalDigital:
                        c.TimbreFiscalDigital.TfdDesdeXml(versionComplemento.Item1, versionComplemento.Item2);
                            break;
                        default:
                            break;
                    }
                }
                i++;
            }
        #endregion

        #region Ipagos
        public static void PagoDesdeXml(this IPagos p,XmlElement el,string version)
        {
            
            switch (version)
            {
                case "2.0":                   
                    var p2 = el.ToObject<Complementos.Pago.v2.Pagos>();
                    if (p2 == null) break;
                    #region Copiar todo de p2 a p1
                    p.CopiarValoresDePropiedades(p2);
                    p.Totales.CopiarValoresDePropiedades(p2.Totales);
                    if(p2.Pago != null)
                    foreach (var pp2 in p2.Pago)
                    {
                        var pp = new Pago();
                        pp.CopiarValoresDePropiedades(pp2);
                            if(pp2.DoctoRelacionado != null)
                        foreach (var pdr2 in pp2.DoctoRelacionado)
                        {
                            var pdr = new PagoDoctoRelacionado();
                            pdr.CopiarValoresDePropiedades(pdr2);
                            if(pdr2.ImpuestosDR?.RetencionesDR != null)
                            foreach (var pdr2ir in pdr2.ImpuestosDR.RetencionesDR)
                            {

                                var pdrir = new PagoDoctoRelacionadoImpuestosDRRetencionDR();
                                pdrir.CopiarValoresDePropiedades(pdr2ir);
                                pdr.ImpuestosDR.RetencionesDR.Add(pdrir);
                            }
                            if(pdr2.ImpuestosDR?.TrasladosDR != null)
                            foreach (var pdrit2 in pdr2.ImpuestosDR.TrasladosDR)
                            {
                                var pdrit = new PagoDoctoRelacionadoImpuestosDRTrasladoDR();
                                pdrit.CopiarValoresDePropiedades(pdrit2);
                                pdr.ImpuestosDR.TrasladosDR.Add(pdrit);
                            }
                            pp.DoctoRelacionado.Add(pdr);
                        }
                            if(pp2.ImpuestosP?.RetencionesP != null)
                        foreach (var pir2 in pp2.ImpuestosP.RetencionesP)
                        {
                            var pir = new PagoImpuestosPRetencionP();
                            pir.CopiarValoresDePropiedades(pir2);
                            pp.ImpuestosP.RetencionesP.Add(pir);
                        }
                       if(pp2.ImpuestosP?.TrasladosP != null)
                        foreach (var pit2 in pp2.ImpuestosP.TrasladosP)
                        {
                            var pit = new PagoImpuestosPTrasladoP();
                            pit.CopiarValoresDePropiedades(pit2);
                            pp.ImpuestosP.TrasladosP.Add(pit);
                        }
                        p.Pago.Add(pp);
                    }
                    #endregion
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region ITimbreFiscalDigital
        public static void TfdDesdeXml(this ITimbreFiscalDigital t,XmlElement el, string version)
        {
            switch (version)
            {
                case "1.1":
                    var t2 = el.ToObject<Complementos.TimbreFiscalDigital.v1_1.TimbreFiscalDigital>();
                    if (t2 == null) break;
                    t.CopiarValoresDePropiedades(t2);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Copia los valores de Tsource a Ttarget Solo si las propiedades tienen el mismo nombre y tipo
        /// </summary>
        /// <typeparam name="Ttarget">Tipo destino</typeparam>
        /// <typeparam name="Tsource">Tipo origen</typeparam>
        /// <param name="t">Destino</param>
        /// <param name="s">Origen</param>
        public static void CopiarValoresDePropiedades<Ttarget, Tsource>(this Ttarget t, Tsource s) 
            where Ttarget : class 
            where Tsource : class
        {
            if (t == null || s == null) return;
            var tt = t.GetType();
            var ts = s.GetType();
            var tProps = tt.GetProperties();
            foreach (PropertyInfo tp in tProps)
            {
                if (!tp.CanWrite) continue;
                var sp = ts.GetProperty(tp.Name);
                if (sp == null) continue;
                if (!sp.CanRead) continue;
                if (tp.PropertyType.FullName != sp.PropertyType.FullName) continue;
                tp.SetValue(t, sp.GetValue(s));
            }
        }

        public static T CargarDesdeXml<T>(string fileName)where T : class
        {
            T result = null;
            try
            {
                var ser = new XmlSerializer(typeof(T));
                using (StreamReader sr = new StreamReader(fileName))
                    result = (T)ser.Deserialize(sr);
            }
            catch (Exception)
            {
                return null;
            }
            
            return result;
        }
        #endregion
    }




}

