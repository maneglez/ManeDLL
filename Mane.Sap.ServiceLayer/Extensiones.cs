using Newtonsoft.Json;
using System;

namespace Mane.Sap.ServiceLayer
{
	/// <summary>
	/// Extensiones para las clases
	/// </summary>
	public static class Extensiones
    {
		/// <summary>
		/// Convierte un objeto en JSON string
		/// </summary>
		/// <typeparam name="T">Tipo de objeto</typeparam>
		/// <param name="obj">objeto</param>
		/// <returns>cadena json que representa el objeto</returns>
        public static string ToJsonString<T>(this T obj) where T : class, new()
        {
			try
			{
				return JsonConvert.SerializeObject(obj);
			}
			catch (Exception)
			{

			}
			return "";
        }
		/// <summary>
		/// Convierte una cadena json a un objeto
		/// </summary>
		/// <typeparam name="T">Tipo de objeto</typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
        public static T JsonToObject<T>(this string obj) where T : class
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}
