﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WPFAuction.ServiceReferenceSeller {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReferenceSeller.IForSeller", CallbackContract=typeof(WPFAuction.ServiceReferenceSeller.IForSellerCallback))]
    public interface IForSeller {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForSeller/GetServerLotDTOForSeller", ReplyAction="http://tempuri.org/IForSeller/GetServerLotDTOForSellerResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AuctionBLLService.DTOClasses.ServerLotDTO[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AuctionBLLService.DTOClasses.ServerLotDTO))]
        AuctionBLLService.DTOClasses.ServerLotDTO[] GetServerLotDTOForSeller(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForSeller/GetServerLotDTOForSeller", ReplyAction="http://tempuri.org/IForSeller/GetServerLotDTOForSellerResponse")]
        System.Threading.Tasks.Task<AuctionBLLService.DTOClasses.ServerLotDTO[]> GetServerLotDTOForSellerAsync(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForSeller/ConnectionForSeller", ReplyAction="http://tempuri.org/IForSeller/ConnectionForSellerResponse")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AuctionBLLService.DTOClasses.ServerLotDTO[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AuctionBLLService.DTOClasses.ServerLotDTO))]
        bool ConnectionForSeller(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForSeller/ConnectionForSeller", ReplyAction="http://tempuri.org/IForSeller/ConnectionForSellerResponse")]
        System.Threading.Tasks.Task<bool> ConnectionForSellerAsync(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IForSeller/AddLot")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AuctionBLLService.DTOClasses.ServerLotDTO[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AuctionBLLService.DTOClasses.ServerLotDTO))]
        void AddLot(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO, AuctionBLLService.DTOClasses.ServerLotDTO serverLotDTO);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IForSeller/AddLot")]
        System.Threading.Tasks.Task AddLotAsync(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO, AuctionBLLService.DTOClasses.ServerLotDTO serverLotDTO);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IForSeller/DisconnectionForSeller")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AuctionBLLService.DTOClasses.ServerLotDTO[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AuctionBLLService.DTOClasses.ServerLotDTO))]
        void DisconnectionForSeller(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IForSeller/DisconnectionForSeller")]
        System.Threading.Tasks.Task DisconnectionForSellerAsync(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IForSeller/DeleteLot")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AuctionBLLService.DTOClasses.ServerLotDTO[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AuctionBLLService.DTOClasses.ServerLotDTO))]
        void DeleteLot(AuctionBLLService.DTOClasses.ServerLotDTO serverLotDTO, AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IForSeller/DeleteLot")]
        System.Threading.Tasks.Task DeleteLotAsync(AuctionBLLService.DTOClasses.ServerLotDTO serverLotDTO, AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IForSeller/UpdateLot")]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AuctionBLLService.DTOClasses.ServerLotDTO[]))]
        [System.ServiceModel.ServiceKnownTypeAttribute(typeof(AuctionBLLService.DTOClasses.ServerLotDTO))]
        void UpdateLot(AuctionBLLService.DTOClasses.ServerLotDTO serverLotDTO, AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IForSeller/UpdateLot")]
        System.Threading.Tasks.Task UpdateLotAsync(AuctionBLLService.DTOClasses.ServerLotDTO serverLotDTO, AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IForSellerCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IForSeller/ReturnSellerLot")]
        void ReturnSellerLot(AuctionBLLService.DTOClasses.ServerLotDTO[] lots);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IForSellerChannel : WPFAuction.ServiceReferenceSeller.IForSeller, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ForSellerClient : System.ServiceModel.DuplexClientBase<WPFAuction.ServiceReferenceSeller.IForSeller>, WPFAuction.ServiceReferenceSeller.IForSeller {
        
        public ForSellerClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ForSellerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ForSellerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ForSellerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ForSellerClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public AuctionBLLService.DTOClasses.ServerLotDTO[] GetServerLotDTOForSeller(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO) {
            return base.Channel.GetServerLotDTOForSeller(serverSellerDTO);
        }
        
        public System.Threading.Tasks.Task<AuctionBLLService.DTOClasses.ServerLotDTO[]> GetServerLotDTOForSellerAsync(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO) {
            return base.Channel.GetServerLotDTOForSellerAsync(serverSellerDTO);
        }
        
        public bool ConnectionForSeller(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO) {
            return base.Channel.ConnectionForSeller(serverSellerDTO);
        }
        
        public System.Threading.Tasks.Task<bool> ConnectionForSellerAsync(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO) {
            return base.Channel.ConnectionForSellerAsync(serverSellerDTO);
        }
        
        public void AddLot(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO, AuctionBLLService.DTOClasses.ServerLotDTO serverLotDTO) {
            base.Channel.AddLot(serverSellerDTO, serverLotDTO);
        }
        
        public System.Threading.Tasks.Task AddLotAsync(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO, AuctionBLLService.DTOClasses.ServerLotDTO serverLotDTO) {
            return base.Channel.AddLotAsync(serverSellerDTO, serverLotDTO);
        }
        
        public void DisconnectionForSeller(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO) {
            base.Channel.DisconnectionForSeller(serverSellerDTO);
        }
        
        public System.Threading.Tasks.Task DisconnectionForSellerAsync(AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO) {
            return base.Channel.DisconnectionForSellerAsync(serverSellerDTO);
        }
        
        public void DeleteLot(AuctionBLLService.DTOClasses.ServerLotDTO serverLotDTO, AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO) {
            base.Channel.DeleteLot(serverLotDTO, serverSellerDTO);
        }
        
        public System.Threading.Tasks.Task DeleteLotAsync(AuctionBLLService.DTOClasses.ServerLotDTO serverLotDTO, AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO) {
            return base.Channel.DeleteLotAsync(serverLotDTO, serverSellerDTO);
        }
        
        public void UpdateLot(AuctionBLLService.DTOClasses.ServerLotDTO serverLotDTO, AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO) {
            base.Channel.UpdateLot(serverLotDTO, serverSellerDTO);
        }
        
        public System.Threading.Tasks.Task UpdateLotAsync(AuctionBLLService.DTOClasses.ServerLotDTO serverLotDTO, AuctionBLLService.DTOClasses.ServerSellerDTO serverSellerDTO) {
            return base.Channel.UpdateLotAsync(serverLotDTO, serverSellerDTO);
        }
    }
}
