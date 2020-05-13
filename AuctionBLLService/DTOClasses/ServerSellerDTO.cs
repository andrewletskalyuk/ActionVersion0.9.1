using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace AuctionBLLService.DTOClasses
{
    public class ServerSellerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
       // public OperationContext operationContextCallBack { get; set; }
        public List<ServerLotDTO> SellerLots { get; set; } //лоти що створив/додав
    }
}