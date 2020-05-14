using System.Collections.ObjectModel;
using System.ServiceModel;

namespace AuctionBLLService.DTOClasses
{

    public class ServerBuyerDTO
    {
        public ServerBuyerDTO()
        {

        }
        public string Name { get; set; }
        public string Password { get; set; }
        public decimal Cash { get; set; }
        public IBuyerCallback buyerCallback { get; set; }
        
        public ObservableCollection<ServerLotDTO> BuyerSelectedLots { get; set; } //лоти що зробив ставку
        public ObservableCollection<ServerLotDTO> BuyerBoughtLots { get; set; } //лоти що купив
    }
}
