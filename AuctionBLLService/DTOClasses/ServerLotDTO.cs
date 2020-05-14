namespace AuctionBLLService.DTOClasses
{
    public class ServerLotDTO
    {
        //передивитись
        public string BuyerName { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public string  Name { get; set; }
        public decimal StartPrice { get; set; }
        public decimal SoldPrice { get; set; }
        public string Photo { get; set; }
        public bool IsSold { get; set; }
        public ServerLotDTO()
        {
        }
    }
}