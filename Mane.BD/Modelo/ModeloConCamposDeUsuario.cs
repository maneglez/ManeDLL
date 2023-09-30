using System;
using System.Collections.Generic;

namespace Mane.BD
{
    public class ModeloConCamposDeUsuario<T> : Modelo<T> where T : ModeloConCamposDeUsuario<T>, new()
    {
        /// <summary>
        /// Lista de campos de usuario del objeto
        /// </summary>
        protected virtual string[] UdfsList => new string[] { };
        private Dictionary<string, object> udfs;
        public ModeloConCamposDeUsuario()
        {
            udfs = new Dictionary<string, object>();
            foreach (var udf in UdfsList)
                udfs.Add(udf, null);
        }
        /// <summary>
        /// Obtener valor del campo de usuario
        /// </summary>
        /// <param name="udfName">Nombre del campo e usuario</param>
        /// <returns></returns>
        public object GetUdf(string udfName)
        {
            if (udfs.ContainsKey(udfName))
                return udfs[udfName];
            else return null;
        }
        /// <summary>
        /// Obtener valor del campo de usuario
        /// </summary>
        /// <param name="udfName">Nombre del campo e usuario</param>
        /// <returns></returns>
        public Tudf GetUdf<Tudf>(string udfName)
        {
            if (udfs.ContainsKey(udfName))
                return Common.ConvertirATipo(typeof(Tudf), udfs[udfName]);
            else return default;
        }
        /// <summary>
        /// Agregar Campo de usuario al objeto
        /// </summary>
        /// <param name="udfName">Nombre del campo de usuario</param>
        /// <param name="udfValue">valor</param>
        protected void AddUdf(string udfName, object udfValue)
        {
            udfs.Add(udfName, udfValue);
        }
        /// <summary>
        /// Asignar valor a campo de usuario
        /// </summary>
        /// <param name="udfName">Nombre del campo de usuario</param>
        /// <param name="value">Valor del campo de usuario</param>
        public void SetUdf(string udfName, object value)
        {
            if (udfs.ContainsKey(udfName))
                udfs[udfName] = value;
        }
        /// <summary>
        /// Guardar solo los campos de usuario
        /// </summary>
        public void SaveUdfs()
        {
            if (udfs?.Count == 0) return;
            if (string.IsNullOrWhiteSpace(idName?.ToString())) return;
            Bd.Query(NombreTabla)
                    .Where(idName, idValue)
                    .Update(udfs, ConnName);
        }
        public Dictionary<string, object> GetAllUdf() => udfs;
        new public static T Find(object id)
        {
            var m = new T();
            var cols = new List<string>();
            cols.AddRange(ColumnasDelModelo(m));
            cols.AddRange(m.GetAllUdf().Keys);
            var dt = Bd.Query(m.getNombreTabla()).Select(cols.ToArray())
                .Where(m.getIdName(), id).Get(m.getConnName());
            if (dt.Rows.Count == 0) return null;
            m = DataTableToModeloCollection(dt)[0];
            var r = dt.Rows[0];
            var keys = new string[m.GetAllUdf().Keys.Count];
            m.GetAllUdf().Keys.CopyTo(keys, 0);
            foreach (var item in keys)
            {
                m.SetUdf(item, r[item]);
            }
            return m;
        }
        new public void Save()
        {
            if (!exists())
                throw new Exception($"No se permite insertar el objeto del tipo {typeof(T).FullName} en la base de datos de SAP");
            SaveUdfs();
            base.Save();
        }
    }
}
