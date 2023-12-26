using Mane.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Security.Models
{
    public class TokenConfig : Modelo<TokenConfig>
    {
        public static string TokenConnectionName = "Mane.Security.TokenGenerator";
        public static string TokenDbFileName = "db_tokens.db";
        protected override string ConnName => TokenConnectionName;
        protected override string NombreTabla => "TokenConfig";
        /// <summary>
        /// Indica en minutos la vida de un token después de su último uso
        /// </summary>
        public int TokenExpirationTime { get; set; }
        /// <summary>
        /// Limite de tokens por usuario
        /// </summary>
        public int TokenLimitPerUser { get; set; }
        public bool Active { get; set; }

        public static TokenConfig GetActiveConfig()
        {
            if(Bd.Conexiones.Find(TokenConnectionName) == null)
            {
                Bd.Conexiones.Add(new Conexion
                {
                    Nombre = TokenConnectionName,
                    Servidor = TokenDbFileName,
                    TipoDeBaseDeDatos = TipoDeBd.SQLite
                });
            }
            var con = Query().Where("Active", true).First();
            if (con == null)
            {
                con = new TokenConfig
                {
                    Active = true
                };
                con.Save();
            }
            return con;
        }
                
        
    }
}
