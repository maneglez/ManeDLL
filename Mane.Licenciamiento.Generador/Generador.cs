using Mane.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Licenciamiento.Generador
{
    public class Generador
    {
        /// <summary>
        /// Genera una nueva clave de licencia
        /// </summary>
        /// <param name="ClaveHw">Clave de Hardware</param>
        /// <param name="AppID">Identificador de aplicación</param>
        /// <param name="FechaExpiracion">Fecha de expiración</param>
        /// <param name="ClaveEncriptado">Clave de encriptado (16 carateres exactos)</param>
        /// <returns>Clave de licencia</returns>
        public static string GenerarLicencia(string ClaveHw, string AppID, DateTime FechaExpiracion, string ClaveEncriptado)
        {
            /*datos =>{
                 [0] Hardware Key
                 [1] App ID
                 [2] Expiración}
                 */
            string clave = string.Format("{0};{1};{2}", ClaveHw, AppID, FechaExpiracion.ToString("O"));
            return Crypto.Encriptar(clave, ClaveEncriptado);

        }
    }
}
