using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Mane.BD
{
    /// <summary>
    /// Modelo
    /// </summary>
    public  class Modelo : INotifyPropertyChanged
    {

        #region Constructores
        /// <summary>
        /// Modelo
        /// </summary>
        public Modelo()
        {
            idValue = "";
            OriginalModel = null;
        }

        #endregion

        #region Propiedades

        #region Propiedades virtuales
        /// <summary>
        /// Nombre de la tabla
        /// </summary>
        protected virtual string NombreTabla { get; }
        /// <summary>
        /// Nombre de la columna de clave primaria
        /// </summary>
        protected virtual string idName { get => "id"; }
        /// <summary>
        /// Nombre de la conexión que utilizará la tabla
        /// </summary>
        protected virtual string ConnName { get => ""; }


        /// <summary>
        /// Tipo de modelo
        /// </summary>
        protected static Type TipoModelo { get => typeof(Modelo); }

        #endregion
        /// <summary>
        /// El id insertado
        /// </summary>
        protected object idValue { get; set; }
        /// <summary>
        /// Indica si el modelo ya fue registrado en la base de datos
        /// </summary>
        private bool RegistredOnDataBase;
        ///// <summary>
        ///// Obtiene la relacion especificada
        ///// </summary>
        ///// <typeparam name="Tmodelo">Tipo de modelo</typeparam>
        ///// <param name="nombreRel">Nombre de la relacion</param>
        ///// <returns>Retorna coleccion de modelos de la relacion</returns>
        //public ModeloCollection<Tmodelo> Relaciones<Tmodelo>(string nombreRel) where Tmodelo : Modelo
        //{
        //    if (_Relaciones.ContainsKey(nombreRel))
        //        return (ModeloCollection<Tmodelo>)_Relaciones[nombreRel];
        //    with(nombreRel);
        //    if (_Relaciones.ContainsKey(nombreRel))
        //        return (ModeloCollection<Tmodelo>)_Relaciones[nombreRel];
        //    return new ModeloCollection<Tmodelo>();
        //}
        ///// <summary>
        ///// Obtiene una relacion especifica
        ///// </summary>
        ///// <typeparam name="Tmodelo">Tipo de modelo</typeparam>
        ///// <param name="nombreRel">Nombre de la relacion</param>
        ///// <returns>Devuele el primer modelo de la relacion</returns>
        //public Tmodelo Relacion<Tmodelo>(string nombreRel) where Tmodelo : Modelo
        //{
        //    var modelos = Relaciones<Tmodelo>(nombreRel);
        //    if (modelos.Count == 0) return null;
        //    return modelos[0];
        //}
        #endregion

        #region Atributos
        /// <summary>
        /// (Helper) nombre actual de la relacion
        /// </summary>
        protected string currRelName;
        protected Dictionary<string, object> OriginalModel;

        private object originalIdValue;
        private BindingSource bindingSource;
        protected string _LastError;
        #endregion

        #region Eventos
        /// <summary>
        /// Envento que es invoncado cada que el valor de una propiedad cambia
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler<ModeloExceptionEventArgs> OnException;
        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Inserta un nuevo registro y devuelve su ID
        /// </summary>
        private object Add()
        {
            var q = new QueryBuilder(NombreTabla);
            idValue = q.insert(Common.ObjectToKeyValue(this), ConnName);
            RegistredOnDataBase = true;
            return idValue;
        }

        /// <summary>
        /// Realiza un insert si el modelo no existe en la BD o realiza un update si ya existe
        /// </summary>
        public void Save()
        {
            try
            {
                if (RegistredOnDataBase)
                    Update();
                else Add();
            }
            catch (Exception e)
            {
                ModeloExceptionHandler(e);
            }
        }
       

        /// <summary>
        /// Elimina un registro
        /// </summary>
        public void Delete()
        {
            try
            {
                if (!RegistredOnDataBase) return;
                var q = new QueryBuilder(NombreTabla);
                q.where(idName, idValue).delete(ConnName);
            }
            catch (Exception e)
            {
                ModeloExceptionHandler(e);
            }
        }
        /// <summary>
        /// Actualiza en base de datos
        /// </summary>
        private void Update()
        {
            if (!IsDirty()) return;
            var q = new QueryBuilder(NombreTabla);
            q.where(idName, idValue);
            var CurrModel = Common.ObjectToKeyValue(this);
            //Actualizar solo los valores que cambiaron respecto al original
            if (OriginalModel != null)
            {
                var propsActualizadas = new Dictionary<string, object>();
                foreach (var key in CurrModel.Keys)
                {
                    if (OriginalModel[key] != CurrModel[key])
                        propsActualizadas.Add(key, CurrModel[key]);
                }
                if (id().ToString() != originalIdValue.ToString())
                    propsActualizadas.Add(getIdName(), id());
                if (propsActualizadas.Count > 0)
                {
                    q.update(propsActualizadas, ConnName);
                    OriginalModel = CurrModel;
                    originalIdValue = id();
                }

            }
            else
                q.update(CurrModel, ConnName);

        }

        /// <summary>
        /// Obtiene el nombre de la tabla
        /// </summary>
        /// <returns>Nombre de la tabla a la que hace referencia</returns>
        public string getNombreTabla()
        {
            return NombreTabla;
        }

        /// <summary>
        /// Obtiene el nombre de la columna con la clave primaria
        /// </summary>
        /// <returns>Nombre de la columna primaria</returns>
        public string getIdName() => idName;
        /// <summary>
        /// Obtiene el último error generado en el objeto
        /// </summary>
        /// <returns>Ultimo error generado en el objeto</returns>
        public string GetLastError() => _LastError == null ? "" : _LastError;
        /// <summary>
        /// Identificador del objeto actual
        /// </summary>
        /// <returns>Devuelve el identificador del objeto actual</returns>
        public object id() => idValue;
        /// <summary>
        /// Establece el valor del id
        /// </summary>
        /// <param name="value">valor de id</param>
        public void setId(object value) => idValue = value;

        /// <summary>
        /// Indica se el modelo ya se registró en la base de datos
        /// </summary>
        /// <returns></returns>
        public bool exists() => RegistredOnDataBase;

        internal void setExists(bool exists) => RegistredOnDataBase = exists;
        internal void setOriginalIdValue(object val) => originalIdValue = val;


        /// <summary>
        /// Obtiene el nombre de la conexión del objeto
        /// </summary>
        /// <returns>Nombre de la conexión</returns>
        public string getConnName() => this.ConnName;

        internal void setOriginalModel(Dictionary<string, object> dic) => OriginalModel = dic;
        internal Dictionary<string, object> getOriginalModel() => OriginalModel;

        #endregion

        #region Metodos relacionales
        /// <summary>
        /// Obtiene colección de objetos relacionados
        /// </summary>
        /// <typeparam name="Tmodelo">Tipo de modelo</typeparam>
        /// <param name="ColumnaIdLocal">Columna local de relación</param>
        /// <param name="ColumnaIdForaneo">Columna de la tabla foranea que se relaciona con la tabla actual</param>
        /// <returns>Collección de Modelos relacionados</returns>
        protected Modelo<Tmodelo>.ModeloCollection oneToMany<Tmodelo>(string ColumnaIdLocal, string ColumnaIdForaneo) where Tmodelo : Modelo, new()
        {
            try
            {
                string idRelacionado = ColumnaIdLocal == idName ? id()?.ToString() : Common.GetPropValueByName(this, ColumnaIdLocal).ToString();
                if (idRelacionado == "") return new Modelo<Tmodelo>.ModeloCollection();
                Tmodelo m = new Tmodelo();
                DataTable dt = Bd.Query(m.getNombreTabla()).select(ColumnasDelModelo<Tmodelo>())
                    .where($"{m.NombreTabla}.{ColumnaIdForaneo}", idRelacionado)
                    .get(this.ConnName);
                string nombreRel = (currRelName == "" || currRelName == null) ? typeof(Tmodelo).Name : currRelName;
                var relacion = Modelo<Tmodelo>.DataTableToModeloCollection(dt);
                return relacion;
            }
            catch (Exception e)
            {
                ModeloExceptionHandler(e);
            }
            return new Modelo<Tmodelo>.ModeloCollection();
        }

        public bool IsDirty()
        {
            if (!exists()) return false;
            if (originalIdValue != null && id() != null)
                if (originalIdValue?.ToString() != id()?.ToString()) return true;
            return OriginalModel != Common.ObjectToKeyValue(this);
        }
        /// <summary>
        /// Tiene uno a traves de...
        /// </summary>
        /// <typeparam name="TmodeloResultante">Tipo de modelo resultante</typeparam>
        /// <typeparam name="TmodeloIntermedio">Tipo de modelo intermedio</typeparam>
        /// <param name="localKey">Nombre de la columna del modelo actual que se relaciona con la tabla intermedia</param>
        /// <param name="middleKey">Nombre de la columna del modelo intermedio que se relaciona con el modelo actual</param>
        /// <param name="foreginKey">Nombre de la columna del modelo resultante que se relaciona con la tabla intermedia</param>
        /// <returns>Modelo de tipo TmodeloResultante o nulo si no existe</returns>
        public TmodeloResultante hasOneThrough<TmodeloResultante, TmodeloIntermedio>(string localKey = "", string middleKey = "", string foreginKey = "") where TmodeloResultante : Modelo, new() where TmodeloIntermedio : Modelo, new()
        {

            var modelo = new TmodeloResultante();

            try
            {
                var modeloIntermedio = new TmodeloIntermedio();
                if (foreginKey == "")
                    foreginKey = modelo.idName;
                if (middleKey == "")
                    middleKey = modeloIntermedio.idName;

                string localKeyValue;
                if (localKey != "")
                    localKeyValue = Common.GetPropValueByName(this, localKey).ToString();
                else localKeyValue = id()?.ToString();

                return Modelo<TmodeloResultante>.Query().where($"{modelo.NombreTabla}.{foreginKey}", q =>
                  q.from(modeloIntermedio.NombreTabla)
                  .select(middleKey)
                  .limit(1)
                  .where($"{modeloIntermedio.NombreTabla}.{middleKey}", localKeyValue)
                  ).first();
            }
            catch (Exception e)
            {
                ModeloExceptionHandler(e);
            }
            return modelo;
        }


        /// <summary>
        /// Obtiene las columnas del modelo
        /// </summary>
        /// <typeparam name="Tmodelo">Tipo de modelo</typeparam>
        /// <param name="incluirNombreTabla">Indica si el arreglo retornara Tabla.Columna si es verdadero, por defecto retorna Columna</param>
        /// <returns>Arreglo con las columnas del modelo incluyendo ID</returns>
        public static string[] ColumnasDelModelo<Tmodelo>(bool incluirNombreTabla = false) where Tmodelo : Modelo, new()
        {
            return ColumnasDelModelo(new Tmodelo(), incluirNombreTabla);
        }
        /// <summary>
        /// Obtiene las columnas del modelo
        /// </summary>
        /// <param name="m">Modelo</param>
        /// <param name="incluirNombreTabla">Indica si el arreglo retornara Tabla.Columna si es verdadero, por defecto retorna Columna</param>
        /// <returns>Arreglo con las columnas del modelo incluyendo ID</returns>
        public static string[] ColumnasDelModelo(Modelo m, bool incluirNombreTabla = false)
        {
            List<string> columnasDelModelo = new List<string>();
            columnasDelModelo.AddRange(Common.ObjectToKeyValue(m).Keys);
            columnasDelModelo.Add(m.idName);
            if (incluirNombreTabla)
            {
                var list = new List<string>();
                foreach (string item in columnasDelModelo)
                {
                    list.Add($"{m.NombreTabla}.{item}");
                }
                return list.ToArray();
            }
            return columnasDelModelo.ToArray();
        }
        /// <summary>
        /// Relacion uno a uno
        /// </summary>
        /// <typeparam name="Tmodelo">Tipo de modelo</typeparam>
        /// <param name="ColumnaIdLocal">Nombre de la columna local relacionada con el Tmodelo</param>
        /// <param name="ColumnaIdForanea">Nombre de la columna foranea</param>
        /// <returns>Modelo relacionado o nulo si no existe</returns>
        protected Tmodelo oneToOne<Tmodelo>(string ColumnaIdLocal, string ColumnaIdForanea) where Tmodelo : Modelo, new()
        {
            try
            {
                Tmodelo m = new Tmodelo();
                string idRelacionado = ColumnaIdLocal == idName ? idValue?.ToString() : Common.GetPropValueByName(this, ColumnaIdLocal)?.ToString();
                if(string.IsNullOrEmpty(idRelacionado))return null;
                var dt = Bd.Query(m.NombreTabla).select(ColumnasDelModelo<Tmodelo>())
                    .where(ColumnaIdForanea, idRelacionado).get(m.ConnName);

                if (dt.Rows.Count > 0)
                    return Modelo<Tmodelo>.DataTableToModeloCollection(dt)[0];
            }
            catch (Exception e)
            {
                ModeloExceptionHandler(e);
            }
            return null;

        }

        /// <summary>
        /// Relacion muchos a muchos
        /// </summary>
        /// <typeparam name="Tmodelo">Tipo de modelo resultante</typeparam>
        /// <param name="localKey">Nombre del id local relacionado con la tabla muchos a muchos</param>
        /// <param name="manyToManyTable">Nombre de la tabla muchos a muchos</param>
        /// <param name="manyToManyToLocalKey">Nombre de la columna de la tabla muchos a muchos que se relaciona con la tabla del modelo actual</param>
        /// <param name="manyToManyToForeginKey">Nombre de la columna de la tabla muchos a muchos que se relaciona con la tabla del modelo resultante</param>
        /// <param name="foreginKey">Nombre de la columna del modelo resultante que se relaciona con la tabla muchos a muchos</param>
        /// <returns></returns>
        protected Modelo<Tmodelo>.ModeloCollection manyToMany<Tmodelo>(string localKey, string manyToManyTable, string manyToManyToLocalKey, string manyToManyToForeginKey, string foreginKey) where Tmodelo : Modelo, new()
        {
            try
            {
                var m = new Tmodelo();
                return Modelo<Tmodelo>.DataTableToModeloCollection(
                    Bd.Query(NombreTabla)
                    .join(manyToManyTable, $"{manyToManyTable}.{manyToManyToLocalKey}", $"{NombreTabla}.{localKey}")
                    .join(m.NombreTabla, $"{m.NombreTabla}.{foreginKey}", $"{manyToManyTable}.{manyToManyToForeginKey}")
                    .where($"{NombreTabla}.{localKey}", localKey == idName ? idValue : Common.GetPropValueByName(this, localKey))
                    .select(ColumnasDelModelo(m, true))
                    .get(ConnName)
                    );
            }
            catch (Exception e)
            {
                ModeloExceptionHandler(e);
            }
            return new Modelo<Tmodelo>.ModeloCollection();
        }
        #endregion

        


        #region Compatibilidad con forms
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

        private void SetBindingToControl(Control c)
        {
            object tag = c.Tag;
            if (tag == null) return;
            object contexto = toContextoBinding(tag);//Convertir el tag a contextobinding
            if (contexto == null) return;
            Type tipoContexto = contexto.GetType();
            if (tipoContexto == typeof(ContextoBinding))
            {
                var aux = (ContextoBinding)contexto;
                var pInfo = this.GetType().GetProperty(aux.PropiedadDeModelo);
                if (pInfo != null)
                {
                    var bind = new Binding(aux.PropiedadDelObjeto, bindingSource, aux.PropiedadDeModelo);
                    if (aux.NegarProp)
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
                    if (!string.IsNullOrEmpty(aux.Format))
                    {
                        bind.FormattingEnabled = true;
                        bind.FormatString = aux.Format;
                        bind.Format += new ConvertEventHandler((s, e) =>
                        {
                            try
                            {
                                if (e.DesiredType == typeof(string))
                                    e.Value = Convert.ToDouble(e.Value).ToString((s as Binding).FormatString);
                            }
                            catch (Exception ex)
                            {

                            }
                        });
                    }
                    c.DataBindings.Clear();
                    c.DataBindings.Add(bind);
                }

            }
            else if (tipoContexto == typeof(ContextoBinding[]))
            {
                foreach (var item in (ContextoBinding[])contexto)
                {
                    if (GetType().GetProperty(item.PropiedadDeModelo) != null)
                        c.DataBindings.Add(item.PropiedadDelObjeto, bindingSource, item.PropiedadDeModelo);
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
        #endregion

        #region Equals
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(Modelo left, Modelo right)
        {
            return EqualityComparer<Modelo>.Default.Equals(left, right);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(Modelo left, Modelo right)
        {
            return !(left == right);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            return obj is Modelo modelo &&
                   NombreTabla == modelo.NombreTabla &&
                   ConnName == modelo.ConnName &&
                   idValue == modelo.idValue;
        }

        internal void ModeloExceptionHandler(Exception e)
        {
            var ex = new ModeloException(this.GetType(), e.Message);
            if (OnException == null)
                throw ex;
            else OnException.Invoke(this, new ModeloExceptionEventArgs(ex));

        }

        public override int GetHashCode()
        {
            int hashCode = -1638527336;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NombreTabla);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(idName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ConnName);
            hashCode = hashCode * -1521134295 + EqualityComparer<object>.Default.GetHashCode(idValue);
            return hashCode;
        }

        #endregion
    }
}
