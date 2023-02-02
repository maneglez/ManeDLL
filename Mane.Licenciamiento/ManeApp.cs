using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Licenciamiento
{
    public static class ManeApp
    {
        public static string ClaveCifrado { get; set; } = "DefaultManeAppKey";
        public static string AppId { get; set; } = "ManeApp";
        public static string AppDesc { get; set; } = "Mane App";
        public static void Configure(string appId,string claveCifrado,string appDesc)
        {
            AppId = appId;
            ClaveCifrado = claveCifrado;
            AppDesc = appDesc;
        }
    }
}
