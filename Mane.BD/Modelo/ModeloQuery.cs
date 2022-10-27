using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD
{
    /// <summary>
    /// 
    /// </summary>
    public class ModeloQuery<Tmodelo> where Tmodelo : Modelo, new()
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
        /// Elimina los modelos que coinciden con la consulta
        /// </summary>
        public void delete() 
        {
           query.Delete(modelo.getConnName());
        }
        /// <summary>
        /// Cuenta total de registros devueltos por la consulta
        /// </summary>
        /// <returns>El numero de registros devueltos por la consulta</returns>
        public int count()
        {
            return query.Count(modelo.getConnName());
        }

        /// <summary>
        /// Ejecuta un update en la tabla actual
        /// </summary>
        /// <param name="objeto">Objeto que representa las columnas y valores ej: new {Nombre = "Nombre",Edad = "23"}</param>
        public void update(object objeto) 
        {
            query.Update(objeto,modelo.getConnName());
        }
        /// <summary>
        /// Indica si quiere limitar la cantidad de registros retornados
        /// </summary>
        /// <param name="limit">Cantidad de registros</param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> limit(int limit)
        {
            query.Limit(limit);
            return this;
        }

        /// <summary>
        /// Condicion de comparacion = entre la columna y valor o columna
        /// </summary>
        /// <param name="col1">Columna</param>
        /// <param name="valor">Valor</param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> where(string col1, object valor)
        {
            return where(col1, "=", valor);
        }
        /// <summary>
        /// Establece la tabla de donde se obtiene la informacion
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <returns>ModeloQuery</returns>
        public ModeloQuery<Tmodelo> from(string tabla)
        {
            query.From(tabla);
            return this;
        }
        /// <summary>
        /// Selecciona columnas
        /// </summary>
        /// <param name="columnas">Columnas</param>
        /// <returns>ModelQuery</returns>
        public ModeloQuery<Tmodelo> select(params string[] columnas)
        {
            query.Select(columnas);
            return this;
        }
        /// <summary>
        /// Condicion where
        /// </summary>
        /// <param name="col1">Columna </param>
        /// <param name="operador">Operador</param>
        /// <param name="valor">Valor</param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> where(string col1, string operador, object valor)
        {
            query.Where(col1, operador, valor);
            return this;
        }
        /// <summary>
        /// Compara si 2 columnas son iguales
        /// </summary>
        /// <param name="col1">columna 1</param>
        /// <param name="col2">columna 2</param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> whereColumn(string col1, string col2)
        {
            return whereColumn(col1, "=", col2);
        }
        /// <summary>
        /// Compara 2 columnas
        /// </summary>
        /// <param name="col1">columna 1</param>
        /// <param name="operador">operador de comparacion</param>
        /// <param name="col2">columna 2</param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> whereColumn(string col1, string operador, string col2)
        {
            query.WhereColumn(col1,operador,col2);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="col1"></param>
        /// <param name="valor1"></param>
        /// <param name="valor2"></param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> whereBetween(string col1, string valor1, string valor2)
        {
            query.WhereBetween(col1, valor1, valor2);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="col1"></param>
        /// <param name="valor1"></param>
        /// <param name="valor2"></param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> orWhereBetween(string col1, string valor1, string valor2)
        {
            query.OrWhereBetween(col1, valor1, valor2);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> where(string col, Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new ModeloQuery<Tmodelo>());
            var qry = aux.query;

            query.Where(col,(q) => qry);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> where(Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new ModeloQuery<Tmodelo>());
            var qry = aux.query;
            query.Where(q => qry);
            return this;
        }
        /// <summary>
        /// Condicion where con el operador or
        /// </summary>
        /// <param name="col1">Columna </param>
        /// <param name="operador">Operador</param>
        /// <param name="Valor">Valor </param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> orWhere(string col1, string operador, object Valor)
        {
            query.OrWhere(col1,operador,Valor);
            return this;
        }
        /// <summary>
        /// Condicion where con el operador or
        /// </summary>
        /// <param name="col1">Columna </param>
        /// <param name="operador">Operador</param>
        /// <param name="Valor">Valor </param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> orWhere(string col1,object Valor)
        {
            query.OrWhere(col1, "=", Valor);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> orWhere(Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new ModeloQuery<Tmodelo>());
            var qry = aux.query;
            query.OrWhere((q) => qry);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="valores"></param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> whereIn(string columna, string[] valores)
        {
            query.WhereIn(columna,valores);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="valores"></param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> whereNotIn(string columna, object[] valores)
        {
            query.WhereNotIn(columna,valores);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> whereIn(string columna, Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new ModeloQuery<Tmodelo>());
            var qry = aux.query;
            query.WhereIn(columna,(q) => qry);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> whereNotIn(string columna, Func<ModeloQuery<Tmodelo>, ModeloQuery<Tmodelo>> func)
        {
            var aux = func.Invoke(new ModeloQuery<Tmodelo>());
            var qry = aux.query;
            query.WhereNotIn(columna, (q) => qry);
            return this;
        }
        /// <summary>
        /// Orden
        /// </summary>
        /// <param name="columna">columna a ordenar</param>
        /// <param name="orden">orden (asc | desc) por defecto asc</param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> orderBy(string columna, OrderDireccion orden = OrderDireccion.Asendente)
        {
            
            query.OrderBy(columna, orden);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnas"></param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> groupBy(params string[] columnas)
        {
            query.GroupBy(columnas);
            return this;
        }
        /// <summary>
        /// Obtiene todos los modelos que coinciden con las condiciones
        /// </summary>
        /// <returns>Coleccion de modelos que coinciden con las condiciones</returns>
        public Modelo<Tmodelo>.ModeloCollection get()
        {
            if (query.GetCurrentColumnsNames().Length == 0)
                query.Select(Modelo.ColumnasDelModelo(modelo));
            var dt = query.Get(modelo.getConnName());
            if(dt.Rows.Count == 0) return new Modelo<Tmodelo>.ModeloCollection();
            return Modelo<Tmodelo>.DataTableToModeloCollection(dt);
        }
      
        /// <summary>
        /// Obtiene el primer modelo que coincide con las condiciones
        /// </summary>
        /// <returns>Modelo o nulo si no hay resultados</returns>
        public Tmodelo first() 
        {
            if (query.GetCurrentColumnsNames().Length == 0)
                query.Select(Modelo.ColumnasDelModelo(modelo));
            var dt = query
                .Limit(1)
                .Get(modelo.getConnName());
            if(dt.Rows.Count == 0) return null;
            return Modelo<Tmodelo>.DataTableToModeloCollection(dt)[0];
        }
        /// <summary>
        /// Determina si existe un modelo
        /// </summary>
        /// <returns>verdadero si la consulta retorna valores</returns>
        public bool exists()
        {
            return query.Exists(modelo.getConnName());
        }
       

    }
}
