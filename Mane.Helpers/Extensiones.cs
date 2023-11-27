using Mane.Helpers.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace Mane.Helpers
{
    public static class Extensiones
    {
        /// <summary>
        /// Elimina los elementos que cumplan una condicion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        public static void RemoveWhere<T>(this IList<T> list, Func<T, bool> predicate)
        {
            var itemsToRemove = new List<T>();
            itemsToRemove.AddRange(list.Where(predicate));
            foreach (var item in itemsToRemove)
            {
                list.Remove(item);
            }
        }
        /// <summary>
        /// Elimina los elementos que cumplan una condicion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="predicate"></param>
        public static void RemoveWhere<T>(this ICollection<T> list, Func<T, bool> predicate)
        {
            var itemsToRemove = new List<T>();
            itemsToRemove.AddRange(list.Where(predicate));
            foreach (var item in itemsToRemove)
            {
                list.Remove(item);
            }
        }
        /// <summary>
        /// Elimina los elementos que cumplan una condicion
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="predicate"></param>
        public static void RemoveWhere(this DataRowCollection rows, Func<DataRow, bool> predicate)
        {
            var itemsToRemove = new List<DataRow>();
            var aux = rows.Cast<DataRow>();
            itemsToRemove.AddRange(aux.Where(predicate));
            foreach (var item in itemsToRemove)
            {
                rows.Remove(item);
            }
            itemsToRemove.Clear();
        }
        public static void AbrirOpcion(this Panel PanelEjecucion, Form Mostrar,bool mantenerVisible = true,bool mostrarBordes = true) //Mostrar una forma en un panel
        {
            Mostrar.WindowState = FormWindowState.Maximized;
            if (PanelEjecucion.Controls.Count > 0)
            {
                bool match = false;
                foreach (Control item in PanelEjecucion.Controls)//ocultar los demás forms
                {
                    if (!(item is Form)) continue;
                    if (item.Name == Mostrar.Name)
                    {
                        item.Visible = true;
                        match = true;
                        if (mantenerVisible)
                        {
                            item.BringToFront();
                        }
                    }
                    else
                    {
                        if(!mantenerVisible)
                        item.Visible = false;
                    }
                        
                }
                if (match)//Si el control ya estaba inicializado
                {
                    Mostrar.Dispose();//Libera el form Recibido como parámetro
                    return;
                }
            }
            if(!mostrarBordes)
            Mostrar.FormBorderStyle = FormBorderStyle.None;
            Mostrar.TopLevel = false; //Se le dice que no es un objeto de alto nivel
            if (mostrarBordes)
                Mostrar.WindowState = FormWindowState.Maximized;
            Mostrar.Dock = DockStyle.Fill; //En el panel se llena completamente
            PanelEjecucion.Controls.Add(Mostrar); //Se agrega el elemento al panel
            Mostrar.Show();

        }
        public static void ExportToCSV(this DataGridView dgv)
        {
            string fileName = "";
            using (var fm = new SaveFileDialog())
            {
                fm.Filter = "csv files (*.csv)|*.csv";
                fm.DefaultExt = ".csv";
                if (fm.ShowDialog() == DialogResult.OK)
                {
                    fileName = fm.FileName;
                }
            }
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            using (var sw = new StreamWriter(new FileStream(fileName, FileMode.Create), Encoding.UTF8))
            {
                var columns = "";
                foreach (DataGridViewColumn c in dgv.Columns)
                {
                    columns += $"\"{c.HeaderText.Replace("\"","")}\",";
                }
                sw.WriteLine(columns.TrimEnd(','));
                foreach (DataGridViewRow r in dgv.Rows)
                {
                    string row = "";
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if (c.Value != null && c.Value != DBNull.Value)
                            row += $"\"{c.Value.ToString().Replace("\"", "")}\"";
                        row += ",";
                    }
                    sw.WriteLine(row.TrimEnd(','));
                }
            }
            MsgBox.info("Exportado Correctamente!");

        }
        public static void CopiarTabla(this DataGridView grid)
        {
            Utils.CopiarTabla(grid);
        }
        public static void AddCopiarTablaOption(this DataGridView grid)
        {

            if (grid.ContextMenuStrip == null)
                grid.ContextMenuStrip = new ContextMenuStrip();
            var i = grid.ContextMenuStrip.Items.Add("Copiar Tabla");
            i.Click += (s, e) => grid.CopiarTabla();

        }
        public static string ToJsonString<T>(this T obj) where T : class, new()
        {
            try
            {
                return JsonSerializer.Serialize(obj);
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// Establece el valor de la propiedad de un objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="PropertyName"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue<T>(this T obj, string PropertyName, object value) where T : class, new()
        {
            try
            {
                obj.GetType().GetProperty(PropertyName).SetValue(obj, value);
            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// Obtiene el valor de una propiedad del objeto dado el nombre de ella
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="PropertyName"></param>
        /// <returns></returns>
        public static object GetPropertyValue<T>(this T obj, string PropertyName) where T : class, new()
        {
            try
            {
                return obj.GetType().GetProperty(PropertyName).GetValue(obj);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string Yyyymmdd(this DateTime d)
        {
            return d.ToString("yyyy-MM-dd");
        }

        public static T CopyObject<T>(this T source) where T : class,new()
        {
            if ( source == null)
                return null;
           var target = new T();
            Type sourceType = source.GetType();
            Type targetType = target.GetType();
            var sourceProperties = sourceType.GetProperties();
            foreach (var sProp in sourceProperties)
            {
                var tProp = targetType.GetProperty(sProp.Name);
                if (tProp == null)
                    continue;
                if (!sProp.CanRead || !tProp.CanWrite)
                    continue;
                if (sProp.PropertyType.IsValueType || sProp.PropertyType.IsPrimitive)
                {
                    tProp.SetValue(target, sProp.GetValue(source));
                    continue;
                }
                var sPropValue = sProp.GetValue(source);
                if (sProp.PropertyType.IsSerializable && !sProp.PropertyType.IsGenericType)
                {
                    try
                    {
                        var sPropCloneValue = sPropValue.CloneSerializer();
                        tProp.SetValue(target, sPropCloneValue);
                        continue;
                    }
                    catch
                    {
                        
                    }
                }

                //if (sProp.PropertyType.IsEnumerable())
                //{
                //    var sValues = (IEnumerable<object>)sPropValue;
                //    var tValues = (dynamic)Activator.CreateInstance(typeof(List<>).MakeGenericType(sProp.PropertyType.GenericTypeArguments.First()));
                //    foreach (var item in sValues)
                //    {
                //        tValues.Add(item.CopyObject());
                //    }
                //    tProp.SetValue(target, Enumerable.AsEnumerable(tValues));
                //}
                
            }
            return target;
        }

        public static bool IsEnumerable(this Type type)
        {
            return type.GetInterfaces().Any(t => t.FullName == "System.Collections.IEnumerable") ;
        }
        public static bool IsCollection(this Type type)
        {
            return type.GetInterfaces().Any(t => t.FullName == "System.Collections.ICollection");
        }

        public static T CloneSerializer<T>(this T source) where T : class
        {
            if (!typeof(T).IsSerializable)
                throw new ArgumentException("NonSerializable");

            if (Object.ReferenceEquals(source, null))
                return null;

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

       

        /// <summary>
        /// Limpia la información de series lotes y ubicaciones
        /// </summary>
        /// <param name="ln"></param>
        public static void ClearSubInfo(this ILineaDocumento ln)
        {
            ln.BinAllocations.Clear();
            ln.SerialNumbers.Clear();
            ln.BatchNumbers.Clear();
        }
        /// <summary>
        /// DbNull safe converter
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int ToInt(this object obj)
        {
            if (obj == null) return 0;
            if (obj.IsDbNull()) return 0;
            return Convert.ToInt32(obj);
        }
        /// <summary>
        /// DbNull safe converter
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ToDbl(this object obj)
        {
            if (obj == null) return 0;
            if (obj.IsDbNull()) return 0;
            return Convert.ToDouble(obj);
        }
        /// <summary>
        /// DbNull safe converter
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToStr(this object obj)
        {
            if (obj == null) return string.Empty;
            if (obj.IsDbNull()) return string.Empty;
            return Convert.ToString(obj);
        }

        public static bool IsDbNull(this object obj)
        {
            return obj == DBNull.Value;
        }

        public static List<T> ToObjectList<T>(this DataTable dt) where T : class, new()
        {
            var lst = new List<T>();
            var obj = new T();
            var type = obj.GetType();
            foreach (DataRow row in dt.Rows)
            {
                var newObj = new T();
                foreach (DataColumn col in dt.Columns)
                {
                  var prop =  type.GetProperty(col.ColumnName);
                    prop?.SetValue(newObj, Utils.ConvertirATipo(prop?.PropertyType, row[col.ColumnName]));
                }
                lst.Add(newObj);
            }
            return lst;
        }

        #region Generic Binding
        public static List<Control> GetAllControls(this Control control)
        {
            var controlsOrg = control.Controls.Cast<Control>().ToList();
            var controls = control.Controls.Cast<Control>().ToList();
            foreach (Control c in controlsOrg)
            {
                controls.AddRange(c.GetAllControls());
            }
            return controls;

        }
        public static void CopiarPropiedadesAForm<T>(this T obj, Form fm) where T : class, new()
        {
            obj.CopiarPropiedadesAControles(GetAllControls(fm));
        }
        public static void CopiarAForm(this DataTable dt, Form fm)
        {
            if (dt.Rows.Count == 0) return;
            var controls = fm.GetAllControls();
            var row = dt.Rows[0];
            foreach (Control control in controls)
            {
                try
                {
                    var bind = (ContextoBinding)ToContextoBinding(control.Tag);
                    if (bind == null) continue;
                    if (!string.IsNullOrWhiteSpace(bind.Format))
                    {
                        control.SetPropertyValue(bind.PropiedadDelControl, (row[bind.PropiedadDelObjeto] as IFormattable).ToString(bind.Format, null));
                        continue;
                    }
                    if (bind.NegarProp)
                    {
                        control.SetPropertyValue(bind.PropiedadDelControl, !(bool)row[bind.PropiedadDelObjeto]);
                        continue;
                    }
                    if (bind.PropiedadDelControl == "Text")
                    {
                        control.SetPropertyValue(bind.PropiedadDelControl, row[bind.PropiedadDelObjeto].ToString());
                        continue;
                    }
                    control.SetPropertyValue(bind.PropiedadDelControl, row[bind.PropiedadDelObjeto]);

                }
                catch (Exception)
                {

                }
            }
        }
        public static void CopiarPropiedadesAControles<T>(this T obj, IEnumerable<Control> controls) where T : class, new()
        {
            foreach (Control control in controls)
            {
                try
                {
                    var bind = (ContextoBinding)ToContextoBinding(control.Tag, obj);
                    if (bind == null) continue;
                    if (!string.IsNullOrWhiteSpace(bind.Format))
                    {
                        control.SetPropertyValue(bind.PropiedadDelControl, (obj.GetPropertyValue(bind.PropiedadDelObjeto) as IFormattable).ToString(bind.Format, null));
                        continue;
                    }
                    if (bind.NegarProp)
                    {
                        control.SetPropertyValue(bind.PropiedadDelControl, !(bool)obj.GetPropertyValue(bind.PropiedadDelObjeto));
                        continue;
                    }
                    if (bind.PropiedadDelControl == "Text")
                    {
                        control.SetPropertyValue(bind.PropiedadDelControl, obj.GetPropertyValue(bind.PropiedadDelObjeto).ToString());
                        continue;
                    }
                    control.SetPropertyValue(bind.PropiedadDelControl, obj.GetPropertyValue(bind.PropiedadDelObjeto));

                }
                catch (Exception)
                {

                }
            }
        }

        private static dynamic ToContextoBinding(object tag, object obj = null)
        {
            if (tag == null) return null;
            var aux = tag.ToString();
            if (!aux.Contains("}")) return null;
            aux = aux.Replace("{", "");
            var strBinds = aux.Split('}');
            var bindings = new List<ContextoBinding>();
            foreach (var strBind in strBinds)
            {
                if (string.IsNullOrWhiteSpace(strBind)) continue;
                var properties = strBind.Split(',');
                if (properties.Length < 2) continue;
                if (properties[1].Contains("."))//especifica clase
                {
                    var clase_propName = properties[1].Split('.');
                    if (obj.GetType()?.Name != clase_propName[0])//Verificar que la clase correspoda a esta clase
                        continue;
                    if (clase_propName.Length != 2) continue;
                    if (string.IsNullOrWhiteSpace(clase_propName[1])) continue;
                    properties[1] = clase_propName[1];
                }
                var bind = new ContextoBinding(properties[0], properties[1]);
                if (properties.Length == 3)//especifica el formato
                    bind.Format = properties[2];
                bindings.Add(bind);
            }
            if (bindings.Count == 0) return null;
            if (bindings.Count == 1) return bindings[0];
            return bindings.ToArray();

        }
        public static object GetCellValue(this DataGridView grid, DataGridViewCellEventArgs e)
        {
            return grid.Rows[e.RowIndex].Cells[e.ColumnIndex]?.Value;
        }
        public static object GetCellValue(this DataGridView grid, DataGridViewCellCancelEventArgs e)
        {
            return grid.Rows[e.RowIndex].Cells[e.ColumnIndex]?.Value;
        }
        public static void SetCellValue(this DataGridView grid, DataGridViewCellEventArgs e, object value)
        {
            grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = value;
        }
        public static void SetCellValue(this DataGridView grid, DataGridViewCellCancelEventArgs e, object value)
        {
            grid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = value;
        }


        /// <summary>
        /// Indica el contexto de vinculo con un control de forms
        /// </summary>
        public class ContextoBinding
        {
            private string propiedadDeModelo;

            /// <summary>
            /// Nombre del atributo del modelo a vincular
            /// </summary>
            public string PropiedadDelObjeto
            {
                get => propiedadDeModelo; set
                {
                    if (value.Contains("!"))
                    {
                        NegarProp = true;
                        propiedadDeModelo = value.Remove(0, 1);
                    }
                    else
                        propiedadDeModelo = value;
                }
            }
            /// <summary>
            /// Nombre de la propiedad del objeto al que se vinculara el modelo
            /// </summary>
            public string PropiedadDelControl { get; set; }

            /// <summary>
            /// Indica si la propiedad se niega (Solo aplica para valores bool)
            /// </summary>
            public bool NegarProp { get; set; }
            /// <summary>
            /// Indica si la propiedad se niega (Solo aplica para valores bool)
            /// </summary>
            public string Format { get; set; }

            /// <summary>
            /// Constructor del Contexto de vinculo
            /// </summary>
            public ContextoBinding()
            {
                PropiedadDelControl = "";
                PropiedadDelObjeto = "";
                Format = "";
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="propiedadDeModelo">Nombre del atributo del modelo a vincular</param>
            /// <param name="propiedadDelObjeto">Nombre de la propiedad del objeto al que se vinculara el modelo</param>
            public ContextoBinding(string propiedadDelObjeto, string propiedadDeModelo)
            {
                PropiedadDelObjeto = propiedadDeModelo;
                PropiedadDelControl = propiedadDelObjeto;
            }
        }

        #endregion

    }
}
