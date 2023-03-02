using Mane.Sap.ServiceLayer.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Linq;
using System.Net;

namespace Mane.Sap.ServiceLayer
{
    /// <summary>
    /// Respuesta de Services Layer
    /// </summary>
    public class SLResponse
    {
        /// <summary>
        /// construye un SLResponse a partir de un IRestResponse
        /// </summary>
        /// <param name="response"></param>
        public SLResponse(IRestResponse response)
        {
            Estatus = response.StatusCode;
            Content = response.Content;
        }
        /// <summary>
        /// Estatus del Response
        /// </summary>
        public HttpStatusCode Estatus { get; set; }
        /// <summary>
        /// Contenido del response
        /// </summary>
        public string Content { get; set; }
        
        /// <summary>
        /// Convierte el json del Content a objeto del tipo T
        /// </summary>
        /// <typeparam name="T">tipo de objeto retornado</typeparam>
        /// <returns></returns>
        public T GetObjectResult<T>() where T : class
        {
            if (string.IsNullOrEmpty(Content))
                return null;            
            try
            {
                var jOb = JObject.Parse(Content);
                return jOb.ToObject<T>();
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// Devuelce un objeto JObject que representa el content
        /// </summary>
        /// <returns></returns>
        public JObject GetJObject()
        {
            try
            {
                return JObject.Parse(Content);
            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
