using AuctionBLLService.DTOClasses;
using AuctionDB;
using AuctionDB.ClassesWithAccessToDB;
using AuctionDB.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace AuctionBLLService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AuctionServiceSeller : IForSeller
    {
        UpdateAuctionService update = new UpdateAuctionService();
        public SellerWrapper sellerWrapper=null;
        List<ServerSellerDTO> tempSellersOnline = new List<ServerSellerDTO>();
        public IMapper mapperLot = null;
        public IMapper mapperSeller = null;
        public AuctionServiceSeller()
        {
            sellerWrapper = new SellerWrapper();

            var configLot = new MapperConfiguration(x =>
            {
                x.CreateMap<Lots, ServerLotDTO>();
                x.CreateMap<ServerLotDTO, Lots>();
            });
            mapperLot = configLot.CreateMapper();

            var configSeller = new MapperConfiguration(x =>
            {
                x.CreateMap<Sellers, ServerSellerDTO>();
                x.CreateMap<ServerSellerDTO, Sellers>();
            });
            mapperSeller = configSeller.CreateMapper();
        }

        public void AddLot(ServerSellerDTO serverSellerDTO, ServerLotDTO serverLotDTO)
        {
            tempSellersOnline.FirstOrDefault(x => x.Name == serverSellerDTO.Name && 
                                             x.Password == serverSellerDTO.Password).SellerLots.Add(serverLotDTO);
            var tempLot = mapperLot.Map<ServerLotDTO, Lots>(serverLotDTO);
            var tempSeller = mapperSeller.Map<ServerSellerDTO, Sellers>(serverSellerDTO);

            sellerWrapper.AddLot(tempLot,tempSeller);
            //викликати CallBack для баєра
          // update.buyer
        }

        public bool ConnectionForSeller(ServerSellerDTO serverSellerDTO)
        {
            Sellers temp = mapperSeller.Map<ServerSellerDTO, Sellers>(serverSellerDTO);
            if (sellerWrapper.DoesItSellerExist(temp))
            {
                if (tempSellersOnline.FirstOrDefault(x => x.Name == serverSellerDTO.Name
                                                    && x.Password == serverSellerDTO.Password) == null)
                {
                    tempSellersOnline.Add(serverSellerDTO);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                sellerWrapper.AddSeller(temp);
                tempSellersOnline.Add(serverSellerDTO);
                return true;
            }
        }

        //написати callback для синхронізацї лотів в селлера та байера
        public void DeleteLot(ServerLotDTO serverLotDTO, ServerSellerDTO serverSellerDTO)
        {
            tempSellersOnline.FirstOrDefault(x => x.Name == serverSellerDTO.Name && 
                                             x.Password == serverSellerDTO.Password).
                                             SellerLots.Remove(serverLotDTO);
            var tempLot = mapperLot.Map<ServerLotDTO, Lots>(serverLotDTO);
            sellerWrapper.DeleteLot(tempLot); 
        }

        public void DisconnectionForSeller(ServerSellerDTO serverSellerDTO)
        {
            tempSellersOnline.Remove(serverSellerDTO);
        }

        public IEnumerable<ServerLotDTO> GetServerLotDTOForSeller(ServerSellerDTO serverSellerDTO)
        {
            var tempLots = mapperLot.Map<IEnumerable<Lots>, IEnumerable<ServerLotDTO>>(sellerWrapper.GetLots()).ToList();
            tempSellersOnline.FirstOrDefault(x => x.Name == serverSellerDTO.Name &&
                                             x.Password == serverSellerDTO.Password).
                                             SellerLots = tempLots;
            return tempLots;
        }

        //подумати чи дійсно цей метод нам треба???
        public void UpdateLot(ServerLotDTO serverLotDTO, ServerSellerDTO serverSellerDTO)
        {
            var tempSeller = tempSellersOnline.FirstOrDefault(x => x.Name == serverSellerDTO.Name && x.Password == serverSellerDTO.Password);

        }
    }
}
