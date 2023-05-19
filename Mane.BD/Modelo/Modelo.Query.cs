using System;
using System.Collections.Generic;

namespace Mane.BD
{
    public class ModeloQuery<Tmodelo> : IQuery<ModeloQuery<Tmodelo>> where Tmodelo : Modelo, new()
    {
        private QueryBuilder query;
        private Tmodelo modelo;

        /// <summary>
        /// 
        /// </summary>
        public ModeloQuery()
        {
            query = new QueryBuilder();
            modelo = new Tmodelo();
            query.From(modelo.getNombreTabla());
        }
        /// <summary>
        /// Obtiene todos los modelos que coinciden con las condiciones
        /// </summary>
        /// <returns>Coleccion de modelos que coinciden con las condiciones</returns>
        public Modelo<Tmodelo>.ModeloCollection Get()
        {
            if (query.GetCurrentColumnsNames().Length == 0)
                query.Select(Modelo.ColumnasDelModelo(modelo));
            var dt = query.Get(modelo.getConnName());
            if (dt.Rows.Count == 0) return new Modelo<Tmodelo>.ModeloCollection();
            return Modelo<Tmodelo>.DataTableToModeloCollection(dt);
        }
        /// <summary>
        /// Obtiene el primer modelo que coincide con las condiciones
        /// </summary>
        /// <returns>Modelo o nulo si no hay resultados</returns>
        public Tmodelo First()
        {
            if (query.GetCurrentColumnsNames().Length == 0)
                query.Select(Modelo.ColumnasDelModelo(modelo));
            var dt = query
                .Limit(1)
                .Get(modelo.getConnName());
            if (dt.Rows.Count == 0) return null;
            return Modelo<Tmodelo>.DataTableToModeloCollection(dt)[0];
        }
        public int Count(string NombreConexion = "")
        {
            return query.Count(modelo.getConnName());
        }

        public void Delete(string NombreConexion = "", bool forzar = false)
        {
            query.Delete(modelo.getConnName());
        }

        public bool Exists(string NombreConexion = "")
        {
            return query.Exists(modelo.getConnName());
        }

        public ModeloQuery<Tmodelo> From(string tabla)
        {
            query.From(tabla);
            return this;
        }

        public ModeloQuery<Tmodelo> GroupBy(params string[] columnas)
        {
            query.GroupBy(columnas);
            return this;
        }

        public object Insert(Dictionary<string, object> col_values, string NombreConexion = "")
        {
            throw new NotImplementedException();
        }

        public object Insert(object col_values, string NombreConexion = "")
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> Join(ModeloQuery<Tmodelo> consulta, string alias, string col1, string col2)
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> Join(ModeloQuery<Tmodelo> consulta, string alias, string col1, string col2, Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> otrasCondiciones)
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> Join(string tabla, string col1, string col2)
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> Join(string tabla, string col1, string col2, string operador = "=")
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> Join(string tabla, string col1, string col2, Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func, string operador = "=")
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> LeftJoin(string tabla, string col1, string col2, string operador = "=")
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> LeftJoin(string tabla, string col1, string col2, Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func, string operador = "=")
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> Limit(int limit)
        {
            query.Limit(limit);
            return this;
        }

        public ModeloQuery<Tmodelo> OrderBy(string columna, OrderDireccion orden = OrderDireccion.Asendente)
        {
            query.OrderBy(columna, orden);
            return this;
        }

        public ModeloQuery<Tmodelo> OrWhere(Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new ModeloQuery<Tmodelo>());
            var qry = aux.query;
            query.OrWhere((q) => qry);
            return this;
        }

        public ModeloQuery<Tmodelo> OrWhere(string col1, string operador, object Valor)
        {
            query.OrWhere(col1, operador, Valor);
            return this;
        }

        public ModeloQuery<Tmodelo> OrWhere(string col, object valor)
        {
            return OrWhere(col, "=", valor);
        }

        public ModeloQuery<Tmodelo> OrWhereBetween(string col1, object valor1, object valor2)
        {
            query.OrWhereBetween(col1, valor1, valor2);
            return this;
        }

        public ModeloQuery<Tmodelo> RawQuery(string query)
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> RightJoin(string tabla, string col1, string col2, string operador = "=")
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> RightJoin(string tabla, string col1, string col2, Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func, string operador = "=")
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> Select(Dictionary<string, ModeloQuery<Tmodelo>> SelectSubquery)
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> Select(params string[] columnas)
        {
            query.Select(columnas);
            return this;
        }

        public ModeloQuery<Tmodelo> Select(ModeloQuery<Tmodelo> query, string alias)
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> SelectRaw(string RawQuery, string alias)
        {
            throw new NotImplementedException();
        }

        public int Update(Dictionary<string, object> dic, string NombreConexion = "", bool forzar = false)
        {
            return query.Update(dic, modelo.getConnName());
        }

        public int Update(object objeto, string NombreConexion = "", bool forzar = false)
        {
            return query.Update(objeto, modelo.getConnName());
        }

        public ModeloQuery<Tmodelo> Where(Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new ModeloQuery<Tmodelo>());
            var qry = aux.query;
            query.Where(q => qry);
            return this;
        }

        public ModeloQuery<Tmodelo> Where(string col, Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new ModeloQuery<Tmodelo>());
            var qry = aux.query;

            query.Where(col, (q) => qry);
            return this;
        }

        public ModeloQuery<Tmodelo> Where(string col1, object valor)
        {
            return Where(col1, "=", valor);
        }

        public ModeloQuery<Tmodelo> Where(string col, string operador, Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func)
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> Where(string col1, string operador, object valor)
        {
            query.Where(col1, operador, valor);
            return this;
        }

        public ModeloQuery<Tmodelo> WhereBetween(string col1, object valor1, object valor2)
        {
            query.WhereBetween(col1, valor1, valor2);
            return this;
        }

        public ModeloQuery<Tmodelo> WhereColumn(string col1, string col2)
        {
            return WhereColumn(col1, "=", col2);
        }

        public ModeloQuery<Tmodelo> WhereColumn(string col1, string operador, string col2)
        {
            query.WhereColumn(col1, operador, col2);
            return this;
        }

        public ModeloQuery<Tmodelo> WhereIn(string columna, Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new ModeloQuery<Tmodelo>());
            var qry = aux.query;
            query.WhereIn(columna, (q) => qry);
            return this;
        }

        public ModeloQuery<Tmodelo> WhereIn(string columna, object[] valores)
        {
            query.WhereIn(columna, valores);
            return this;
        }

        public ModeloQuery<Tmodelo> WhereIsNotNull(string columna)
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> WhereIsNull(string columna)
        {
            throw new NotImplementedException();
        }

        public ModeloQuery<Tmodelo> WhereNotIn(string columna, Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new ModeloQuery<Tmodelo>());
            var qry = aux.query;
            query.WhereNotIn(columna, (q) => qry);
            return this;
        }

        public ModeloQuery<Tmodelo> WhereNotIn(string columna, object[] valores)
        {
            query.WhereNotIn(columna, valores);
            return this;
        }

        public ModeloQuery<Tmodelo> Paginate(int page, int paginate)
        {
            query.Paginate(page, paginate);
            return this;
        }

        public ModeloQuery<Tmodelo> Where(Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func, string operador, object valor)
        {
            var aux = func.Invoke(new ModeloQuery<Tmodelo>());
            var qry = aux.query;
            query.Where(q=>qry, operador, valor);
            return this;
        }

        public ModeloQuery<Tmodelo> AddSelect(string column)
        {
            query.AddSelect(column);
            return this;
        }
    }
}
