using System;
using System.Collections.Generic;

namespace Mane.BD
{
    public class WebModelQuery<Tmodelo> : IQuery<WebModelQuery<Tmodelo>> where Tmodelo : WebModel, new()
    {
        private QueryBuilder query;
        private Tmodelo modelo;

        /// <summary>
        /// 
        /// </summary>
        public WebModelQuery()
        {
            query = new QueryBuilder();
            modelo = new Tmodelo();
            query.From(modelo.getNombreTabla());
        }
        /// <summary>
        /// Obtiene todos los modelos que coinciden con las condiciones
        /// </summary>
        /// <returns>Coleccion de modelos que coinciden con las condiciones</returns>
        public WebModel<Tmodelo>.WebModelCollection Get()
        {
            if (query.GetCurrentColumnsNames().Length == 0)
                query.Select(WebModel.ColumnasDelModelo(modelo));
            var dt = query.Get(modelo.getConnName());
            if (dt.Rows.Count == 0) return new WebModel<Tmodelo>.WebModelCollection();
            return WebModel<Tmodelo>.DataTableToModeloCollection(dt);
        }
        /// <summary>
        /// Obtiene el primer modelo que coincide con las condiciones. Retorna nulo si no hay resultados.
        /// </summary>
        /// <returns>WebModel o nulo si no hay resultados</returns>
        public Tmodelo First()
        {
            if (query.GetCurrentColumnsNames().Length == 0)
                query.Select(WebModel.ColumnasDelModelo(modelo));
            var dt = query
                .Limit(1)
                .Get(modelo.getConnName());
            if (dt.Rows.Count == 0) return null;
            return WebModel<Tmodelo>.DataTableToModeloCollection(dt)[0];
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

        public WebModelQuery<Tmodelo> From(string tabla)
        {
            query.From(tabla);
            return this;
        }

        public WebModelQuery<Tmodelo> GroupBy(params string[] columnas)
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

        public WebModelQuery<Tmodelo> Join(WebModelQuery<Tmodelo> consulta, string alias, string col1, string col2)
        {
            throw new NotImplementedException();
        }

        public WebModelQuery<Tmodelo> Join(WebModelQuery<Tmodelo> consulta, string alias, string col1, string col2, Func<WebModelQuery<Tmodelo>, WebModelQuery<Tmodelo>> otrasCondiciones)
        {
            throw new NotImplementedException();
        }

        public WebModelQuery<Tmodelo> Join(string tabla, string col1, string col2)
        {
            query.Join(tabla, col1, col2);
            return this;
        }

        public WebModelQuery<Tmodelo> Join(string tabla, string col1, string col2, string operador = "=")
        {
            query.Join(tabla, col1, col2, operador);
            return this;
        }

        public WebModelQuery<Tmodelo> Join(string tabla, string col1, string col2, Func<WebModelQuery<Tmodelo>, WebModelQuery<Tmodelo>> func, string operador = "=")
        {
            throw new NotImplementedException();
        }

        public WebModelQuery<Tmodelo> LeftJoin(string tabla, string col1, string col2, string operador = "=")
        {
            throw new NotImplementedException();
        }

        public WebModelQuery<Tmodelo> LeftJoin(string tabla, string col1, string col2, Func<WebModelQuery<Tmodelo>, WebModelQuery<Tmodelo>> func, string operador = "=")
        {
            throw new NotImplementedException();
        }

        public WebModelQuery<Tmodelo> Limit(int limit)
        {
            query.Limit(limit);
            return this;
        }

        public WebModelQuery<Tmodelo> OrderBy(string columna, OrderDireccion orden = OrderDireccion.Asendente)
        {
            query.OrderBy(columna, orden);
            return this;
        }

        public WebModelQuery<Tmodelo> OrWhere(Func<WebModelQuery<Tmodelo>, WebModelQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new WebModelQuery<Tmodelo>());
            var qry = aux.query;
            query.OrWhere((q) => qry);
            return this;
        }

        public WebModelQuery<Tmodelo> OrWhere(string col1, string operador, object Valor)
        {
            query.OrWhere(col1, operador, Valor);
            return this;
        }

        public WebModelQuery<Tmodelo> OrWhere(string col, object valor)
        {
            return OrWhere(col, "=", valor);
        }

        public WebModelQuery<Tmodelo> OrWhereBetween(string col1, object valor1, object valor2)
        {
            query.OrWhereBetween(col1, valor1, valor2);
            return this;
        }

        public WebModelQuery<Tmodelo> RawQuery(string query)
        {
            throw new NotImplementedException();
        }

        public WebModelQuery<Tmodelo> RightJoin(string tabla, string col1, string col2, string operador = "=")
        {
            throw new NotImplementedException();
        }

