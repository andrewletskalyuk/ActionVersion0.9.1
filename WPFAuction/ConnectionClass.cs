using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFAuction.ServiceReferenceBuyer;

namespace WPFAuction
{
    public class ConnectionClass
    {
        public ForBuyerClient BuyerClient { get; set; }
        public ConnectionClass()
        {

        }
        public void AddBuyerClient(ForBuyerClient client)
        {
            BuyerClient = client;
        }
    }
}
