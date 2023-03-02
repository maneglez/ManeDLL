using Mane.Sap.ServiceLayer.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Sap.ServiceLayer.Clases
{
    public class ObjetoSapBase : ICamposDeUsuario
    {
        protected virtual BoObjectTypes ObjectType { get; }
        protected Dictionary<string, object> CamposDeUsuario;

        public SLResponse Add(string nombreConexion = "")
        {
            return SapSrvLayer.POST(Helper.GetObjectName(ObjectType), ToJson(), nombreConexion);
        }
        public string ToJson()
        {
            if (CamposDeUsuario != null)
            {
                var obj = JObject.FromObject(this);
                foreach (var k in CamposDeUsuario.Keys)
                {
                    obj.Add(new JProperty(k, CamposDeUsuario[k]));
                }
                return obj.ToString();
            }
            else
            {
                return this.ToJsonString();
            }
        }
        
        internal BoObjectTypes GetObjectType() => ObjectType;

        Dictionary<string, object> ICamposDeUsuario.CamposDeUsuario()
        => CamposDeUsuario;

        public void SetCampoDeUsuario(string name, object value)
        {
            if (CamposDeUsuario == null)
            {
                CamposDeUsuario = new Dictionary<string, object>
                {
                    {name,value }
                };
            }
            else if (CamposDeUsuario.ContainsKey(name))
                CamposDeUsuario[name] = value;
            else CamposDeUsuario.Add(name, value);
        }

        public object GetCampoDeUsuario(string name)
        {
            if (CamposDeUsuario == null) return null;
            if (!CamposDeUsuario.ContainsKey(name)) throw new Exception($"El campo de usuario {name} no existe");
            return CamposDeUsuario[name];
        }
    }

    public class ObjetoSapBase<T> : ObjetoSapBase where T : ObjetoSapBase, new()
    {
        /// <summary>
        /// Obtiene la instancia de un objeto en base a su clave primaria
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombreConexion"></param>
        /// <returns></returns>
        public static T Find(object id,string nombreConexion = "")
        {
            var ob = new T();
            var query = Helper.GetObjectQuery(ob.GetObjectType(),id);
            var result = SapSrvLayer.GET(query, nombreConexion);
            T objResult;
            if (result.Estatus == System.Net.HttpStatusCode.OK)
            {
                objResult = result.GetObjectResult<T>();
            }
                
            return null;
        }
        public static T Parse(string jsonOfObject)
        {
            var jObj = JObject.Parse(jsonOfObject);
            T objResult = jObj.ToObject<T>();
            if (objResult != null)
                SetCamposDeUsuarioAlObjeto(objResult, jObj);
            return objResult;
        }
        private static void SetCamposDeUsuarioAlObjeto(ICamposDeUsuario obj, JObject jObj)
        {
            foreach (var p in jObj.Properties())
            {
                
                if (p.Name.StartsWith("U_"))
                    obj.SetCampoDeUsuario(p.Name, p.Value<object>());
            }
            var props = obj.GetType().GetProperties();
            foreach (var p in props)
            {
                if (p.PropertyType.GetInterfaces().Contains(typeof(ICamposDeUsuario)))
                {
                    SetCamposDeUsuarioAlObjeto(p.GetValue(obj) as ICamposDeUsuario, JObject.Parse(jObj[p.Name].ToString()));
                }else if (p.PropertyType.IsArray)
                {
                    if(p.GetValue(obj) is Array arr)
                    {
                        if(arr.Length > 0)
                        {
                            var item = arr.GetValue(0);
                            if(item != null)
                            if (item.GetType().GetInterfaces().Contains(typeof(ICamposDeUsuario)))
                            {
                                    //SetCamposDeUsuarioAlObjeto(item as ICamposDeUsuario,)
                            }
                        }
                    }
                }

            }
        }
     
    }
}
