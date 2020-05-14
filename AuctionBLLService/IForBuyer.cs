using AuctionBLLService.DTOClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AuctionBLLService
{
    [ServiceContract(CallbackContract =typeof(IBuyerCallback))]
    public interface IForBuyer
    {
        [OperationContract]
        ICollection<ServerLotDTO> GetAllProduct();
        [OperationContract]
        bool ConnectionForBuyer(ServerBuyerDTO serverBuyerDTO);
        [OperationContract(IsOneWay = true)]
        void MakeBet(ServerLotDTO serverLotDTO, ServerBuyerDTO serverBuyerDTO, decimal newPrice);
        [OperationContract(IsOneWay = true)]
        void DisconectionForBuyer(ServerBuyerDTO serverBuyerDTO);
        [OperationContract(IsOneWay = true)]
        void BoughtLot(ServerBuyerDTO serverBuyerDTO, ServerLotDTO serverLotDTO);
        [OperationContract(IsOneWay = true)]
        void AddCashForBuyer(ServerBuyerDTO serverBuyerDTO);
    }

    public interface IBuyerCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReturnBuyerCash(decimal cash);
    }
}
