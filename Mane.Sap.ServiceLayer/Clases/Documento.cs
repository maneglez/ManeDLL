using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mane.Sap.ServiceLayer.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mane.Sap.ServiceLayer.Clases
{
   public class Documento
    {
        [JsonProperty(PropertyName = CamposDeUsuario.Docs_UUID)]
        public string UUID { get; set; }
    }

    public static class CamposDeUsuario
    {
        public const string Docs_UUID = "U_UUID";
    }



}
