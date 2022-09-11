using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD
{
    /// <summary>
    /// Excepcion de la clase Bd
    /// </summary>
    public class BdException : Exception
    {
        public string QueryCulpable { get; set; }
        /// <summary>
        /// Excepcion de la clase Bd
        /// </summary>
        /// <param name="message">Mensaje de error</param>
        public BdException(string message, string queryCulpable  = "") : base(message)
        {
            QueryCulpable = queryCulpable;
        }
    }
}
