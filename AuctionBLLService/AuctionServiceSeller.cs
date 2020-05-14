using AuctionBLLService.DTOClasses;
using AuctionDB;
using AuctionDB.ClassesWithAccessToDB;
using AuctionDB.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;

namespace AuctionBLLService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AuctionServiceSeller : IForSeller
    {

        public SellerWrapper sellerWrapper = null;
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
                                             x.Password == serverSellerDTO.Password).SellerLot.Add(serverLotDTO);
            var tempLot = mapperLot.Map<ServerLotDTO, Lots>(serverLotDTO);
            var tempSeller = mapperSeller.Map<ServerSellerDTO, Sellers>(serverSellerDTO);

            sellerWrapper.AddLot(tempLot, tempSeller);
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

                    serverSellerDTO.sellerCallback = OperationContext.Current.GetCallbackChannel<ISellerCallback>();

                    var tempSeller = mapperSeller.Map<ServerSellerDTO, Sellers>(serverSellerDTO);
                    var sellerlots = sellerWrapper.GetSellerLots(tempSeller);
                    var tempLot = mapperLot.Map<IEnumerable<Lots>, IEnumerable<ServerLotDTO>>(sellerlots);
                    foreach (ServerLotDTO item in tempLot)
                    { serverSellerDTO.SellerLot.Add(item); }
                    serverSellerDTO.IsOnline = true;
                    tempSellersOnline.Add(serverSellerDTO);

                    foreach (var item in tempSellersOnline)
                    {
                        if (item == serverSellerDTO)
                            item.sellerCallback.ReturnSellerLot(tempLot.ToList());
                    }
                    return true;
                }
                else
                {
                    if (serverSellerDTO.IsOnline == true)
                        return false;
                    else
                    {
                        tempSellersOnline.FirstOrDefault(x => x.Name == serverSellerDTO.Name
                                                     && x.Password == serverSellerDTO.Password).IsOnline = true;
                        tempSellersOnline.FirstOrDefault(x => x.Name == serverSellerDTO.Name
                                                    && x.Password == serverSellerDTO.Password).sellerCallback = 
                                                    OperationContext.Current.GetCallbackChannel<ISellerCallback>();
                        foreach (var item in tempSellersOnline)
                        {
                            if (item.Name == serverSellerDTO.Name)
                                item.sellerCallback.ReturnSellerLot(item.SellerLot);
                        }
                        return true;
                    }
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
                                             SellerLot.Remove(serverLotDTO);
            var tempLot = mapperLot.Map<ServerLotDTO, Lots>(serverLotDTO);
            sellerWrapper.DeleteLot(tempLot);
        }

        public void DisconnectionForSeller(ServerSellerDTO serverSellerDTO)
        {
            tempSellersOnline.FirstOrDefault(x => x.Name == serverSellerDTO.Name && x.Password == serverSellerDTO.Password).IsOnline = false;
        }
        //МОЖНА ВИКОРИСТАТИ ЦЕЙ МЕТОД ПРИ КРННЕКТІ
        public IEnumerable<ServerLotDTO> GetServerLotDTOForSeller(ServerSellerDTO serverSellerDTO)
        {
            var tempLots = mapperLot.Map<IEnumerable<Lots>, IEnumerable<ServerLotDTO>>(sellerWrapper.GetLots()).ToList();
            //tempSellersOnline.FirstOrDefault(x => x.Name == serverSellerDTO.Name &&
            //                                 x.Password == serverSellerDTO.Password).
            //                                 SellerLots = tempLots;
            return tempLots;
        }

        //подумати чи дійсно цей метод нам треба???
        public void UpdateLot(ServerLotDTO serverLotDTO, ServerSellerDTO serverSellerDTO)
        {
            var tempSeller = tempSellersOnline.FirstOrDefault(x => x.Name == serverSellerDTO.Name && x.Password == serverSellerDTO.Password);

        }
    }
}
