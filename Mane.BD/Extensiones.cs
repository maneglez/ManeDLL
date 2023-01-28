using Newtonsoft.Json;

namespace Mane.BD
{
    public static class Extensiones
    {
        public static T JsonToObject<T>(this string obj) where T : class
        {
            return JsonConvert.DeserializeObject<T>(obj);
        }
    }
}
