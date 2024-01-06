using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            if (value is byte[] bytes)//Convertir arreglo de bytes en string sql binary
                return "0x" + string.Join("", bytes.Select(b => b.ToString("x2")));
            string strVal = value.ToString();
            if (double.TryParse(strVal, out double dval))
                return strVal;
            return Delimit(value.ToString(), delimiters);
        }
        public static string FormatColumn(string columna, char[] delimiters)
        {
            if (string.IsNullOrWhiteSpace(columna)) return "";
            string colFormateada = "";
            if (columna.StartsWith(delimiters[0].ToString()))
                return columna;
            if (columna.ToLower().Contains(" as ")) // Columna as otraCosa
            {
                string funcion = "";
                if (columna.Contains(")"))//Funcion()
                {
                    var splitFunc = columna.Split(')');
                    if (splitFunc.Length == 1)
                        return columna;//funcion( as )
                    else if (splitFunc.Length == 2)
                        if (string.IsNullOrWhiteSpace(splitFunc[1]))
                            return columna;//funcion( as )
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
                        return $"{FormatColumn(aux2[0], delimiters)} AS {FormatColumn(aux2[1], delimiters)}";
                    return $"{FormatColumn(aux2[0], delimiters)} AS {FormatColumn(aux2[1], delimiters)}";
                }
                else if (aux2.Length == 1) // as columna
                    return FormatColumn(aux2[0], delimiters);

            }
            if (columna.Contains("("))//Funcion()
                return columna;
            if (columna.StartsWith("'") && columna.EndsWith("'")) // 'valor'
                return columna;

            if (columna.Contains("."))//BaseDeDatos.Esquema.Tabla.Columna
            {
                string[] aux = columna.Split('.');
                foreach (var item in aux)
                {
                    if (item == "*") colFormateada += item; //Columna.*
                    else
                    {
                        if (item.Contains(" * ") || item.Contains(" - ") || item.Contains(" + ") || item.Contains(" / "))//columna (*,+,-,/) valor
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
            if (string.IsNullOrWhiteSpace(tabla)) return "";
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
            if (string.IsNullOrWhiteSpace(value) && delimiter[0] != '\'') return "";
            return delimiter[0] + value + delimiter[1];
        }

        internal static string GetColumnType(PropertyInfo propertyInfo,TipoDeBd tipoBd)
        {
            var basicTypeMap = new Dictionary<string, Dictionary<TipoDeBd, string>>();

            basicTypeMap.Add("System.String", new Dictionary<TipoDeBd, string>
            {
                {TipoDeBd.SqlServer,"VARCHAR(100)" },
                {TipoDeBd.Hana,"VARCHAR(100)" },
                {TipoDeBd.SQLite,"TEXT" }
            });

            basicTypeMap.Add("Mane.BD.TextAttribute", new Dictionary<TipoDeBd, string>
            {
                {TipoDeBd.SqlServer,"TEXT" },
                {TipoDeBd.Hana,"TEXT" },
                {TipoDeBd.SQLite,"TEXT" }
            });
            

            basicTypeMap.Add("System.Int32", new Dictionary<TipoDeBd, string>
            {
                {TipoDeBd.SqlServer,"INT" },
                {TipoDeBd.Hana,"INT" },
                {TipoDeBd.SQLite,"INTEGER" }
            }); 
            basicTypeMap.Add("Mane.BD.IntAttribute", basicTypeMap["System.Int32"]);
            basicTypeMap.Add("System.Int16", basicTypeMap["System.Int32"]);
            
            
            basicTypeMap.Add("System.Double", new Dictionary<TipoDeBd, string>
            {
                {TipoDeBd.SqlServer,"DECIMAL(19,6)" },
                {TipoDeBd.Hana,"DECIMAL(19,6)" },
                {TipoDeBd.SQLite,"NUMERIC" }
            });
            
            basicTypeMap.Add("System.Decimal", basicTypeMap["System.Double"]);

            basicTypeMap.Add("System.Byte[]", new Dictionary<TipoDeBd, string>
            {
                {TipoDeBd.SqlServer,"VARBINARY" },
                {TipoDeBd.Hana,"BLOB" },
                {TipoDeBd.SQLite,"BLOB" }
            });
            basicTypeMap.Add("System.DateTime", new Dictionary<TipoDeBd, string>
            {
                {TipoDeBd.SqlServer,"DATETIME" },
                {TipoDeBd.Hana,"TIMESTAMP" },
                {TipoDeBd.SQLite,"TEXT" }
            });
            basicTypeMap.Add("Mane.BD.DateTimeAttribute", basicTypeMap["System.DateTime"]);
            basicTypeMap.Add("Mane.BD.DateAttribute", new Dictionary<TipoDeBd, string>
            {
                {TipoDeBd.SqlServer,"DATE" },
                {TipoDeBd.Hana,"DATE" },
                {TipoDeBd.SQLite,"TEXT" }
            });

            if (propertyInfo.CustomAttributes.Count() == 0)
            {
                if (basicTypeMap.ContainsKey(propertyInfo.PropertyType.FullName))
                    return basicTypeMap[propertyInfo.PropertyType.FullName][tipoBd] + " NULL";
                else if (propertyInfo.PropertyType.BaseType.FullName == "System.Enum")
                    return basicTypeMap["System.Int32"][tipoBd];
                throw new Exception($"No se pudo convertir el modelo {propertyInfo.DeclaringType.FullName} campo {propertyInfo.Name} tipo del campo {propertyInfo.PropertyType.FullName}");
            }
            else
            {
                var attributes = propertyInfo.CustomAttributes.ToDictionary(a => a.AttributeType.FullName);
                var columnType = string.Empty;

                if (attributes.ContainsKey("Mane.BD.DecimalAttribute"))
                {
                    switch (tipoBd)
                    {
                        case TipoDeBd.SQLite:
                            columnType = "NUMERIC";
                            break;
                        default:
                            var ctorArguments = attributes["Mane.BD.DecimalAttribute"].ConstructorArguments;
                            columnType = $"DECIMAL({ctorArguments[0].Value},{ctorArguments[1].Value})";
                            break;
                    }
                }else if (attributes.ContainsKey("Mane.BD.VarcharAttribute"))
                {
                    if (tipoBd == TipoDeBd.SQLite)
                        columnType = "TEXT";
                    else
                    {
                        var len = (int)attributes["Mane.BD.DecimalAttribute"].ConstructorArguments[0].Value;
                        columnType = $"VARCHAR({(len == 0 ? "MAX" : len.ToString())})";
                    }
                }
                else if(basicTypeMap.ContainsKey(propertyInfo.PropertyType.FullName))
                {
                    columnType = basicTypeMap[propertyInfo.PropertyType.FullName][tipoBd];
                }
                else if(propertyInfo.PropertyType.BaseType.FullName == "System.Enum")
                {
                    columnType = basicTypeMap["System.Int32"][tipoBd];
                }
                else
                {
                    throw new Exception($"No se pudo convertir el modelo {propertyInfo.DeclaringType.FullName} campo {propertyInfo.Name} tipo del campo {propertyInfo.PropertyType.FullName}");
                }

                if (attributes.ContainsKey("Mane.BD.NotNullAttribute"))
                {
                    columnType += " NOT NULL";
                }
                else
                {
                    columnType += " NULL";
                }

                if (attributes.ContainsKey("Mane.BD.DefaultAttribute"))
                    columnType += " DEFAULT " + FormatValue(attributes["Mane.BD.DefaultAttribute"].ConstructorArguments[0].Value);
                return columnType;
            }
            
         
        }


    }
}
