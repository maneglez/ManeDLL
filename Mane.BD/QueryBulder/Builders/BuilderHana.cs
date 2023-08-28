using System;

namespace Mane.BD.QueryBulder.Builders
{
    internal class BuilderHana : BuilderSQLite
    {
        protected override TipoDeBd Tipo => TipoDeBd.Hana;
        public string DataBaseName { get; set; }
        public override string SelectLastInsertedIndexQuery => "";//$"select current_identity_value() FROM {FormatTable(QueryBuilder.Tabla)}";
        public BuilderHana(QueryBuilder queryBuilder, string DataBaseName = "") : base(queryBuilder)
        {
            ColumnDelimiters = new char[] { '"', '"' };
            this.DataBaseName = DataBaseName;
        }

        public override string FormatTable(string value)
        {
            if (string.IsNullOrEmpty(value)) return String.Empty;
            return base.FormatTable($"{DataBaseName}.{value}");
        }
        public override string BuildWeres()
        {

            var where = "";
            var qb = QueryBuilder;
            if (qb.Wheres.Count == 0) return where;
            foreach (var w in qb.Wheres)
            {
                where += $" {w.Boleano} {FormatColumn(w.Columna)} {w.Operador} ";
                switch (w.Tipo)
                {
                    case QueryBuilder.TipoWhere.WhereIn:
                        string val = "";
                        if (w.Valor.GetType().IsArray)
                        {
                            Array valores = (Array)w.Valor;
                            foreach (var item in valores)
                            {
                                val += $"{FormatValue(item)},";
                            }
                            val = val.Trim(',');
                            val = $"({val})";
                        }
                        else if (w.Valor.GetType() == typeof(QueryBuilder))
                        {
                            var q = (QueryBuilder)w.Valor;
                            val = $"({BuildSubQuery(q)})";
                        }
                        where = where.Trim(' ');
                        where += val;
                        break;
                    case QueryBuilder.TipoWhere.WhereColumn:
                        where += FormatColumn((string)w.Valor);
                        break;
                    case QueryBuilder.TipoWhere.WhereGroup:
                        QueryBuilder q1 = (QueryBuilder)w.Valor;
                        var tmpConn = new Conexion { NombreBD = DataBaseName, TipoDeBaseDeDatos = TipoDeBd.Hana };
                        if (q1.Columnas.Length == 0) where += $"({q1.GetBuilder(tmpConn).BuildWeres()})";
                        else where += $"({BuildSubQuery(q1)})";
                        break;
                    case QueryBuilder.TipoWhere.WhereBetween:
                        var vals = (object[])w.Valor;
                        where += $"{FormatValue(vals[0])} AND {FormatValue(vals[1])}";
                        break;
                    case QueryBuilder.TipoWhere.WhereNull:
                        break;
                    default:
                        where += FormatValue(w.Valor);
                        break;
                }
            }
            where = where.Substring(qb.Wheres[0].Boleano.Length + 1);
            return where;

        }

        protected override string BuildSubQuery(QueryBuilder q)
        {
            return new BuilderHana(q, DataBaseName).BuildQuery();
        }

    }
}
