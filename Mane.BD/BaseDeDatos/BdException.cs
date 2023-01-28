using System;

namespace Mane.BD
{
    /// <summary>
    /// Excepcion de la clase Bd
    /// </summary>
    public class BdException : Exception
    {
        public string QueryCulpable { get; set; }
        public bool IsConnectionError { get; set; }
        /// <summary>
        /// Excepcion de la clase Bd
        /// </summary>
        /// <param name="message">Mensaje de error</param>
        public BdException(string message, string queryCulpable = "") : base(message)
        {
            QueryCulpable = queryCulpable;
        }
        public override string ToString()
        {
            return $"Error en la consulta: {QueryCulpable} \n{base.ToString()}";
        }
    }
}
