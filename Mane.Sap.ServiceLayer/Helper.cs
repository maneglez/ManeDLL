using Mane.Sap.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Sap.ServiceLayer
{
    public static class Helper
    {
        /// <summary>
        /// Convierte el nombre del tipo de objeto a consulta Service Layer
        /// </summary>
        /// <param name="tipoObjeto">tipo de objeto</param>
        /// <returns>si le pasas BoObjectTypes.oOrders devuelve "Orders"</returns>
        public static string GetObjectName(BoObjectTypes tipoObjeto)
        {
            
            switch (tipoObjeto) //ha algunos casos que el tipo no coincide con la convicción en ese caso hay
           //que utilizar el switch e ir agregando los que no cumplen, (Normalmente son los tipos de objeto que estan en singular
            {
                case BoObjectTypes.oInventoryGenEntry:
                    return "InventoryGenEntries";
                default:
                    break;
            }
            return tipoObjeto.ToString().Substring(1);
        }
        /// <summary>
        /// Genera la consulta en base al tipo de objeto y id específicos
        /// </summary>
        /// <param name="tipo">tipo de objeto</param>
        /// <param name="id">id</param>
        /// <returns>tipo de objeto + (id) por ejemplo: Orders(134) o BusinessPartners('C-3423')</returns>
        public static string GetObjectQuery(BoObjectTypes tipo,object id)
        {
            string query = GetObjectName(tipo);
            if (id is string)
                query += $"('{id}')";
            else query += $"({id})";
            return query;
        }
    }
}
