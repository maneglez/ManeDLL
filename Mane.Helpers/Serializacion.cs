using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;

namespace Mane.Helpers
{
    public static class Serializacion
    {
        /// <summary>
        /// Convierte objeto a Clave valor
        /// </summary>
        /// <param name="obj">ovjeto</param>
        /// <returns></returns>
        public static Dictionary<string, object> ObjectToKeyValue(object obj)
        {
            if (obj == null) throw new Exception("El objeto es nulo");
            var dic = new Dictionary<string, object>();
            var props = obj.GetType().GetProperties();
            foreach (var prop in props)
            {
                dic.Add(prop.Name, prop.GetValue(obj));
            }
            return dic;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static T KeyValueToObject<T>(Dictionary<string, object> dic) where T : new()
        {
            T obj = new T();
            var props = obj.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (prop.CanWrite)
                    prop.SetValue(obj, ConvertirATipo(prop.PropertyType, dic[prop.Name]));
            }
            return obj;
        }
        /// <summary>
        /// Obtiene el valor de una propiedad En base al nombre
        /// </summary>
        /// <param name="src">Objeto del cual se requiere una propiedad</param>
        /// <param name="propName">Nombre de la propiedad</param>
        /// <returns></returns>
        public static object GetPropValueByName(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static dynamic ConvertirATipo(Type t, object value)
        {
            if (value == null) return value;
            try
            {
                switch (t.Name)
                {
                    case "String": return value == DBNull.Value ? "" : value.ToString();
                    case "Boolean": return value == DBNull.Value ? false : (value.ToString() == "1" ? true : false);
                    case "DateTime": return value == DBNull.Value ? DateTime.MinValue : DateTime.Parse(value.ToString());
                    case "Double": return value == DBNull.Value ? 0 : double.Parse(value.ToString());
                    case "Int32": return value == DBNull.Value ? 0 : int.Parse(value.ToString());
                    case "Decimal": return value == DBNull.Value ? 0 : decimal.Parse(value.ToString());
                }
                if (t.BaseType == typeof(Enum)) return int.Parse(value.ToString());

            }
            catch (Exception ex)
            {
              //  Log.Add("Conversor de tipo \r\n" + ex.StackTrace);
            }
            return value;
        }

        /// <summary>
        /// Convierte un Xml a un T objeto
        /// </summary>
        /// <typeparam name="T">Tipo de objeto</typeparam>
        /// <param name="xmlPath">ruta del XML</param>
        /// <returns>T objeto</returns>
        public static T XMLToObject<T>(string xmlPath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (StreamReader sr = new StreamReader(xmlPath))
                return (T)serializer.Deserialize(sr);
        }
        /// <summary>
        /// Convierte un objeto a XML
        /// </summary>
        /// <param name="objeto">Objeto a Serializar</param>
        /// <param name="xmlPath">Ruta de guardado</param>
        /// <param name="reemplazar"></param>
        public static void ObjectToXML(object objeto, string xmlPath, bool reemplazar = false)
        {
            WriteStringToFile(ObjectToXML(objeto), xmlPath, reemplazar);
        }
        /// <summary>
        /// Convierte un objeto a XML
        /// </summary>
        /// <param name="objeto">Objeto a Serializar</param>
        /// <returns>Cadena con el XML resultante</returns>
        public static string ObjectToXML(object objeto)
        {

            string xml = "";
            XmlSerializer serializer = new XmlSerializer(objeto.GetType());
            using (StringWriter sw = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sw))
                {
                    serializer.Serialize(writer, objeto);
                    xml = sw.ToString();
                }
            }
            return xml;
        }

