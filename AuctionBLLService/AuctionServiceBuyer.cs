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


    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class AuctionServiceBuyer : UpdateAuctionService, IForBuyer
    {
        public List<ServerBuyerDTO> tempBuyersOnline = new List<ServerBuyerDTO>();

        public BuyerWrapper buyerWrapper = null;
        public IMapper mapperLot = null;
        public IMapper mapperBuyer = null;
        List<ServerLotDTO> templotsBuyer = new List<ServerLotDTO>(); //всі лоти 

        public AuctionServiceBuyer()
        {

            buyerWrapper = new BuyerWrapper();

            var configLot = new MapperConfiguration(x =>
            {
                x.CreateMap<Lots, ServerLotDTO>();
                x.CreateMap<ServerLotDTO, Lots>();
            });
            mapperLot = configLot.CreateMapper();

            var configBuyer = new MapperConfiguration(x =>
            {
                x.CreateMap<Buyers, ServerBuyerDTO>();
                x.CreateMap<ServerBuyerDTO, Buyers>();
            });
            mapperBuyer = configBuyer.CreateMapper();

            templotsBuyer = mapperLot.Map<IEnumerable<Lots>, IEnumerable<ServerLotDTO>>(buyerWrapper.GetLots()).ToList();
        }
        public bool ConnectionForBuyer(ServerBuyerDTO serverBuyerDTO)
        {
            var test = mapperLot.Map<IEnumerable<Lots>, IEnumerable<ServerLotDTO>>(buyerWrapper.GetLots()).ToList();
            if(test.Count> templotsBuyer.Count)
            { 
                templotsBuyer.Add(test[test.Count-1]);
            }
            Buyers temp = mapperBuyer.Map<ServerBuyerDTO, Buyers>(serverBuyerDTO);
            if (buyerWrapper.DoesItBuyerExist(temp))
            {
                if (tempBuyersOnline.FirstOrDefault(x => x.Name == serverBuyerDTO.Name
                                                    && x.Password == serverBuyerDTO.Password) == null)
                {
                    //повертаємо гроші баєру які в нього є на рахунку

                    serverBuyerDTO.Cash = buyerWrapper.GetCurrentCash(temp);
                    serverBuyerDTO.buyerCallback = OperationContext.Current.GetCallbackChannel<IBuyerCallback>();
                    serverBuyerDTO.IsOnline = true;
                    tempBuyersOnline.Add(serverBuyerDTO);
                    foreach (ServerBuyerDTO item in tempBuyersOnline)
                    {
                        if (item.Name == serverBuyerDTO.Name)
                        {
                            decimal cash = item.Cash;
                            item.buyerCallback.ReturnBuyerCash(cash);
                        }
                    }

                    return true;
                }
                else
                {
                    if (tempBuyersOnline.FirstOrDefault(x => x.Name == serverBuyerDTO.Name
                                                     && x.Password == serverBuyerDTO.Password).IsOnline == true)
                        return false;
                    else
                    {
                        tempBuyersOnline.FirstOrDefault(x => x.Name == serverBuyerDTO.Name
                                                     && x.Password == serverBuyerDTO.Password).IsOnline = true;
                        tempBuyersOnline.FirstOrDefault(x => x.Name == serverBuyerDTO.Name
                                                     && x.Password == serverBuyerDTO.Password).buyerCallback =
                                                     OperationContext.Current.GetCallbackChannel<IBuyerCallback>();
                        foreach (ServerBuyerDTO item in tempBuyersOnline)
                        {
                            if (item.Name == serverBuyerDTO.Name)
                            {
                                decimal cash = item.Cash;
                                item.buyerCallback.ReturnBuyerCash(cash);
                                item.buyerCallback.UpdateBuyerLots(null, item.BuyerSelectedLotsDTO);
                                var boughtLots = tempBuyersOnline.FirstOrDefault(x => x.Name == serverBuyerDTO.Name).
                                               BuyerBoughtLotsDTO;
                                item.buyerCallback.UpdateBuyerBoughtLotsAfter(boughtLots);
                                
                            }
                        }

                        return true;
                    }
                }
            }
            else
            {
                buyerWrapper.AddBuyer(temp);
                tempBuyersOnline.Add(serverBuyerDTO);
                return true;
            }
        }

        public void BoughtLot(ServerBuyerDTO serverBuyerDTO, ServerLotDTO serverLotDTO)
        {

            if (serverBuyerDTO.Name == serverLotDTO.BuyerName)
            {
                var tempLot = templotsBuyer.FirstOrDefault(x => x.Name == serverLotDTO.Name);

                tempBuyersOnline.FirstOrDefault(x => x.Name == tempLot.BuyerName).BuyerSelectedLotsDTO.Remove(tempLot);


                tempBuyersOnline.FirstOrDefault(x => x.Name == serverLotDTO.BuyerName).
                                                BuyerBoughtLotsDTO.Add(tempLot);

                var boughtLots = tempBuyersOnline.FirstOrDefault(x => x.Name == serverLotDTO.BuyerName).
                                                BuyerBoughtLotsDTO;

                templotsBuyer.FirstOrDefault(x => x.Name == serverLotDTO.Name).IsSold = true;

                foreach (ServerBuyerDTO item in tempBuyersOnline)
                {
                    // item.buyerCallback.UpdateBuyerLots(null, item.BuyerSelectedLotsDTO);
                    item.buyerCallback.UpdateBuyerLots(null, item.BuyerSelectedLotsDTO);
                    item.buyerCallback.UpdateBuyerBoughtLots(templotsBuyer.FirstOrDefault(x => x.Name == serverLotDTO.Name));
                    if (item.Name == serverLotDTO.BuyerName)
                    {
                        item.buyerCallback.UpdateBuyerBoughtLotsAfter(boughtLots);
                    }
                }
            }
        }

        public ICollection<ServerLotDTO> GetAllProduct()
        {
            return templotsBuyer;
        }
        //ПЕРЕДИВИТИСЬ МЕТОД 
        public void AddCashForBuyer(ServerBuyerDTO serverBuyerDTO, decimal cash)
        {
            tempBuyersOnline.FirstOrDefault(x => x.Name == serverBuyerDTO.Name &&
                                            x.Password == serverBuyerDTO.Password).Cash += cash;
            serverBuyerDTO.Cash = tempBuyersOnline.FirstOrDefault(x => x.Name == serverBuyerDTO.Name &&
                                              x.Password == serverBuyerDTO.Password).Cash;
            Buyers temp = mapperBuyer.Map<ServerBuyerDTO, Buyers>(serverBuyerDTO);
            buyerWrapper.AddCashForBuyer(temp);
            foreach (ServerBuyerDTO item in tempBuyersOnline)
            {
                if (item.Name == serverBuyerDTO.Name)
                    item.buyerCallback.ReturnBuyerCash(item.Cash);
            }
        }

        public void DisconectionForBuyer(ServerBuyerDTO serverBuyerDTO)
        {
            tempBuyersOnline.FirstOrDefault(x => x.Name == serverBuyerDTO.Name && x.Password == serverBuyerDTO.Password).IsOnline = false;           
        }

        public void MakeBet(ServerLotDTO serverLotDTO, ServerBuyerDTO serverBuyerDTO, decimal newPrice)
        {

            tempBuyersOnline.FirstOrDefault(x => x.Name == serverBuyerDTO.Name &&
                                            x.Password == serverBuyerDTO.Password).Cash -= newPrice;

            var tempLot = templotsBuyer.FirstOrDefault(x => x.BuyerName == serverLotDTO.BuyerName);
            if (tempLot != null && tempLot.BuyerName != "No buyer")
            {
                tempBuyersOnline.FirstOrDefault(x => x.Name == tempLot.BuyerName).Cash += tempLot.StartPrice;
                tempBuyersOnline.FirstOrDefault(x => x.Name == tempLot.BuyerName).BuyerSelectedLotsDTO.Remove(tempLot);
            }

            templotsBuyer.FirstOrDefault(x => x.Photo == serverLotDTO.Photo).StartPrice = newPrice;
            templotsBuyer.FirstOrDefault(x => x.Photo == serverLotDTO.Photo).BuyerName = serverBuyerDTO.Name;
            var newtempLot = templotsBuyer.FirstOrDefault(x => x.Name == serverLotDTO.Name);
            tempBuyersOnline.FirstOrDefault(x => x.Name == serverBuyerDTO.Name &&
                                            x.Password == serverBuyerDTO.Password).BuyerSelectedLotsDTO.Add(newtempLot);
            foreach (ServerBuyerDTO item in tempBuyersOnline)
            {
                item.buyerCallback.UpdateBuyerLots(newtempLot, item.BuyerSelectedLotsDTO);
                item.buyerCallback.ReturnBuyerCash(item.Cash);
            }
        }
    }
}
