using Mane.BD;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CommonModules
{
    public static class Configuration
    {
        internal static string ConnectionName => "Mane.CommonModules.ConnectionName";
        /// <summary>
        /// Inicializa o establece la conexion para todos los módulos
        /// </summary>
        /// <param name="conection"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetConnection(Conection conection)
        {
            if (conection == null)
                throw new ArgumentNullException(nameof(conection));
            var bdCon = Bd.Conexiones.Find(ConnectionName);
            if (bdCon != null)
            {
                Bd.Conexiones.Remove(bdCon);
            }
            Bd.Conexiones.Add(conection.ToDbConnection());
            var sapCon = Sap.Sap.Conexiones.Find(ConnectionName);
            if(sapCon != null)
            {
                Sap.Sap.Conexiones.Remove(sapCon);
            }
            Sap.Sap.Conexiones.Add(conection.ToSapConnection());
        }
    }
}
