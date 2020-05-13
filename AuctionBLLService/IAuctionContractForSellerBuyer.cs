using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AuctionBLLService
{
    [ServiceContract]
    public interface IAuctionContractForSellerBuyer
    {
        [OperationContract]
        IEnumerable<ServerLotDTO> GetAllProduct();
    }
    
}
