using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD.BaseDeDatos.Executors.WebApiExecutor
{
    public class WebApiResponse
    {
        public WebApiResponse(object data,HttpStatusCode estatusCode = HttpStatusCode.OK, string message = "")
        {
            EstatusCode = estatusCode;
            Message = message;
            Data = data;
        }
        public WebApiResponse()
        {
            
        }

        public HttpStatusCode EstatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public DataTable DataTable { get; set; }
    }
}
