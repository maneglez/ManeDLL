﻿using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Mane.BD.QueryBulder.Builders
{
    internal static class Common
    {
        public static string FormatValue(object value, char[] delimiters = null)
        {
            if (delimiters == null) delimiters = new char[] { '\'', '\'' };
            if (value == null) return Delimit("", delimiters);
            if (value is string)
            {
                var val = (string)value;
                if (val.Contains(delimiters[0])) val = val.Replace(delimiters[0].ToString(), new StringBuilder().Append(delimiters[0]).Append(delimiters[0]).ToString());
                if (delimiters[0] != delimiters[1])
                    if (val.Contains(delimiters[1])) val = val.Replace(delimiters[1].ToString(), new StringBuilder().Append(delimiters[1]).Append(delimiters[1]).ToString());
                return Delimit(val, delimiters);
            }
            if (value is bool)
                return (bool)value ? Delimit("1", delimiters) : Delimit("0", delimiters);
            if (value is DateTime)
                return Delimit(Bd.ToDateTimeSqlFormat((DateTime)value), delimiters);
            if (value.GetType().IsEnum)
                return Delimit(((int)value).ToString(), delimiters);
            if(value is byte[] bytes)//Convertir arreglo de bytes en string sql binary
                return "0x" + string.Join("", bytes.Select(b => b.ToString("x2")));
            string strVal = value.ToString();
            if (double.TryParse(strVal, out double dval))
                return strVal;
            return Delimit(value.ToString(), delimiters);
        }
        public static string FormatColumn(string columna, char[] delimiters)
        {
            if (string.IsNullOrEmpty(columna)) return "";
            string colFormateada = "";
            if (columna.StartsWith(delimiters[0].ToString()))
                return columna;

            if (columna.ToLower().Contains(" as ")) // Columna as otraCosa
            {
                string funcion = "";
                if (columna.Contains(")"))//Funcion()
                {
                    var splitFunc = columna.Split(')');
                    if (splitFunc.Length == 1)//funcion( as )
                        return columna;
                    funcion = splitFunc[0] + ")";

                    if (funcion.ToLower().Contains(" as "))//funcion( as ) as columna
                    {
                        var colTmp = splitFunc[1];
                        colTmp = colTmp.Trim();
                        colTmp = colTmp.Replace("As ", "as ").Replace("AS ", "as ").Replace("aS ", "as ").Replace("as ", "");
                        return $"{funcion} AS {FormatColumn(colTmp, delimiters)}";

                    }

                }

                columna = columna.Replace(" As ", " as ").Replace(" AS ", " as ").Replace(" aS ", " as ");
                string[] aux2 = columna.Replace(" as ", ";").Split(';');
                if (aux2.Length == 2)
                {
                    if (aux2[0].Contains("'"))// 'Algo' as Columna
                        return $"{aux2[0]} AS {FormatColumn(aux2[1], delimiters)}";
                    if (aux2[0].Contains("-") || aux2[0].Contains("+") || (aux2[0].Contains("*") && !aux2[0].Contains(".*")) || aux2[0].Contains("/"))
                        return $"{FormatColumn(aux2[0],delimiters)} AS {FormatColumn(aux2[1], delimiters)}";
                    return $"{FormatColumn(aux2[0], delimiters)} AS {FormatColumn(aux2[1], delimiters)}";
                }
                else if (aux2.Length == 1) // as columna
                    return FormatColumn(aux2[0], delimiters);

            }
            if (columna.Contains("("))//Funcion()
            {
                return columna;
            }

            if (columna.Contains("."))//BaseDeDatos.Esquema.Tabla.Columna
            {
                string[] aux = columna.Split('.');
                foreach (var item in aux)
                {
                    if (item == "*") colFormateada += item; //Columna.*
                    else
                    {
                        if(item.Contains(" * ") || item.Contains(" - ") || item.Contains(" + ") || item.Contains(" / "))//columna (*,+,-,/) valor
                        {
                            var colAux = item.Split(' ')[0];
                            var resto = item.Substring(colAux.Length, item.Length - colAux.Length);
                            colFormateada += Delimit(colAux, delimiters) + resto;
                        }
                        else
                        colFormateada += $"{Delimit(item, delimiters)}.";
                    }
                        
                }
                colFormateada = colFormateada.Trim('.');
            }
            else colFormateada = Delimit(columna, delimiters);
            return colFormateada;
        }
        public static string FormatTable(string tabla, char[] delimiters)
        {
            if (string.IsNullOrEmpty(tabla)) return "";
            string output = tabla.Trim();
            if (output.StartsWith(delimiters[0].ToString()))
                return output;
            if (output.Contains(" "))// table alias
            {
                var aux = output.Split(' ');
                if (aux.Length == 2)
                    return $"{FormatColumn(aux[0], delimiters)} {FormatColumn(aux[1], delimiters)}";
                else return output;
            }
            return FormatColumn(output, delimiters);
        }
        private static string Delimit(string value, char[] delimiter)
        {
            if (string.IsNullOrEmpty(value) && delimiter[0] != '\'') return "";
            return delimiter[0] + value + delimiter[1];
        }


    }
}
