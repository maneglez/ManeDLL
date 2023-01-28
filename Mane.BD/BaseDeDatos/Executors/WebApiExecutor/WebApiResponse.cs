using Newtonsoft.Json.Linq;

namespace Mane.BD.BaseDeDatos.Executors.WebApiExecutor
{
    internal class WebApiResponse
    {
        public string message { get; set; }
        public string data { get; set; }
        public JToken GetDataValue(string propName)
        {
            return JObject.Parse(data)[propName];
        }
        public static WebApiResponse Parse(string json)
        {
            var obj = JObject.Parse(json);
            return new WebApiResponse()
            {
                message = obj["message"].ToString(),
                data = obj["data"].ToString()
            };
        }
    }
}
