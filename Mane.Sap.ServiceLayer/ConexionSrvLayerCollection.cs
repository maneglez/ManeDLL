using System.Collections.Generic;

namespace Mane.Sap.ServiceLayer
{
    /// <summary>
    /// Colección de conexiones
    /// </summary>
    public class ConexionSrvLayerCollection : List<ConexionSrvLayer>
    {
        /// <summary>
        /// Agrega una conexión
        /// </summary>
        /// <param name="conn"></param>
        /// <exception cref="System.Exception"></exception>
        new public void Add(ConexionSrvLayer conn)
        {
            if (Find(conn.Nombre) != null)
                throw new System.Exception($"La Conexión {conn.Nombre} ya existe");
            base.Add(conn);
        }
        /// <summary>
        /// Encuentra una conexion en base a su nombre
        /// </summary>
        /// <param name="nombreConexion">Nombre identificador de la conexion</param>
        /// <returns></returns>
       public ConexionSrvLayer Find(string nombreConexion)
        {
            return Find(c => c.Nombre == nombreConexion);
        }
    }
}