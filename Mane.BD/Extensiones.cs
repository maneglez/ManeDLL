using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
