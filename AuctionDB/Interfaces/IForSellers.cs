using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionDB.Interfaces
{
    public interface IForSellers<L,S>
    {
        void AddLot(L item,S seller);
        void AddSeller(S item);
        void UpdateLot(L item);
        void DeleteLot(L item);
        bool DoesItSellerExist(S seller);
        IEnumerable<L> GetSellerLots(S item);
    }
}
