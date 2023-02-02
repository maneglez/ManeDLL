using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mane.BD.WebApi
{
    public class PermisosConexionModel : Modelo<PermisosConexionModel>
    {
        protected override string ConnName => NombreConexion.Local;
        protected override string NombreTabla => base.NombreTabla;
    }
}