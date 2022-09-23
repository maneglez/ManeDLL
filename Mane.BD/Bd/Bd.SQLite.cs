using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ;

namespace Mane.BD
{
    public static partial class Bd
    {
        public DataTable executeQuerySQLite(string query,string connName)
        {
            using (SQLiteConnection db = new SQLiteConnection(_path))
            {
                db.CreateTable<Configuracion>();
            }
        }
    }
}
