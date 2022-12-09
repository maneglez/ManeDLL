using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Data;

namespace Mane.Helpers
{
    public static class Extensiones
    {
        public static string ToJsonString<T>(this T obj) where T: class, new()
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
        public static void SetPropertyValue<T>(this T obj,string PropertyName, object value) where T : class, new()
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
        public static void CopiarPropiedadesAForm<T>(this T obj,Form fm) where T:class, new()
        {
            obj.CopiarPropiedadesAControles(GetAllControls(fm));
        }
        public static void CopiarAForm(this DataTable dt,Form fm)
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
                    if (!string.IsNullOrEmpty(bind.Format))
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
        public static void CopiarPropiedadesAControles<T>(this T obj,IEnumerable<Control> controls) where T : class,new()
        {
            foreach (Control control in controls)
            {
                try
                {
                    var bind = (ContextoBinding)ToContextoBinding(control.Tag,obj);
                    if (bind == null) continue;
                    if (!string.IsNullOrEmpty(bind.Format))
                    {
                        control.SetPropertyValue(bind.PropiedadDelControl, (obj.GetPropertyValue(bind.PropiedadDelObjeto) as IFormattable).ToString(bind.Format,null));
                        continue;
                    }
                    if (bind.NegarProp)
                    {
                        control.SetPropertyValue(bind.PropiedadDelControl, !(bool)obj.GetPropertyValue(bind.PropiedadDelObjeto));
                        continue;
                    }
                    if(bind.PropiedadDelControl == "Text")
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
                if (string.IsNullOrEmpty(strBind)) continue;
                var properties = strBind.Split(',');
                if (properties.Length < 2) continue;
                if (properties[1].Contains("."))//especifica clase
                {
                    var clase_propName = properties[1].Split('.');
                    if (obj.GetType()?.Name != clase_propName[0])//Verificar que la clase correspoda a esta clase
                        continue;
                    if (clase_propName.Length != 2) continue;
                    if (string.IsNullOrEmpty(clase_propName[1])) continue;
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
