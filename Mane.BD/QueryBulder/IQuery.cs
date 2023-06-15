using System;
using System.Collections.Generic;

namespace Mane.BD
{
    public interface IQuery<T> where T : IQuery<T>
    {
        T AddSelect(string column);
        int Count(string NombreConexion = "");
        void Delete(string NombreConexion = "", bool forzar = false);
        bool Exists(string NombreConexion = "");
        T From(string tabla);
        T GroupBy(params string[] columnas);
        /// <summary>
        /// Inserta un registro en la tabla seleccionada utilizando la conexión proporcionada
        /// </summary>
        /// <param name="col_values">Nombre y valores de cada columna</param>
        /// <param name="NombreConexion">Nombre de la conexión a utilizar</param>
        /// <returns>El valor de la identidad (ID insertado)</returns>
        object Insert(Dictionary<string, object> col_values, string NombreConexion = "");
        /// <summary>
        /// Inserta un registro en la tabla seleccionada utilizando la conexión proporcionada
        /// </summary>
        /// <param name="col_values">Nombre y valores de cada columna</param>
        /// <param name="NombreConexion">Nombre de la conexión a utilizar</param>
        /// <returns>El valor de la identidad (ID insertado)</returns>
        object Insert(object col_values, string NombreConexion = "");
        T Join(T consulta, string alias, string col1, string col2);
        T Join(T consulta, string alias, string col1, string col2, Func<T, T> otrasCondiciones);
        T Join(string tabla, string col1, string col2);
        T Join(string tabla, string col1, string col2, string operador = "=");
        T Join(string tabla, string col1, string col2, Func<T, T> func, string operador = "=");
        T LeftJoin(string tabla, string col1, string col2, string operador = "=");
        T LeftJoin(string tabla, string col1, string col2, Func<T, T> func, string operador = "=");
        T Limit(int limit);
        T OrderBy(string columna, OrderDireccion orden = OrderDireccion.Asendente);
        T OrWhere(Func<T, T> func);
        T OrWhere(string col, object valor);
        T OrWhere(string col1, string operador, object Valor);
        T OrWhereBetween(string col1, object valor1, object valor2);
        T OrWhereColumn(string col1, string col2,string operador = "=");
        T OrWhereIn(string col, object[] values);
        /// <summary>
        /// Pagina el resultado de la consulta
        /// </summary>
        /// <param name="page">Número de página</param>
        /// <param name="paginate">Limite de registros por página</param>
        /// <returns>T</returns>
        T Paginate(int page, int paginate);
        T RawQuery(string query);
        T RightJoin(string tabla, string col1, string col2, string operador = "=");
        T RightJoin(string tabla, string col1, string col2, Func<T, T> func, string operador = "=");
        T Select(Dictionary<string, T> SelectSubquery);
        T Select(params string[] columnas);
        T Select(T query, string alias);
        T SelectRaw(string RawQuery, string alias);
        int Update(Dictionary<string, object> dic, string NombreConexion = "", bool forzar = false);
        int Update(object objeto, string NombreConexion = "", bool forzar = false);
        T Where(Func<T, T> func);
        T Where(string col, Func<T, T> func);
        T Where(string col1, object valor);
        T Where(string col, string operador, Func<T, T> func);
        T Where(string col1, string operador, object valor);
        T Where(Func<T, T> func, string operador, object valor );
        T WhereBetween(string col1, object valor1, object valor2);
        T WhereColumn(string col1, string col2);
        T WhereColumn(string col1, string operador, string col2);
        T WhereIn(string columna, Func<T, T> func);
        T WhereIn(string columna, object[] valores);
        T WhereIsNotNull(string columna);
        T WhereIsNull(string columna);
        T WhereNotIn(string columna, Func<T, T> func);
        T WhereNotIn(string columna, object[] valores);

    }
}