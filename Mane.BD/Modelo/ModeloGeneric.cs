﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD
{
    public partial class Modelo<T> : Modelo where T : Modelo,new()
    {
        /// <summary>
        /// Obtiene todos los modelos del tipo T (Maximo 1000)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ModeloCollection All(int limit = 1000)
            => Query().limit(limit).get();

        /// <summary>
        /// Obtiene el pirmer modelo que encuentre en la tabla
        /// </summary>
        /// <typeparam name="T">Tipo de modelo</typeparam>
        /// <returns>Primer modelo de la tabla, o nulo si no hay registros</returns>
        public static T First() 
        {
            var m = new T();
            var dt = Bd.Query(m.getNombreTabla())
                .select(ColumnasDelModelo(m))
                .limit(1).get(m.getConnName());
            if (dt.Rows.Count == 0) return null;
            return DataTableToModeloCollection(dt)[0];
        }

        /// <summary>
        /// Encuentra un registro
        /// </summary>
        /// <typeparam name="T">Tipo de modelo que se desea encontrar</typeparam>
        /// <param name="id">Valor de la clave primaria que desea encontrar</param>
        /// <returns>Una nueva instancia del modelo que coincidio con el ID proporcionado, o nulo si no hubo coincidencias</returns>
        public static T Find(object id) 
        {
            T m = new T();
            string[] columnas = ColumnasDelModelo(m);
            DataTable dt = Bd.Query(m.getNombreTabla()).select(columnas)
                .where(m.getIdName(), id.ToString()).limit(1).get(m.getConnName());
            if (dt.Rows.Count == 0)
                return null;
            var mdls = DataTableToModeloCollection(dt);
            if (mdls.Count > 0)
                return mdls[0];

            return null;
        }
        /// <summary>
        /// Vuelve a consultar los datos del modelo desde la base de datos
        /// </summary>
        public void Refresh()
        {
            if (string.IsNullOrEmpty(idValue?.ToString())) return;
            Inicializando = true;
            var tipo = GetType();
            var m = Find(idValue);
            var dic = m.getOriginalModel();
            foreach (var item in dic.Keys)
            {
                var info = tipo.GetProperty(item);
                if (info != null)
                    if (info.CanWrite)
                        info.SetValue(this, dic[item]);
            }
            Inicializando = false;
        }

        /// <summary>
        /// Genera una nueva instancia de consulta de modelos
        /// </summary>
        /// <typeparam name="T">Tipo de modelo</typeparam>
        /// <returns>Modelo query</returns>
        public static ModeloQuery<T> Query()
        {
            return new ModeloQuery<T>();
        }
        /// <summary>
        /// Convierte un data table en una coleccion de modelos
        /// </summary>
        /// <typeparam name="T">Tipo de modelo</typeparam>
        /// <param name="dt">Tabla</param>
        /// <returns>Coleccion de modelos</returns>
        public static ModeloCollection DataTableToModeloCollection(DataTable dt) 
        {
            ModeloCollection mc = new ModeloCollection();
            if (dt.Rows.Count == 0) return mc;
            T m = new T();
            try
            {
                
                Dictionary<string, object> dicAux = Common.ObjectToKeyValue(m);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                
                foreach (DataRow r in dt.Rows)
                {
                    dic.Clear();
                    foreach (string key in dicAux.Keys)
                    {
                        dic.Add(key, r[key]);
                    }
                    T mAux = DicToModel(dic);
                    mAux.setId(r[mAux.getIdName()]);
                    mAux.setOriginalModel(dic);
                    mAux.setExists(true);
                    mAux.setOriginalIdValue(r[mAux.getIdName()]);
                    mc.Add(mAux);
                }
            }
            catch (Exception e)
            {
                m.ModeloExceptionHandler(e);
            }
            return mc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        private static T DicToModel(Dictionary<string, object> dic)
        {
            T modelo = new T();
            modelo.Inicializando = true;
            var props = modelo.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (prop.CanWrite && !prop.IsDefined(typeof(IgnorarPropAttribute), false))
                    prop.SetValue(modelo, Common.ConvertirATipo(prop.PropertyType, dic[prop.Name]));
            }
            modelo.Inicializando = false;
            return modelo;
        }


    }
}
