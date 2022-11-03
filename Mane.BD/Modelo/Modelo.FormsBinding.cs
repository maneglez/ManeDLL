using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mane.BD
{
        
    public  partial class Modelo : INotifyPropertyChanged
    {
        /// <summary>
        /// Envento que es invoncado cada que el valor de una propiedad cambia
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Vincula las propiedades del modelo a los controles
        /// </summary>
        /// <param name="form">Fprmulario a vincular</param>
        /// <remarks>
        /// <para>Para que un control sea vinculable debe de asingarse al control en la propedad Tag, los valores del
        /// nombre de la propiedad del objeto seguido del nombre de la propiedad del modelo ejemplo: Control.Tag = "{PropiedadDeControl,PropiedadDelModelo}".
        /// Tambien se puede agregar el vinculo a varios atributos del control de la siguiente forma: Control.Tag = "{PropiedadControl1,PropiedadModelo1},{PropiedadControl2,PropiedadModelo}"</para>
        /// </remarks>
        public void BindToForm(Form form)
        {
            if (form == null) throw new ArgumentNullException("form");
            if (form.Controls.Count == 0) return;
            BindToControls(form.Controls.Cast<Control>());

        }

        /// <summary>
        /// Vincula las propiedades del modelo a los controles
        /// </summary>
        /// <param name="controles">Controles a vincular con el objeto</param>
        /// <remarks>
        /// <para>Para que un control sea vinculable debe de asingarse al control en la propedad Tag, los valores del
        /// nombre de la propiedad del objeto seguido del nombre de la propiedad del modelo ejemplo: Control.Tag = "{PropiedadDeControl,PropiedadDelModelo}".
        /// Tambien se puede agregar el vinculo a varios atributos del control de la siguiente forma: Control.Tag = "{PropiedadControl1,PropiedadModelo1},{PropiedadControl2,PropiedadModelo}"</para>
        /// </remarks>
        public void BindToControls(IEnumerable<Control> controles)
        {
            if (bindingSource == null)
            {
                bindingSource = new BindingSource();
                bindingSource.Add(this);
            }
            foreach (Control c in controles)
            {
                SetBindingToControl(c);
                if (c.Controls.Count > 0)
                {
                    BindToControls(c.Controls.Cast<Control>());
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controles"></param>
        public void BindToControls(Control[] controles)
        {
            if (bindingSource == null)
            {
                bindingSource = new BindingSource();
                bindingSource.Add(this);
            }
            foreach (Control c in controles)
            {
                SetBindingToControl(c);
                if (c.Controls.Count > 0)
                {
                    BindToControls(c.Controls.Cast<Control>());
                }

            }
        }

        private void AddBindTocontrol(Control control, ContextoBinding contexto)
        {
            var pInfo = this.GetType().GetProperty(contexto.PropiedadDeModelo);
            if (pInfo != null)
            {
                var bind = new Binding(contexto.PropiedadDelObjeto, bindingSource, contexto.PropiedadDeModelo);
                if (contexto.NegarProp)
                {
                    bind.FormattingEnabled = true;
                    bind.DataSourceUpdateMode = DataSourceUpdateMode.Never;
                    bind.Format += new ConvertEventHandler((s, e) =>
                    {
                        try
                        {
                            e.Value = !Convert.ToBoolean(e.Value);
                        }
                        catch (Exception) { }
                    });
                }
                if (!string.IsNullOrEmpty(contexto.Format))
                {
                    bind.FormattingEnabled = true;
                    bind.FormatString = contexto.Format;
                    bind.Format += new ConvertEventHandler((s, e) =>
                    {
                        try
                        {
                            if (e.DesiredType == typeof(string))
                                e.Value = Convert.ToDouble(e.Value).ToString((s as Binding).FormatString);
                        }
                        catch (Exception)
                        {

                        }
                    });
                }
                control.DataBindings.Clear();
                control.DataBindings.Add(bind);
            }
        }
        private void SetBindingToControl(Control c)
        {
            object tag = c.Tag;
            if (tag == null) return;
            object contexto = toContextoBinding(tag);//Convertir el tag a contextobinding
            if (contexto == null) return;
            Type tipoContexto = contexto.GetType();
            if (tipoContexto == typeof(ContextoBinding))
            {
                AddBindTocontrol(c, contexto as ContextoBinding);
            }
            else if (tipoContexto == typeof(ContextoBinding[]))
            {
                c.DataBindings.Clear();
                foreach (var item in (ContextoBinding[])contexto)
                {
                    AddBindTocontrol(c, item);
                }
            }
        }
        private dynamic toContextoBinding(object tag)
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
                    if (GetType().Name != clase_propName[0])//Verificar que la clase correspoda a esta clase
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
        /// notificar que la propiedad cambia
        /// </summary>
        /// <param name="prop"></param>
        protected void NotifyPropChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
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
            public string PropiedadDeModelo
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
            public string PropiedadDelObjeto { get; set; }

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
                PropiedadDelObjeto = "";
                PropiedadDeModelo = "";
                Format = "";
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="propiedadDeModelo">Nombre del atributo del modelo a vincular</param>
            /// <param name="propiedadDelObjeto">Nombre de la propiedad del objeto al que se vinculara el modelo</param>
            public ContextoBinding(string propiedadDelObjeto, string propiedadDeModelo)
            {
                PropiedadDeModelo = propiedadDeModelo;
                PropiedadDelObjeto = propiedadDelObjeto;
            }
        }
       
    }
}
