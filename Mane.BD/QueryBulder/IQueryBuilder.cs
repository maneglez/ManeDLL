using System;
using System.Collections.Generic;
using System.Data;

namespace Mane.BD
{
    public interface IQueryBuilder
    {
        IQueryBuilder Copy();
        int Count(string connName = "");
        void Delete(string connName = "");
        bool Exists(string connName = "");
        IQueryBuilder From(string tabla);
        IQueryBuilder Join(IQueryBuilder consulta, string alias, string col1, string col2);
        IQueryBuilder Join(IQueryBuilder consulta, string alias, string col1, string col2, Func<IQueryBuilder, IQueryBuilder> otrasCondiciones);
        IQueryBuilder Join(string tabla, string col1, string col2);
        IQueryBuilder Join(string tabla, string col1, string col2, string operador = "=");
        IQueryBuilder Join(string tabla, string col1, string col2, Func<IQueryBuilder, IQueryBuilder> func, string operador = "=");
        IQueryBuilder LeftJoin(string tabla, string col1, string col2, string operador = "=");
        IQueryBuilder LeftJoin(string tabla, string col1, string col2, Func<IQueryBuilder, IQueryBuilder> func, string operador = "=");
        IQueryBuilder Limit(int limit);
        IQueryBuilder OrderBy(string columna, QueryBuilder.OrderDireccion orden = QueryBuilder.OrderDireccion.Asendente);
        IQueryBuilder OrWhere(Func<IQueryBuilder, IQueryBuilder> func);
        IQueryBuilder OrWhere(string col1, string operador, object Valor);
        IQueryBuilder OrWhereBetween(string col1, object valor1, object valor2);
        IQueryBuilder RawQuery(string query);
        IQueryBuilder RightJoin(string tabla, string col1, string col2, string operador = "=");
        IQueryBuilder RightJoin(string tabla, string col1, string col2, Func<IQueryBuilder, IQueryBuilder> func, string operador = "=");
        IQueryBuilder Select(Dictionary<string, IQueryBuilder> SelectSubquery);
        IQueryBuilder Select(params string[] columnas);
        IQueryBuilder Select(IQueryBuilder query, string alias);
        IQueryBuilder SelectRaw(string RawQuery, string alias);
        int Update(Dictionary<string, object> dic, string connName = "");
        int Update(object objeto, string connName = "");
        IQueryBuilder Where(Func<IQueryBuilder, IQueryBuilder> func);
        IQueryBuilder Where(string col, Func<IQueryBuilder, IQueryBuilder> func);
        IQueryBuilder Where(string col1, object valor);
        IQueryBuilder Where(string col, string operador, Func<IQueryBuilder, IQueryBuilder> func);
        IQueryBuilder Where(string col1, string operador, object valor);
        IQueryBuilder WhereBetween(string col1, object valor1, object valor2);
        IQueryBuilder WhereColumn(string col1, string col2);
        IQueryBuilder WhereColumn(string col1, string operador, string col2);
        IQueryBuilder WhereIn(string columna, Func<IQueryBuilder, IQueryBuilder> func);
        IQueryBuilder WhereIn(string columna, int[] valores);
        IQueryBuilder WhereIn(string columna, string[] valores);
        IQueryBuilder WhereNotIn(string columna, Array valores);
        IQueryBuilder WhereNotIn(string columna, Func<IQueryBuilder, IQueryBuilder> func);
    }
}