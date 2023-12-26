using Mane.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Security
{
    public static class TokenGenerator
    {
        private static TokenConfig _tokenConfig;

        public static TokenConfig TokenConfiguration
        {
            get
            {
                if (_tokenConfig == null)
                    _tokenConfig = TokenConfig.GetActiveConfig();
                return _tokenConfig;
            }
        }
        private static void RevocarTokens()
        {
            if (TokenConfiguration.TokenExpirationTime == 0)
                return;
            var minDate = DateTime.Now.AddMinutes(-TokenConfiguration.TokenExpirationTime);
            Token.Query()
                .Where("Active", true)
                .Where("LastUse", "<=", minDate)
                .Update(new { Active = false }, TokenConfig.TokenConnectionName);
        }
        public static bool ValidateToken(string token,object userId)
        {
            RevocarTokens();
            var t = Token.FindByToken(token);
            if(t == null)
            {
                return false;
            }
            if (!t.Active || t.User_id != userId.ToString())
                return false; 
            t.LastUse = DateTime.Now;
            t.Save();
            return true;
        }

        /// <summary>
        /// Registra un nuevo token de sesión y devuelve el token generado en outMessage
        /// </summary>
        /// <param name="user_id">identificador de usuario</param>
        /// <param name="outMessage">Verdadero cuando se genera correctamente y asigna el token en outMessage o falso si hay error y asigna el mensaje de error en outMessage</param>
        /// <returns></returns>
        public static bool CreateToken(object user_id,out string outMessage)
        {
            RevocarTokens();
            outMessage = string.Empty;
            if(TokenConfiguration.TokenLimitPerUser > 0)
            {
                var tokensActivos = Token.Query()
                     .Where("Active", true)
                     .Where("User_id", user_id)
                     .Count();
                if(tokensActivos > TokenConfiguration.TokenLimitPerUser)
                {
                    outMessage = "Limite de tokens excedido";
                    return false;
                }
            }
            var token = new Token
            {
                CreatedDate = DateTime.Now,
                LastUse = DateTime.Now,
                TokenText = Crypto.GetMD5(user_id + DateTime.Now.ToString("dd MM yyyy mm ss fff")),
                Active = true,
                User_id = user_id.ToString()
            };
            token.Save();
            outMessage = token.TokenText;
            return true;
           
        }
    }
}
