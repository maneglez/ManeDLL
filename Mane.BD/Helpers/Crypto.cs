using System;
using System.Security.Cryptography;
using System.Text;

namespace Mane.BD.Helpers
{
    /// <summary>
    /// Clase de Encriptado
    /// </summary>
    public class Crypto
    {
        /// <summary>
        /// La clave de encriptado por defecto, solo se puede configurar en Aplicaciontie.ClaveDeEncriptado
        /// </summary>
        public static string ClaveDeEncriptado => "ManeDefaultKey"; //"Tie.030524ei06Te";//
        /// <summary>
        /// Encripta un dato en base a la clave de encriptado especificada en la clase "AplicacionTie"
        /// </summary>
        /// <param name="dato">Cadena a encriptar</param>
        /// <returns>Cadena encriptada con TripeDES</returns>
        public static string Encriptar(string dato)
        {
            return Encriptar(dato, ClaveDeEncriptado);
        }
        /// <summary>
        /// Encripta una cadena utilizando una clave específica
        /// </summary>
        /// <param name="dato">Cadena a encriptar</param>
        /// <param name="clave">Clave de encriptado</param>
        /// <returns></returns>
        public static string Encriptar(string dato, string clave)
        {
            if (string.IsNullOrEmpty(dato) || string.IsNullOrEmpty(clave))
                return dato;
            try
            {
                clave = GetMD5(clave).Substring(0, 16);
                byte[] keyArray = Encoding.UTF8.GetBytes(clave);
                byte[] encriptar = Encoding.UTF8.GetBytes(dato);
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cryptoTransform = tdes.CreateEncryptor();
                byte[] resultado = cryptoTransform.TransformFinalBlock(encriptar, 0, encriptar.Length);
                tdes.Clear();
                return Convert.ToBase64String(resultado);
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// Desencripta una cadena utilizando la clave de encriptado especificada en la clase "AplicacionTie"
        /// </summary>
        /// <param name="dato">Cadena a desencriptar</param>
        /// <returns>Cadena desencriptada con TripleDES</returns>
        public static string Decriptar(string dato)
        {
            return Decriptar(dato, ClaveDeEncriptado);
        }
        /// <summary>
        /// Desencripta una cadena utilizando una clave específica
        /// </summary>
        /// <param name="dato">cadena a desencriptar</param>
        /// <param name="clave">clave de desencriptado</param>
        /// <returns></returns>
        public static string Decriptar(string dato, string clave)
        {
            if (string.IsNullOrEmpty(dato) || string.IsNullOrEmpty(clave))
                return dato;
            try
            {
                clave = GetMD5(clave).Substring(0, 16);
                byte[] keyArray = Encoding.UTF8.GetBytes(clave);
                byte[] decriptar = Convert.FromBase64String(dato);
                TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
                tdes.Key = keyArray;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform cryptoTransform = tdes.CreateDecryptor();
                byte[] resultado = cryptoTransform.TransformFinalBlock(decriptar, 0, decriptar.Length);
                tdes.Clear();
                return Encoding.UTF8.GetString(resultado);
            }
            catch (Exception)
            {
                return "";
            }
        }
        /// <summary>
        /// Obtiene un hash de cualquier cadena de texto
        /// </summary>
        /// <param name="str">Cadena</param>
        /// <returns>Devuelve el hash MD5 de la cadena</returns>
        public static string GetMD5(string str)
        {
            MD5 md5 = MD5.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
