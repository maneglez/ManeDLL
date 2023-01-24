using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD
{
    /// <summary>
    /// Dirección de ordenamiento del ORDER BY
    /// </summary>
    public enum OrderDireccion
    {
        /// <summary>
        /// Orden Asendente
        /// </summary>
        Asendente,
        /// <summary>
        /// Orden Descendente
        /// </summary>
        Descendente
    }
    /// <summary>
    /// Tipos de bases de datos
    /// </summary>
    public enum TipoDeBd
    {
        /// <summary>
        /// Servidor SQL
        /// </summary>
        [Description("SQL Server")]
        SqlServer,
        /// <summary>
        /// Base de datos SQLite
        /// </summary>
        [Description("SQLite")]
        SQLite,
        /// <summary>
        /// Base de datos de HanaDB
        /// </summary>
        [Description("Hana DB")]
        Hana,
        /// <summary>
        /// Consultas via web
        /// </summary>
        [Description("Api Web")]
        ApiWeb

    }
}
