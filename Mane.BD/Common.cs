using System;
using System.Collections.Generic;


namespace Mane.BD
{
    public static class Common
    {

        /// <summary>
        /// Obtiene arreglo con las columnas del modelo
        /// </summary>
        /// <typeparam name="Tmodelo">Tipo de modelo</typeparam>
        /// <returns>Arreglo con las columnas del Tmodelo</returns>


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
                if (!prop.IsDefined(typeof(ManeBdIgnorarPropAttribute), false))
                    dic.Add(prop.Name, prop.GetValue(obj));
            }
            return dic;
        }
        /// <summary>
        /// Convierte objeto a Clave valor
        /// </summary>
        /// <param name="obj">ovjeto</param>
        /// <returns></returns>
        //public static Dictionary<string, object> ObjectToKeyValue(Modelo obj)
        //{
        //    if (obj == null) throw new Exception("El objeto es nulo");
        //    var dic = new Dictionary<string, object>();
        //    var props = obj.GetType().GetProperties();
        //    var idName = obj.getIdName();
        //    foreach (var prop in props)
        //    {
                
        //        if (!prop.IsDefined(typeof(ManeBdIgnorarPropAttribute), false) && prop.Name != idName)
        //            dic.Add(prop.Name, prop.GetValue(obj));
        //    }
        //    return dic;
        //}
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
              
                if (prop.CanWrite && !prop.IsDefined(typeof(ManeBdIgnorarPropAttribute), false))
                {
                    if(dic.TryGetValue(prop.Name,out object value))
                     prop.SetValue(obj, ConvertirATipo(prop.PropertyType,value));
                }
                    
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
            if (value.GetType().FullName == "Microsoft.Extensions.Primitives.StringValues")
                value = value.ToString();
            try
            {
                switch (t.Name)
                {
                    case "String": return value == DBNull.Value ? "" : value.ToString();
                    case "Boolean":
                        if (value is bool @bool) return @bool;
                        if (value is string @string)
                            return @string == "1";
                        return value == DBNull.Value ? false : Convert.ToBoolean(value);
                    case "DateTime": return value == DBNull.Value ? DateTime.MinValue : DateTime.Parse(value.ToString());
                    case "Double": return value == DBNull.Value ? 0 : double.Parse(value.ToString());
                    case "Int32": return value == DBNull.Value ? 0 : Convert.ToInt32(value);
                    case "Int16": return value == DBNull.Value ? 0 : Convert.ToInt16(value);//aqui bota error cuando se le asigna a una propiedad del tipo Short
                    case "Decimal": return value == DBNull.Value ? 0 : decimal.Parse(value.ToString());
                    case "Byte[]": return value == DBNull.Value ? new byte[] { } : StringToByteArray(value.ToString());
                }
                if (t.BaseType == typeof(Enum)) return Convert.ToInt16(value == DBNull.Value ? null : value);

            }
            catch (Exception ex)
            {
                var msg = ex.Message;
            }
            return value;
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

        public static byte[] StringToByteArray(string hex)
        {
            int cnt = hex.Length;
            if (hex.StartsWith("0x"))
                hex = hex.Substring(2, hex.Length - 2);

            byte[] bytes = new byte[cnt / 2];

            for (int i = 0; i < cnt; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            return bytes;
        }
    }
}
