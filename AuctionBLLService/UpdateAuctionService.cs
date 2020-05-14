using AuctionBLLService.DTOClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AuctionBLLService
{

    public class UpdateAuctionService:IUpdateService 
    {
        
       
       // List<ISellerCallback> sellerCallbacks;
        public UpdateAuctionService(IBuyerCallback buyerCallback)
        {
          //  this.buyerCallback.Add(buyerCallback);

        }
        public UpdateAuctionService()
        {

        }
        public void UpdateBuyer()
        {
         //   buyerCallback.Add(OperationContext.Current.GetCallbackChannel<IBuyerCallback>());
        }
        public void UpdateSeller()
        {

        }
    }
}
