using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.CrystalReports.Interfaces
{
    public interface IConnectionInfo
    {
        string ServerName { get; set; }
        string DatabaseName { get; set; }
        string UserID { get; set; }
        string Password { get; set; }
        ConnectionInfoType Type { get; set; }
    }
}
