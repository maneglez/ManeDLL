using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CrystalReports.Interfaces
{
    public interface ITableLogonInfo
    {
        string TableName { get; set; }
        string ReportName { get; set; }
        IConnectionInfo ConnectionInfo { get; set; }

    }
}
