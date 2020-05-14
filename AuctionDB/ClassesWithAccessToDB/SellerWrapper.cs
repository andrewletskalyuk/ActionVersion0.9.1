using AuctionDB.Interfaces;
using AuctionDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionDB.ClassesWithAccessToDB
{
    public class SellerWrapper : IForSellers<Lots, Sellers>
    {
        AuctionModel auctionContext;
        public SellerWrapper()
        {
            auctionContext = new AuctionModel();
        }
        ~SellerWrapper()
        {
            auctionContext.Dispose();
            auctionContext = null;
        }
        public void AddLot(Lots item,Sellers seller)
        {

            var tempseller = auctionContext.Sellers.FirstOrDefault(x => x.Name == seller.Name && x.Password == seller.Password);
            item.SellerId = tempseller.Id;
            item.BuyerId = 4;
                auctionContext.Lots.Add(item);
                Commit();

        }

        public void AddSeller(Sellers item)
        {
            auctionContext.Sellers.Add(item);
            Commit();
        }

        public void DeleteLot(Lots item)
        {
            var flot = auctionContext.Lots.FirstOrDefault(i => i.Name == item.Name);
            auctionContext.Lots.Remove(flot);
            Commit();
        }

        public bool DoesItSellerExist(Sellers seller)
        {
            var _seller = auctionContext.Sellers.FirstOrDefault(b => b.Name == seller.Name && b.Password == seller.Password);
            return _seller != null ? true : false;
        }

        public IEnumerable<Lots> GetLots()
        {
            IEnumerable<Lots> lots = auctionContext.Lots;
            return lots;
        }

        public IEnumerable<Lots> GetSellerLots(Sellers item)
        {
            var sellerLots = (from obj in auctionContext.Lots
                              where obj.Seller.Name == item.Name &&
                                    obj.Seller.Password == item.Password
                              select obj).ToList();
            return sellerLots;
        }

        public void UpdateLot(Lots item)
        {
            auctionContext.Entry(item).State = System.Data.Entity.EntityState.Modified;
            Commit();
        }
        public void Commit()
        {
            auctionContext.SaveChanges();
        }
    }
}
