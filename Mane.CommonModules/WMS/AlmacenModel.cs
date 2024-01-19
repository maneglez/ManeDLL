using Mane.BD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mane.CommonModules.WMS
{
    internal class AlmacenModel : Modelo<AlmacenModel>
    {
        protected override string ConnName => Configuration.ConnectionName;
        protected override string idName => "WhsCode";
        protected override string NombreTabla => "OWHS";
        public string WhsCode { get; set; }
        public string WhsName { get; set; }
        public string BinActivat { get; set; }

        [ManeBdIgnorarProp]
        public string DisplayName => $"{WhsCode}-{WhsName}";
        public static ModeloCollection AlmacenesActivos() => Query().Where("Inactive", "N").Get();

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj is AlmacenModel a)
                return a.WhsCode == WhsCode && a.WhsName == WhsName;
            return false;
        }
    }
}