        /// <summary>
        /// Lee todo el texto de un archivo
        /// </summary>
        /// <param name="ruta">Ruta del archivo</param>
        /// <returns>string con los datos del archivo</returns>
        public static string ReadAllFromFile(string ruta)
        {
            string data = "";
            using (var sr = new StreamReader(ruta))
                data = sr.ReadToEnd();
            return data;
        }
        /// <summary>
        /// Convierte un enum a un datatable("value","name")
        /// </summary>
        /// <param name="enumType">Tipo de Enum</param>
        /// <returns>DataTable con 2 Columnas: "value","name"</returns>
        [Obsolete("Utilizar la función genérica")]
        public static DataTable EnumToDataTable(Type enumType)
        {
            var values = Enum.GetValues(enumType);
            var names = Enum.GetNames(enumType);
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("value"));
            dt.Columns.Add(new DataColumn("name"));
            DataRow r;
            int i = 0;
            foreach (object value in values)
            {
                r = dt.NewRow();
                r[0] = (int)value;
                r[1] = names[i];
                dt.Rows.Add(r);
                i++;
            }
            return dt;
        }
        /// <summary>
        /// Convierte un enum a un datatable("value","name")
        /// </summary>
        /// <typeparam name="Tenum">Tenumipo de Enum</typeparam>
        /// <returns>DataTenumable con 2 Columnas: "value","name"</returns>
        public static DataTable EnumToDataTable<Tenum>() where Tenum : Enum
        {
            var tipo = typeof(Tenum);
            var values = Enum.GetValues(tipo);
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("value"));
            dt.Columns.Add(new DataColumn("name"));
            DataRow r;
            foreach (var value in values)
            {
                r = dt.NewRow();
                r[0] = (int)value;
                r[1] = GetDescriptionAttr((Tenum)value);
                dt.Rows.Add(r);
            }
            return dt;
        }

        /// <summary>
        /// Convierte un enum a un diccionario
        /// </summary>
        /// <typeparam name="Tenum">Tipo de Enum</typeparam>
        /// <returns>Diccionario</returns>
        public static Dictionary<string, Tenum> EnumToDictionary<Tenum>() where Tenum : Enum
        {
            var tipo = typeof(Tenum);
            var values = Enum.GetValues(tipo);
            var dic = new Dictionary<string, Tenum>();
            foreach (Tenum value in values)
                dic.Add(GetDescriptionAttr(value), value);
            return dic;
        }
        /// <summary>
        /// Asigna todos los valores de un enum a un combobox
        /// </summary>
        /// <typeparam name="Tenum">Tipo de Enum</typeparam>
        /// <param name="cb">Combobox</param>
        public static void EnumToComboBox<Tenum>(ComboBox cb) where Tenum : Enum
        {
            cb.DataSource = new BindingSource(EnumToDictionary<Tenum>(), null);
            cb.DisplayMember = "Key";
            cb.ValueMember = "Value";
        }
        /// <summary>
        /// Obtiene la descripción del decorador Descripcion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string GetDescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
        /// <summary>
        /// Convierte una cadena del portapapeles proveniente de excel a un data table
        /// </summary>
        /// <param name="data">Cadena proveniente de excel</param>
        /// <returns>DataTable con datos formateados</returns>
        public static DataTable ExcelClipBoardToDataTable(string data)
        {

            var dt = new DataTable();
            try
            {
                data = data.Replace("\r", "");
                var rows = data.Split('\n');
                if (rows.Length == 0) return dt;
                var columns = rows[0].Split('\t');
                if (columns.Length == 0) return dt;
                foreach (var item in columns)
                {
                    dt.Columns.Add(new DataColumn());
                }
                DataRow r;
                string[] values;
                for (int i = 0; i < rows.Length; i++)
                {
                    r = dt.NewRow();
                    values = rows[i].Split('\t');
                    for (int j = 0; j < values.Length; j++)
                    {
                        r[j] = values[j].Trim(' ');
                    }
                    dt.Rows.Add(r);
                }
            }
            catch (Exception) { }
            return dt;

        }
        /// <summary>
        /// Cantidad de milisegundos que tardó en ejecutar el action
        /// </summary>
        /// <param name="action">Funcion o action</param>
        /// <returns>Milisegundos en total</returns>
        public static double CuantoTiempoTarda(Action action)
        {
            DateTime ahora = DateTime.Now;
            action.Invoke();
            return (DateTime.Now - ahora).TotalMilliseconds;
        }
        /// <summary>
        /// Copia todo el contenido de un datagrid al clipboard
        /// </summary>
        /// <param name="dgv">DataGrid a copiar</param>
        public static void CopiarTabla(DataGridView dgv)
        {
            dgv.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            int orgSelMode = (int)dgv.SelectionMode;
            dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
            bool orgMultiSelect = dgv.MultiSelect;
            dgv.MultiSelect = true;
            dgv.SelectAll();
            var obj = dgv.GetClipboardContent();
            if (obj != null)
                Clipboard.SetDataObject(obj);
            dgv.SelectionMode = (DataGridViewSelectionMode)orgSelMode;
            dgv.MultiSelect = orgMultiSelect;
        }

        /// <summary>
        /// Convierte una notacion de camello en una frase con espacios
        /// </summary>
        /// <param name="camelNotation">cadena en notacion de camello "EsteEsUnEjemplo"</param>
        /// <returns>"EsteEsUnEjemplo" => "Este Es Un Ejemplo"</returns>
        public static string SpaceCamelNotation(string camelNotation)
        {
            return System.Text.RegularExpressions.Regex.Replace(
        System.Text.RegularExpressions.Regex.Replace(
            camelNotation,
            @"(\P{Ll})(\P{Ll}\p{Ll})",
            "$1 $2"
        ),
        @"(\p{Ll})(\P{Ll})",
        "$1 $2"
    );
        }
    }
}
