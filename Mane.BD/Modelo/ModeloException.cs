using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.BD
{
    public class ModeloException : Exception
    {
        private Type modelo;
        private string error;
        /// <summary>
        /// Mensaje
        /// </summary>
        public override string Message => $"Exepcion en el modelo \"{modelo.Name}\", lanzó el siguiente error \"{error}\"";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelo"></param>
        /// <param name="error"></param>
        public ModeloException(Type modelo, string error) : base()
        {
            this.modelo = modelo;
            this.error = error;
        }
    }
}
