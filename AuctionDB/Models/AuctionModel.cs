using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionDB.Models
{
    public class AuctionModel : DbContext
    {
        public AuctionModel(): base ("name=AuctionModel")
        {
            Database.SetInitializer<AuctionModel>(new CreateDatabaseIfNotExists<AuctionModel>());
        }
        public virtual DbSet<Buyers>  Buyers { get; set; }
        public virtual DbSet<Lots>  Lots { get; set; }
        public virtual DbSet<Sellers>  Sellers { get; set; }
    }
}
