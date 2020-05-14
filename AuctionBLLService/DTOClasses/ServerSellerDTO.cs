using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ServiceModel;

namespace AuctionBLLService.DTOClasses
{
    public class ServerSellerDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsOnline { get; set; }
        public ISellerCallback sellerCallback { get; set; }
        public List<ServerLotDTO> SellerLot { get; set; } //лоти що створив/додав
        public ServerSellerDTO()
        {
            SellerLot = new List<ServerLotDTO>();
        }
    }
}