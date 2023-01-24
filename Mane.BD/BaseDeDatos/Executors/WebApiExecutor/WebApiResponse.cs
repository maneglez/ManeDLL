using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
