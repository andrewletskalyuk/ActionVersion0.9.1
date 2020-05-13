using AuctionDB;
using AuctionDB.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionDB
{
    public class Lots
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsSell { get; set; }
        public decimal StartPrice { get; set; }
        public decimal? SoldPrice { get; set; }
        public string Photo { get; set; }
        
        public int? SellerId { get; set; }
        
        public int? BuyerId { get; set; }
        [ForeignKey(nameof(SellerId))]
        public virtual Sellers Seller { get; set; }
        [ForeignKey(nameof(BuyerId))]
        
        public virtual Buyers Buyer { get; set; }
    }
}

