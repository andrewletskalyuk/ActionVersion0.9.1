using AuctionBLLService.DTOClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AuctionBLLService
{
    [ServiceContract]
    public interface IForSeller
    {
        [OperationContract]
        IEnumerable<ServerLotDTO> GetServerLotDTOForSeller(ServerSellerDTO serverSellerDTO);
        [OperationContract]
        bool ConnectionForSeller(ServerSellerDTO serverSellerDTO);
        [OperationContract(IsOneWay = true)]
        void AddLot(ServerSellerDTO serverSellerDTO, ServerLotDTO serverLotDTO);
        [OperationContract(IsOneWay = true)]
        void DisconnectionForSeller(ServerSellerDTO serverSellerDTO);
        [OperationContract(IsOneWay = true)]
        void DeleteLot(ServerLotDTO serverLotDTO, ServerSellerDTO serverSellerDTO);
        [OperationContract(IsOneWay = true)]
        void UpdateLot(ServerLotDTO serverLotDTO, ServerSellerDTO serverSellerDTO);
    }
}
