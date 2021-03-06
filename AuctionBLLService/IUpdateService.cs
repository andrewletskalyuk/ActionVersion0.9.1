﻿using AuctionBLLService.DTOClasses;
using System.ServiceModel;

namespace AuctionBLLService
{
    [ServiceContract(CallbackContract = typeof(IUpdateCallBack))]
    public interface IUpdateService
    {
        [OperationContract(IsOneWay = true)]
        void Refresh();
    }
    
    public interface IUpdateCallBack
    {
        [OperationContract(IsOneWay = true)]
        void UpdateLot(ServerLotDTO lot);
    }
}
