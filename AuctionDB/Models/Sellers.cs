using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionDB.Models
{
    
    public class Sellers
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public ICollection<Lots> Products { get; set; }
        public Sellers()
        {
            Products = new HashSet<Lots>();
        }
    }
}
