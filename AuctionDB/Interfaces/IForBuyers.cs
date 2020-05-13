using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionDB.Interfaces
{
    public interface IForBuyers<L,B>
    {
        void AddBuyer(B item);
        void AddCashForBuyer(B buyer);
        bool DoesItBuyerExist(B buyer);
        void BoughtLot(L item, B buyer);
        IEnumerable<L> GetLots();
    }
}
