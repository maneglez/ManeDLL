using System;
using System.Data;

namespace Mane.BD.Executors
{
    public interface IBdExecutor : IDisposable
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
