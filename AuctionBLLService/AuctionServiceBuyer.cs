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
    public class AuctionServiceBuyer : IForBuyer 
    {
        public BuyerWrapper buyerWrapper=null;
        public IMapper mapperLot = null;
        public IMapper mapperBuyer = null;
        List<ServerLotDTO> templotsBuyer = new List<ServerLotDTO>(); //всі лоти 
       public List<ServerBuyerDTO> tempBuyersOnline = new List<ServerBuyerDTO>();
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
        }
        public bool ConnectionForBuyer(ServerBuyerDTO serverBuyerDTO)
        {
            Buyers temp = mapperBuyer.Map<ServerBuyerDTO, Buyers>(serverBuyerDTO);
            if (buyerWrapper.DoesItBuyerExist(temp))
            {
                if (tempBuyersOnline.FirstOrDefault(x=>x.Name==serverBuyerDTO.Name 
                                                    && x.Password==serverBuyerDTO.Password)==null)
                {
                    tempBuyersOnline.Add(serverBuyerDTO);
                    return true;
                }
                else
                {
                    return false;
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
            tempBuyersOnline.FirstOrDefault(x => x.Name == serverBuyerDTO.Name && 
                                            x.Password == serverBuyerDTO.Password).
                                            BuyerBoughtLots.Add(serverLotDTO);
            Lots tempLotF = mapperLot.Map<ServerLotDTO, Lots>(serverLotDTO);
            Buyers tempBuyerF = mapperBuyer.Map<ServerBuyerDTO, Buyers>(serverBuyerDTO);
            buyerWrapper.BoughtLot(tempLotF, tempBuyerF);
        }

        public ICollection<ServerLotDTO> GetAllProduct()
        {
            templotsBuyer = mapperLot.Map<IEnumerable<Lots>, IEnumerable<ServerLotDTO>>(buyerWrapper.GetLots()).ToList();
            return templotsBuyer;
        }

        public void AddCashForBuyer(ServerBuyerDTO serverBuyerDTO)
        {
            tempBuyersOnline.FirstOrDefault(x => x.Name == serverBuyerDTO.Name &&
                                            x.Password == serverBuyerDTO.Password).Cash = serverBuyerDTO.Cash;
            Buyers temp = mapperBuyer.Map<ServerBuyerDTO, Buyers>(serverBuyerDTO);
            buyerWrapper.AddCashForBuyer(temp);
        }

        //передивитись на предмет дісконнекта, коли баєр уходить що робити з грошима і ставкою
        public void DisconectionForBuyer(ServerBuyerDTO serverBuyerDTO)
        {
            tempBuyersOnline.Remove(serverBuyerDTO);
        }

        public void MakeBet(ServerLotDTO serverLotDTO, ServerBuyerDTO serverBuyerDTO, decimal newPrice)
        {
            tempBuyersOnline.FirstOrDefault(x => x.Name == serverBuyerDTO.Name &&
                                            x.Password == serverBuyerDTO.Password).Cash -= newPrice;

            var tempLot = templotsBuyer.FirstOrDefault(x => x.BuyerName == serverLotDTO.BuyerName);
            if (tempLot != null)
            {
                tempBuyersOnline.FirstOrDefault(x => x.Name == tempLot.BuyerName).Cash += tempLot.StartPrice;
            }

            templotsBuyer.FirstOrDefault(x => x.BuyerName == serverLotDTO.BuyerName).StartPrice = newPrice;
            templotsBuyer.FirstOrDefault(x => x.BuyerName == serverLotDTO.BuyerName).BuyerName = serverBuyerDTO.Name;
        }
    }
}
