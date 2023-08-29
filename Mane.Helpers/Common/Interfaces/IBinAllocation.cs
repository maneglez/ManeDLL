using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mane.Helpers.Common
{
    public interface IBinAllocation
    {
        int BinAbsEntry { get; set; }
        double Quantity { get; set; }
        int SerialAndBatchNumbersBaseLine { get; set; }
        int BaseLineNumber { get; set; }
    }
}
