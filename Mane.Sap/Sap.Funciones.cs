using Mane.Sap.Helpers;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Mane.Sap
{
    public static partial class Sap
    {
        /// <summary>
        /// Muestra formulario para testear conexiones a SAP
        /// </summary>
        public static void ShowTester()
        {
            Forms.ConectionTester.ShowTester();
        }
        /// <summary>
        /// Crea objetos del tipo attachments
        /// </summary>
        /// <param name="fileNames">Archivos a adjuntar</param>
        /// <returns>Crea un adjunto</returns>
        public static bool CrearAdjuntos(string[] fileNames)
        {

            Attachments2 attach = comp.GetBusinessObject(BoObjectTypes.oAttachments2); //objeto de adjuntos
            var ln = attach.Lines;
            for (int i = 0; i < fileNames.Length; i++)
            {
                ln.SetCurrentLine(i);
                ln.FileName = Path.GetFileNameWithoutExtension(fileNames[i]);
                ln.FileExtension = Path.GetExtension(fileNames[i]).Substring(1);
                ln.SourcePath = Path.GetDirectoryName(fileNames[i]);
                ln.Override = BoYesNoEnum.tYES;
                ln.Add();
            }
            if (attach.Add() != 0) throw new Exception(comp.GetLastErrorDescription());
            return true;

        }

        //private static string SetPropsToObject(BoObjectTypes tipoObj, Dictionary<string, object> SetNewHeadrValuesToTargetDoc)
        //{
        //    var xml = comp.GetBusinessObjectXmlSchema(tipoObj);
        //    var xdoc = new XmlDocument();
        //    xdoc.LoadXml(xml);
        //    var nodoEncabezado = xdoc.SelectSingleNode("//BO").FirstChild.NextSibling;

        //        //Establecer valores nuevos en el encabezado
        //        foreach (string key in SetNewHeadrValuesToTargetDoc.Keys)
        //        {
        //            try
        //            {
        //                nodoEncabezado.SelectSingleNode("//" + key).InnerText = SetNewHeadrValuesToTargetDoc[key].ToString();
        //            }
        //            catch (Exception)
        //            {

        //            }
        //        }

        //    //Guardar el XML Editado en una ruta temporal
        //    var rutaXmlTmp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
        //    xdoc.Save(rutaXmlTmp);
        //    return rutaXmlTmp;
        //}
        /// <summary>
        /// Copia un documento a otro
        /// </summary>
        /// <param name="BaseEntry">DocEntry del documento del que se desea copiar</param>
        /// <param name="BaseType">Tipo de documento del que se desea copiar</param>
        /// <param name="TargetType">Tipo de documento destino</param>
        /// <param name="thisLinesOnly">(Opcional) indicar cuales lineas copiar</param>
        /// <returns>Documento copiado sin agregar a SAP</returns>
        public static Documents CopyDocument(int BaseEntry, TipoObjetoSap BaseType, TipoObjetoSap TargetType, int[] thisLinesOnly = null)
        {
            Documents docTarget = comp.GetBusinessObject((BoObjectTypes)TargetType),
             docBase = comp.GetBusinessObject((BoObjectTypes)BaseType);
            if (!docBase.GetByKey(BaseEntry)) throw new Exception($"No se encontró el documento con la entrada {BaseEntry}");
            var lnBase = docBase.Lines;
            var lnTarget = docTarget.Lines;
            lnTarget.Delete();
            if (thisLinesOnly == null)
            {
                for (int i = 0; i < lnBase.Count; i++)
                {
                    lnTarget.Add();
                    lnBase.SetCurrentLine(i);
                    lnTarget.SetCurrentLine(i);
                    if (docBase.DocType == BoDocumentTypes.dDocument_Items)
                        lnTarget.BaseType = (int)docBase.DocObjectCode;
                    lnTarget.BaseLine = lnBase.LineNum;
                    lnTarget.BaseEntry = lnBase.DocEntry;
                }
            }
            else
            {
                for (int i = 0; i < thisLinesOnly.Length; i++)
                {
                    lnTarget.Add();
                    lnBase.SetCurrentLine(thisLinesOnly[i]);
                    lnTarget.SetCurrentLine(i);
                    lnTarget.BaseType = (int)docBase.DocObjectCode;
                    lnTarget.BaseLine = lnBase.LineNum;
                    lnTarget.BaseEntry = lnBase.DocEntry;
                }
            }
            return docTarget;


        }
        /// <summary>
        /// Copia un documento a otro
        /// </summary>
        /// <param name="BaseEntry">DocEntry del documento del que se desea copiar</param>
        /// <param name="BaseType">Tipo de documento del que se desea copiar</param>
        /// <param name="TargetType">Tipo de documento destino</param>
        /// <param name="thisLinesOnly">(Opcional) indicar cuales lineas copiar</param>
        /// <returns>Verdadero si jaló falso si no jaló</returns>
        public static bool CopyDocuments(int BaseEntry, TipoObjetoSap BaseType, TipoObjetoSap TargetType, int series = -1, int[] thisLinesOnly = null)
        {
            try
            {
                var doc = CopyDocument(BaseEntry, BaseType, TargetType, thisLinesOnly);
                if (series != -1)
                    doc.Series = series;
                if (doc.Add() != 0)
                    throw new Exception(comp.GetLastErrorDescription());
                return true;
            }
            catch (Exception e)
            {
                LastError = e.ToString();
                return false;
            }
        }


        public static bool CopiarDeBorrador(int DocEntry, TipoObjetoSap targetDocType, Dictionary<string, object> SetPropsToTargetDoc = null)
        {

            Documents borrador = comp.GetBusinessObject(BoObjectTypes.oDrafts);
            if (!borrador.GetByKey(DocEntry))
                throw new Exception("No se encontró el Borrador con la entrada: " + DocEntry);
            if (SetPropsToTargetDoc != null)
            {
                var fileName = DocumentToXml(borrador, false, SetPropsToTargetDoc);
                if (borrador.UpdateFromXML(fileName) != 0)
                    throw new Exception("Error al actualizar borrador: " + comp.GetLastErrorDescription());
                borrador.GetByKey(DocEntry);
                File.Delete(fileName);
            }
            borrador.DocObjectCode = (BoObjectTypes)targetDocType;
            if (borrador.SaveDraftToDocument() != 0)
                throw new Exception("No se pudo generar el documento a partir del borrador: " + comp.GetLastErrorDescription());
            return true;


        }
        public static bool DuplicateDocument(int DocEntry, TipoObjetoSap DocType, Dictionary<string, object> SetPropsToTargetDoc = null)
        {

            Documents doc = Company.GetBusinessObject((BoObjectTypes)DocType);
            if (!doc.GetByKey(DocEntry))
                throw new Exception($"No se encontró del documento con la entrada {DocEntry}");
            var rutaXmlTmp = DocumentToXml(doc, true, SetPropsToTargetDoc);
            //Recuperar el documento
            var result = true;
            Documents duplicatedDoc = comp.GetBusinessObjectFromXML(rutaXmlTmp, 0);
            if (duplicatedDoc.Add() != 0)
            {
                LastError = comp.GetLastErrorDescription();
                result = false;
            }
            //Eliminar el archivo temporal creado
            File.Delete(rutaXmlTmp);
            return result;

        }
        /// <summary>
        /// Convierte el documento a xml y retorna la ruta donde está guardado
        /// </summary>
        /// <param name="doc">Documento a generar xml</param>
        /// <param name="CleanDoc">Indica si hay que crear el xml como documento nuevo pero conservando las lineas y demas config (borra docentry y relaciones)</param>
        /// <param name="NewDocHeaderValues">Propiedades de encabezado a actualizar</param>
        /// <returns>ruta del xml Recien creado</returns>
        public static string DocumentToXml(Documents doc, bool CleanDoc, Dictionary<string, object> NewDocHeaderValues = null)
        {
            var xdoc = new XmlDocument();
            xdoc.LoadXml(doc.GetAsXML());
            var nodoEncabezado = xdoc.SelectSingleNode("//BO").FirstChild.NextSibling;
            var nodoLineas = nodoEncabezado.NextSibling;
            if (CleanDoc)
            {
                //quitar valores para que quede como nuevo doc
                nodoEncabezado.SelectSingleNode("//DocEntry").InnerText = "";
                nodoEncabezado.SelectSingleNode("//DocNum").InnerText = "";
                nodoEncabezado.SelectSingleNode("//JrnlMemo").InnerText = "";
                //Quitar la relación de las lineas
                if (nodoLineas != null)
                {
                    var dic = new Dictionary<string, string>();
                    dic.Add("TargetType", "-1");
                    dic.Add("DocEntry", "");
                    dic.Add("BaseType", "-1");
                    dic.Add("TargetEntry", "0");
                    dic.Add("BaseEntry", "0");
                    dic.Add("BaseLine", "0");
                    dic.Add("BaseRef", "");
                    dic.Add("LineStatus", "O");
                    foreach (var key in dic.Keys)
                        foreach (XmlNode n in nodoLineas.SelectNodes($"//{key}"))
                            n.InnerText = dic[key];
                }

            }

            if (NewDocHeaderValues != null)
            {
                //Establecer valores nuevos en el encabezado
                foreach (string key in NewDocHeaderValues.Keys)
                {
                    try
                    {
                        nodoEncabezado.SelectSingleNode("//" + key).InnerText = NewDocHeaderValues[key].ToString();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            //Guardar el XML Editado en una ruta temporal
            var rutaXmlTmp = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            xdoc.Save(rutaXmlTmp);
            return rutaXmlTmp;
        }
        public static bool CrearDocumento(IDocuments doc, IEnumerable<IDocument_Lines> lineas, TipoObjetoSap tipoDoc)
        {

            var c = Company;
            Documents d = c.GetBusinessObject((BoObjectTypes)tipoDoc);
            d.CardCode = doc.CardCode;
            d.DocDate = doc.DocDate;
            d.Series = doc.Series;
            d.DocDate = doc.DocDueDate;
            d.Comments = doc.Comments;
            int totalLn = lineas.Count();
            var l = d.Lines;
            if (d.DocType == BoDocumentTypes.dDocument_Service)
            {
                for (int i = 0; i < totalLn; i++)
                {
                    var ln = lineas.ElementAt(i);
                    l.SetCurrentLine(i);
                    l.ItemDescription = ln.ItemDescription;
                    l.Price = ln.Price;
                    l.TaxCode = ln.TaxCode;
                    l.Add();
                }
            }
            else
            {
                for (int i = 0; i < totalLn; i++)
                {
                    var ln = lineas.ElementAt(i);
                    l.SetCurrentLine(i);
                    l.ItemCode = ln.ItemCode;
                    l.Quantity = ln.Quantity;
                    l.Price = ln.Price;
                    l.TaxCode = ln.TaxCode;
                    l.WarehouseCode = ln.WarehouseCode;
                    l.Add();
                }
            }
            if (d.Add() != 0)
                throw new Exception("Error al crear el documento: " + c.GetLastErrorDescription());
            return true;

        }

        private static string _rutaConexiones;
        public static string RutaConexiones
        {
            get
            {
                if (string.IsNullOrEmpty(_rutaConexiones))
                    return Path.Combine(".", "ManeSapConections.xml");
                return _rutaConexiones;
            }
            set => _rutaConexiones = value;
        }

        public static IEnumerable<ConexionSap> InstanciarConexiones(string rutaConexiones = "")
        {
            if (string.IsNullOrEmpty(rutaConexiones))
                rutaConexiones = RutaConexiones;
            if (!File.Exists(rutaConexiones))
                return new List<ConexionSap>();
            var aux = Serializacion.XMLToObject<List<ConexionSap>>(rutaConexiones);
            if (aux == null)
                return new List<ConexionSap>();
            if (!string.IsNullOrWhiteSpace(EncryptPassword))
            {
                foreach (var item in aux)
                {
                    item.Password = Crypto.Decriptar(item.Password, EncryptPassword);
                    item.DbPassword = Crypto.Decriptar(item.DbPassword, EncryptPassword);
                }
            }
            return aux;
        }
        public static void GuardarConexiones(IEnumerable<ConexionSap> conexiones,string rutaConexiones = "")
        {
            if (string.IsNullOrEmpty(rutaConexiones))
                rutaConexiones = RutaConexiones;
            if (conexiones == null)
                return;
            if (!string.IsNullOrEmpty(EncryptPassword))
            {
                foreach (var item in conexiones)
                {
                    item.Password = Crypto.Encriptar(item.Password, EncryptPassword);
                    item.DbPassword = Crypto.Encriptar(item.DbPassword, EncryptPassword);
                }
            }
            Serializacion.ObjectToXML(conexiones, rutaConexiones, true);
        }

       
    }


        
}
