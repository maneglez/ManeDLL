using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD.Executors
{
   public interface IBdExecutor
    {
        /// <summary>
        /// Consulta a ejecutar
        /// </summary>
        string Query { get; set; }
        /// <summary>
        /// Cadena de conexion
        /// </summary>
        string ConnString { get; set; }
        bool AutoDisconnect { get; set; }
        DataTable ExecuteQuery();
        int ExecuteNonQuery();
        object ExecuteEscalar();
        bool TestConnection();
        void Connect();
        void Disconnect();
    }
}