        public WebModelQuery<Tmodelo> RightJoin(string tabla, string col1, string col2, Func<WebModelQuery<Tmodelo>, WebModelQuery<Tmodelo>> func, string operador = "=")
        {
            throw new NotImplementedException();
        }

        public WebModelQuery<Tmodelo> Select(Dictionary<string, WebModelQuery<Tmodelo>> SelectSubquery)
        {
            throw new NotImplementedException();
        }

        public WebModelQuery<Tmodelo> Select(params string[] columnas)
        {
            query.Select(columnas);
            return this;
        }

        public WebModelQuery<Tmodelo> Select(WebModelQuery<Tmodelo> query, string alias)
        {
            throw new NotImplementedException();
        }

        public WebModelQuery<Tmodelo> SelectRaw(string RawQuery, string alias)
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

        public WebModelQuery<Tmodelo> Where(Func<WebModelQuery<Tmodelo>, WebModelQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new WebModelQuery<Tmodelo>());
            var qry = aux.query;
            query.Where(q => qry);
            return this;
        }

        public WebModelQuery<Tmodelo> Where(string col, Func<WebModelQuery<Tmodelo>, WebModelQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new WebModelQuery<Tmodelo>());
            var qry = aux.query;

            query.Where(col, (q) => qry);
            return this;
        }

        public WebModelQuery<Tmodelo> Where(string col1, object valor)
        {
            return Where(col1, "=", valor);
        }

        public WebModelQuery<Tmodelo> Where(string col, string operador, Func<WebModelQuery<Tmodelo>, WebModelQuery<Tmodelo>> func)
        {
            throw new NotImplementedException();
        }

        public WebModelQuery<Tmodelo> Where(string col1, string operador, object valor)
        {
            query.Where(col1, operador, valor);
            return this;
        }

        public WebModelQuery<Tmodelo> WhereBetween(string col1, object valor1, object valor2)
        {
            query.WhereBetween(col1, valor1, valor2);
            return this;
        }

        public WebModelQuery<Tmodelo> WhereColumn(string col1, string col2)
        {
            return WhereColumn(col1, "=", col2);
        }

        public WebModelQuery<Tmodelo> WhereColumn(string col1, string operador, string col2)
        {
            query.WhereColumn(col1, operador, col2);
            return this;
        }

        public WebModelQuery<Tmodelo> WhereIn(string columna, Func<WebModelQuery<Tmodelo>, WebModelQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new WebModelQuery<Tmodelo>());
            var qry = aux.query;
            query.WhereIn(columna, (q) => qry);
            return this;
        }

        public WebModelQuery<Tmodelo> WhereIn(string columna, object[] valores)
        {
            query.WhereIn(columna, valores);
            return this;
        }

        public WebModelQuery<Tmodelo> WhereIsNotNull(string columna)
        {
            throw new NotImplementedException();
        }

        public WebModelQuery<Tmodelo> WhereIsNull(string columna)
        {
            throw new NotImplementedException();
        }

        public WebModelQuery<Tmodelo> WhereNotIn(string columna, Func<WebModelQuery<Tmodelo>, WebModelQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new WebModelQuery<Tmodelo>());
            var qry = aux.query;
            query.WhereNotIn(columna, (q) => qry);
            return this;
        }

        public WebModelQuery<Tmodelo> WhereNotIn(string columna, object[] valores)
        {
            query.WhereNotIn(columna, valores);
            return this;
        }

        public WebModelQuery<Tmodelo> Paginate(int page, int paginate)
        {
            query.Paginate(page, paginate);
            return this;
        }

        public WebModelQuery<Tmodelo> Where(Func<WebModelQuery<Tmodelo>, WebModelQuery<Tmodelo>> func, string operador, object valor)
        {
            var aux = func.Invoke(new WebModelQuery<Tmodelo>());
            var qry = aux.query;
            query.Where(q => qry, operador, valor);
            return this;
        }

        public WebModelQuery<Tmodelo> AddSelect(string column)
        {
            query.AddSelect(column);
            return this;
        }

        public WebModelQuery<Tmodelo> OrWhereColumn(string col1, string col2, string operador = "=")
        {
            query.OrWhereColumn(col1, col2, operador);
            return this;
        }

        public WebModelQuery<Tmodelo> OrWhereIn(string col, object[] values)
        {
            query.OrWhereIn(col, values);
            return this;
        }

        public WebModelQuery<Tmodelo> SelectDistinct(params string[] columnas)
        {
            query.SelectDistinct(columnas);
            return this;
        }

        public WebModelQuery<Tmodelo> OrderBy(params string[] columnas)
        {
            query.OrderBy(columnas);
            return this;
        }

        public WebModelQuery<Tmodelo> OrderByDesc(params string[] columnas)
        {
            query.OrderByDesc(columnas);
            return this;
        }
    }
}
