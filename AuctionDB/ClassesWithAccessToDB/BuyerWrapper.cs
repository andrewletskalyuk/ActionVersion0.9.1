using AuctionDB.Interfaces;
using AuctionDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionDB.ClassesWithAccessToDB
{
    public class BuyerWrapper : IForBuyers<Lots, Buyers>
    {
        AuctionModel auctionModel;
        public BuyerWrapper()
        {
            auctionModel = new AuctionModel();
        }
        ~BuyerWrapper()
        {
            auctionModel.Dispose();
            auctionModel = null;
        }
        public void AddBuyer(Buyers item)
        {
            auctionModel.Buyers.Add(item);
            Commit();
        }

        public void AddCashForBuyer(Buyers buyer)
        {
            var _buyer = auctionModel.Buyers.FirstOrDefault(b => b.Name == buyer.Name && b.Password == buyer.Password);
            _buyer.Cash = buyer.Cash;
            auctionModel.Entry(_buyer).State = System.Data.Entity.EntityState.Modified;
            Commit();
        }

        public void BoughtLot(Lots item, Buyers buyer)
        {
            var _buyer = auctionModel.Buyers.FirstOrDefault(b => b.Name == buyer.Name && b.Password==buyer.Password);
            auctionModel.Lots.FirstOrDefault(x => x.Name == item.Name).BuyerId = _buyer.Id;
            Commit();
        }

        public bool DoesItBuyerExist(Buyers buyer)
        {
            var _buyer = auctionModel.Buyers.FirstOrDefault(b => b.Name == buyer.Name && b.Password == buyer.Password);
            return _buyer != null ? true : false;
        }

        public IEnumerable<Lots> GetLots()
        {
            IEnumerable<Lots> lots = auctionModel.Lots;
            return lots;
        }
        public void Commit()
        {
            auctionModel.SaveChanges();
        }
    }
}
