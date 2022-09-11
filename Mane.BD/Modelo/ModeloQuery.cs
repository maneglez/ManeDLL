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
            query.from(modelo.getNombreTabla());
        }
        
        /// <summary>
        /// Elimina los modelos que coinciden con la consulta
        /// </summary>
        public void delete() 
        {
           query.delete(modelo.getConnName());
        }
        /// <summary>
        /// Cuenta total de registros devueltos por la consulta
        /// </summary>
        /// <returns>El numero de registros devueltos por la consulta</returns>
        public int count()
        {
            return query.count(modelo.getConnName());
        }

        /// <summary>
        /// Ejecuta un update en la tabla actual
        /// </summary>
        /// <param name="objeto">Objeto que representa las columnas y valores ej: new {Nombre = "Nombre",Edad = "23"}</param>
        public void update(object objeto) 
        {
            query.update(objeto,modelo.getConnName());
        }
        /// <summary>
        /// Indica si quiere limitar la cantidad de registros retornados
        /// </summary>
        /// <param name="limit">Cantidad de registros</param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> limit(int limit)
        {
            query.limit(limit);
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
            query.from(tabla);
            return this;
        }
        /// <summary>
        /// Selecciona columnas
        /// </summary>
        /// <param name="columnas">Columnas</param>
        /// <returns>ModelQuery</returns>
        public ModeloQuery<Tmodelo> select(params string[] columnas)
        {
            query.select(columnas);
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
            query.where(col1, operador, valor);
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
            query.whereColumn(col1,operador,col2);
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
            query.whereBetween(col1, valor1, valor2);
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
            query.orWhereBetween(col1, valor1, valor2);
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

            query.where(col,(q) => qry);
            return this;
        }
        /// <summary>
        /// Condicion where con el operador or
        /// </summary>
        /// <param name="col1">Columna </param>
        /// <param name="operador">Operador</param>
        /// <param name="Valor">Valor </param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> orWhere(string col1, string operador, string Valor)
        {
            query.orWhere(col1,operador,Valor);
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
            query.orWhere((q) => qry);
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
            query.whereIn(columna,valores);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columna"></param>
        /// <param name="valores"></param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> whereNotIn(string columna, Array valores)
        {
            query.whereNotIn(columna,valores);
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
            query.whereIn(columna,(q) => qry);
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
            query.whereNotIn(columna, (q) => qry);
            return this;
        }
        /// <summary>
        /// Orden
        /// </summary>
        /// <param name="columna">columna a ordenar</param>
        /// <param name="orden">orden (asc | desc) por defecto asc</param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> orderBy(string columna, QueryBuilder.OrderDireccion orden = QueryBuilder.OrderDireccion.Asendente)
        {
            
            query.orderBy(columna, orden);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnas"></param>
        /// <returns></returns>
        public ModeloQuery<Tmodelo> groupBy(params string[] columnas)
        {
            query.groupBy(columnas);
            return this;
        }
        /// <summary>
        /// Obtiene todos los modelos que coinciden con las condiciones
        /// </summary>
        /// <returns>Coleccion de modelos que coinciden con las condiciones</returns>
        public Modelo.ModeloCollection<Tmodelo> get()
        {
            var dt = query.select(Modelo.ColumnasDelModelo(modelo)).get(modelo.getConnName());
            if(dt.Rows.Count == 0) return new Modelo.ModeloCollection<Tmodelo>();
            return Modelo.DataTableToModeloCollection<Tmodelo>(dt);
        }
        /// <summary>
        /// Obtiene el primer modelo que coincide con las condiciones
        /// </summary>
        /// <returns>Modelo o nulo si no hay resultados</returns>
        public Tmodelo first() 
        {
            var dt = query.select(Modelo.ColumnasDelModelo(modelo))
                .limit(1)
                .get(modelo.getConnName());
            if(dt.Rows.Count == 0) return null;
            
            return Modelo.DataTableToModeloCollection<Tmodelo>(dt)[0];
        }
        /// <summary>
        /// Determina si existe un modelo
        /// </summary>
        /// <returns>verdadero si la consulta retorna valores</returns>
        public bool exists()
        {
            return query.exists(modelo.getConnName());
        }

    }
}
