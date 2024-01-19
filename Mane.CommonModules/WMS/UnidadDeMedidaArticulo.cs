using Mane.BD.Helpers;
using System.Collections.Generic;
using System.Data;
using System;

namespace Mane.CommonModules.WMS
{
    internal class UnidadDeMedidaArticulo
    {
        private readonly string uomCode;
        private readonly double quantity;
        private readonly string uomName;
        private readonly int ugpEntry;

        public UnidadDeMedidaArticulo(string uomCode, string uomName, int ugpEntry, double quantity)
        {
            this.uomCode = uomCode;
            this.uomName = uomName;
            this.ugpEntry = ugpEntry;
            this.quantity = quantity;
        }

        public string UomCode => uomCode;
        public string UomName => uomName;
        public int UgpEntry => ugpEntry;
        public double Quantity => quantity;
        public static List<UnidadDeMedidaArticulo> Get(string itemCode)
        {
            var dt = QueriesSapb1.UnidadesDeMedidaArticulo(itemCode)
                .Get(Configuration.ConnectionName);
            var lst = new List<UnidadDeMedidaArticulo>();
            foreach (DataRow item in dt.Rows)
            {
                lst.Add(new UnidadDeMedidaArticulo(item["UomCode"].ToString(),
                    item["UomName"].ToString(),
                    Convert.ToInt32(item["UgpEntry"]),
                    Convert.ToDouble(item["BaseQty"]) / Convert.ToDouble(item["AltQty"])));
            }
            return lst;
        }
    }

}