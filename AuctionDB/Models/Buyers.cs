using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionDB.Models
{
    public class Buyers
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public decimal Cash { get; set; }
        public virtual ICollection<Lots> BoughtLots { get; set; }
        public Buyers()
        {
            BoughtLots = new HashSet<Lots>();
        }
    }
}
