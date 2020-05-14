using System.Collections.ObjectModel;
using System.ServiceModel;

namespace AuctionBLLService.DTOClasses
{

    public class ServerBuyerDTO
    {
        public ServerBuyerDTO()
        {
            BuyerBoughtLotsDTO = new ObservableCollection<ServerLotDTO>();
            BuyerSelectedLotsDTO = new ObservableCollection<ServerLotDTO>();
        }
        public string Name { get; set; }
        public string Password { get; set; }
        public decimal Cash { get; set; }
        public IBuyerCallback buyerCallback { get; set; }
        public bool IsOnline { get; set; }
        public ObservableCollection<ServerLotDTO> BuyerSelectedLotsDTO { get; set; } //лоти що зробив ставку
        public ObservableCollection<ServerLotDTO> BuyerBoughtLotsDTO { get; set; } //лоти що купив
    }
}
