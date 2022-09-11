using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD
{
    /// <summary>
    /// Exepciones del querybuilder
    /// </summary>
    public class QueryBuilderExeption : Exception
    {
        /// <summary>
        /// Exepciones del querybuilder
        /// </summary>
        public QueryBuilderExeption(string mensaje) : base("QueryBuilderExeption: " + mensaje)
        {

        }
    }
}
