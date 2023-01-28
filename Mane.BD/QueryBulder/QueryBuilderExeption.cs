using System;

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
