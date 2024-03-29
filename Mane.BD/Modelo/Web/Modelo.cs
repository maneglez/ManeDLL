﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Mane.BD
{
    /// <summary>
    /// WebModel sin referencia a windowsforms
    /// </summary>
    public partial class WebModel
    {

        #region Constructores
        /// <summary>
        /// WebModel
        /// </summary>
        public WebModel()
        {
            idValue = null;
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
        protected static Type TipoModelo { get => typeof(WebModel); }

        #endregion
        /// <summary>
        /// El id insertado
        /// </summary>
        protected object idValue { get; set; }
        /// <summary>
        /// Indica si el modelo ya fue registrado en la base de datos
        /// </summary>
        protected bool RegistredOnDataBase;

        internal bool Inicializando { get; set; }
        /// <summary>
        /// Indica cuando se están asignando los valores obtenidos de la base de datos
        /// a las propiedades del objeto
        /// </summary>
        public bool isInitializing() => Inicializando;
        ///// <summary>
        ///// Obtiene la relacion especificada
        ///// </summary>
        ///// <typeparam name="Tmodelo">Tipo de modelo</typeparam>
        ///// <param name="nombreRel">Nombre de la relacion</param>
        ///// <returns>Retorna coleccion de modelos de la relacion</returns>
        //public WebModelCollection<Tmodelo> Relaciones<Tmodelo>(string nombreRel) where Tmodelo : WebModel
        //{
        //    if (_Relaciones.ContainsKey(nombreRel))
        //        return (WebModelCollection<Tmodelo>)_Relaciones[nombreRel];
        //    with(nombreRel);
        //    if (_Relaciones.ContainsKey(nombreRel))
        //        return (WebModelCollection<Tmodelo>)_Relaciones[nombreRel];
        //    return new WebModelCollection<Tmodelo>();
        //}
        ///// <summary>
        ///// Obtiene una relacion especifica
        ///// </summary>
        ///// <typeparam name="Tmodelo">Tipo de modelo</typeparam>
        ///// <param name="nombreRel">Nombre de la relacion</param>
        ///// <returns>Devuele el primer modelo de la relacion</returns>
        //public Tmodelo Relacion<Tmodelo>(string nombreRel) where Tmodelo : WebModel
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
        protected string _LastError;
        #endregion

        #region Eventos
        public event EventHandler<ModeloExceptionEventArgs> OnException;
        public event EventHandler OnSave;
        public event EventHandler OnDelete;
        #endregion

        #region Metodos Publicos

        /// <summary>
        /// Inserta un nuevo registro y devuelve su ID
        /// </summary>
        private object Add()
        {
            var q = new QueryBuilder(NombreTabla);
            idValue = q.Insert(Common.ObjectToKeyValue(this), ConnName);
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
            OnSave?.Invoke(this, new EventArgs());
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
                q.Where(idName, idValue).Delete(ConnName);
            }
            catch (Exception e)
            {
                ModeloExceptionHandler(e);
            }
            OnDelete?.Invoke(this, new EventArgs());
        }
        /// <summary>
        /// Actualiza en base de datos
        /// </summary>
        public void Update()
        {
            if (!IsDirty()) return;
            var q = new QueryBuilder(NombreTabla);
            q.Where(idName, idValue);
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
                if (Id().ToString() != originalIdValue.ToString())
                    propsActualizadas.Add(getIdName(), Id());
                if (propsActualizadas.Count > 0)
                {
                    q.Update(propsActualizadas, ConnName);
                    OriginalModel = CurrModel;
                    originalIdValue = Id();
                }

            }
            else
                q.Update(CurrModel, ConnName);

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
        public object Id() => idValue;
        public int IntId()
        {
            try
            {
                return Convert.ToInt32(idValue);
            }
            catch (Exception)
            {
                return 0;
            }
        }
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
        protected WebModel<Tmodelo>.WebModelCollection oneToMany<Tmodelo>(string ColumnaIdLocal, string ColumnaIdForaneo) where Tmodelo : WebModel, new()
        {
            try
            {
                string idRelacionado = ColumnaIdLocal == idName ? Id()?.ToString() : Common.GetPropValueByName(this, ColumnaIdLocal).ToString();
                if (string.IsNullOrWhiteSpace(idRelacionado)) return new WebModel<Tmodelo>.WebModelCollection();
                Tmodelo m = new Tmodelo();
                DataTable dt = Bd.Query(m.getNombreTabla()).Select(ColumnasDelModelo<Tmodelo>())
                    .Where($"{m.NombreTabla}.{ColumnaIdForaneo}", idRelacionado)
                    .Get(m.ConnName);
                string nombreRel = (currRelName == "" || currRelName == null) ? typeof(Tmodelo).Name : currRelName;
                var relacion = WebModel<Tmodelo>.DataTableToModeloCollection(dt);
                return relacion;
            }
            catch (Exception e)
            {
                ModeloExceptionHandler(e);
            }
            return new WebModel<Tmodelo>.WebModelCollection();
        }

        public bool IsDirty()
        {
            if (!exists()) return false;
            if (originalIdValue != null && Id() != null)
                if (originalIdValue.ToString() != Id().ToString()) return true;
            if (OriginalModel == null) return true;
            var CurrModel = Common.ObjectToKeyValue(this);
            if (CurrModel != OriginalModel) return true;
            return false;
        }
        /// <summary>
        /// Tiene uno a traves de...
        /// </summary>
        /// <typeparam name="TmodeloResultante">Tipo de modelo resultante</typeparam>
        /// <typeparam name="TmodeloIntermedio">Tipo de modelo intermedio</typeparam>
        /// <param name="localKey">Nombre de la columna del modelo actual que se relaciona con la tabla intermedia</param>
        /// <param name="middleKey">Nombre de la columna del modelo intermedio que se relaciona con el modelo actual</param>
        /// <param name="foreginKey">Nombre de la columna del modelo resultante que se relaciona con la tabla intermedia</param>
        /// <returns>WebModel de tipo TmodeloResultante o nulo si no existe</returns>
        public TmodeloResultante hasOneThrough<TmodeloResultante, TmodeloIntermedio>(string localKey = "", string middleKey = "", string foreginKey = "") where TmodeloResultante : WebModel, new() where TmodeloIntermedio : WebModel, new()
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
                else localKeyValue = Id()?.ToString();

                return WebModel<TmodeloResultante>.Query().Where($"{modelo.NombreTabla}.{foreginKey}", q =>
                  q.From(modeloIntermedio.NombreTabla)
                  .Select(middleKey)
                  .Limit(1)
                  .Where($"{modeloIntermedio.NombreTabla}.{middleKey}", localKeyValue)
                  ).First();
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
        public static string[] ColumnasDelModelo<Tmodelo>(bool incluirNombreTabla = false) where Tmodelo : WebModel, new()
        {
            return ColumnasDelModelo(new Tmodelo(), incluirNombreTabla);
        }
        /// <summary>
        /// Obtiene las columnas del modelo
        /// </summary>
        /// <param name="m">WebModel</param>
        /// <param name="incluirNombreTabla">Indica si el arreglo retornara Tabla.Columna si es verdadero, por defecto retorna Columna</param>
        /// <returns>Arreglo con las columnas del modelo incluyendo ID</returns>
        public static string[] ColumnasDelModelo(WebModel m, bool incluirNombreTabla = false)
        {
            var columnasDelModelo = new HashSet<string>();
            var keys = Common.ObjectToKeyValue(m).Keys;
            foreach (var item in keys)
            {
                columnasDelModelo.Add(item);
            }
            if (!string.IsNullOrWhiteSpace(m.idName))
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
        /// <returns>WebModel relacionado o nulo si no existe</returns>
        protected Tmodelo oneToOne<Tmodelo>(string ColumnaIdLocal, string ColumnaIdForanea) where Tmodelo : WebModel, new()
        {
            try
            {
                Tmodelo m = new Tmodelo();
                string idRelacionado = ColumnaIdLocal == idName ? idValue?.ToString() : Common.GetPropValueByName(this, ColumnaIdLocal)?.ToString();
                if (string.IsNullOrWhiteSpace(idRelacionado)) return null;
                var dt = Bd.Query(m.NombreTabla).Select(ColumnasDelModelo<Tmodelo>())
                    .Where(ColumnaIdForanea, idRelacionado).Get(m.ConnName);

                if (dt.Rows.Count > 0)
                    return WebModel<Tmodelo>.DataTableToModeloCollection(dt)[0];
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
        protected WebModel<Tmodelo>.WebModelCollection manyToMany<Tmodelo>(string localKey, string manyToManyTable, string manyToManyToLocalKey, string manyToManyToForeginKey, string foreginKey) where Tmodelo : WebModel, new()
        {
            try
            {
                var m = new Tmodelo();
                return WebModel<Tmodelo>.DataTableToModeloCollection(
                    Bd.Query(NombreTabla)
                    .Join(manyToManyTable, $"{manyToManyTable}.{manyToManyToLocalKey}", $"{NombreTabla}.{localKey}")
                    .Join(m.NombreTabla, $"{m.NombreTabla}.{foreginKey}", $"{manyToManyTable}.{manyToManyToForeginKey}")
                    .Where($"{NombreTabla}.{localKey}", localKey == idName ? idValue : Common.GetPropValueByName(this, localKey))
                    .Select(ColumnasDelModelo(m, true))
                    .Get(ConnName)
                    );
            }
            catch (Exception e)
            {
                ModeloExceptionHandler(e);
            }
            return new WebModel<Tmodelo>.WebModelCollection();
        }
        #endregion

        #region Equals
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(WebModel left, WebModel right)
        {
            return EqualityComparer<WebModel>.Default.Equals(left, right);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(WebModel left, WebModel right)
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
            if (!(obj is WebModel)) return false;
            var modelo = obj as WebModel;
            if (IntId() == 0 && modelo.IntId() == 0)
            {
                var thisType = GetType();
                var objType = modelo.GetType();
                if (thisType.FullName != objType.FullName) return false;
                var thisProps = thisType.GetProperties().ToList();
                var objProps = objType.GetProperties().ToList();
                if (thisProps.Count != objProps.Count) return false;
                try
                {
                    foreach (var prop in thisProps)
                    {
                        var objProp = objProps.Find(p => p.Name == prop.Name);
                        if (objProp == null) return false;
                        if (prop.GetValue(this)?.ToString() != objProp.GetValue(modelo)?.ToString()) return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            else
                return NombreTabla == modelo.NombreTabla &&
                       ConnName == modelo.ConnName &&
                       IntId() == modelo.IntId();
        }

        internal void ModeloExceptionHandler(Exception e)
        {
            var ex = new ModeloException(this.GetType(), e);
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
