using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mane.Sap.ServiceLayer.Interfaces
{
    /// <summary>
    /// Indica que el objeto contiene campos de usuario
    /// </summary>
    public interface ICamposDeUsuario
    {
        Dictionary<string, object> CamposDeUsuario();
        void SetCampoDeUsuario(string name, object value);
        object GetCampoDeUsuario(string name);

    }

}
