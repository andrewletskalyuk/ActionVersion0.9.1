using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionDB.Interfaces
{
    public interface IGetLotsForSellerBuyer<T>
    {
        IEnumerable<T> GetLots(); 
    }
}
