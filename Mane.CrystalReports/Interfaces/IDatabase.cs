using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CrystalReports.Interfaces
{
    public interface IDatabase
    {
        ICollection<ITable> Tables { get; set; }
    }
}
