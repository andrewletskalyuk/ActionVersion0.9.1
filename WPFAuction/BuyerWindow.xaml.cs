using AuctionBLLService.DTOClasses;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPFAuction.ServiceReferenceBuyer;
using WPFAuction.ViewModel;

namespace WPFAuction
{
    /// <summary>
    /// Логика взаимодействия для BuyerWindow.xaml
    /// </summary>
    public partial class BuyerWindow : Window,IForBuyerCallback
    {
        ForBuyerClient ForBuyerClient;
        public AuctionViewModelBuyer BuyerViewModel { get; set; }
        public IMapper buyerMaper = null;
        public IMapper lotMaper = null;
        public BuyerWindow(string name,string password)
        {
            InitializeComponent();
            ForBuyerClient = new ForBuyerClient(new InstanceContext(this));
            var buyerconfig = new MapperConfiguration(x =>
            {
                x.CreateMap<ServerBuyerDTO, AuctionViewModelBuyer>();
                x.CreateMap<AuctionViewModelBuyer, ServerBuyerDTO>();
            });
            buyerMaper = buyerconfig.CreateMapper();

            var lotconfig = new MapperConfiguration(x =>
            {
                x.CreateMap<ServerLotDTO, Lot>();
                x.CreateMap<Lot, ServerLotDTO>();
            });

            lotMaper = lotconfig.CreateMapper();
            BuyerViewModel = new AuctionViewModelBuyer();
            BuyerViewModel.Name = name;
            BuyerViewModel.Password = password;
            this.DataContext = BuyerViewModel;
            stTest.DataContext = BuyerViewModel;
            ConnectionForBuyer();


        }

        private async void ConnectionForBuyer()
        {
            var tempBuyer = buyerMaper.Map<AuctionViewModelBuyer, ServerBuyerDTO>(BuyerViewModel);

            if( await ForBuyerClient.ConnectionForBuyerAsync(tempBuyer))
            {
                buyerWindowTitle.Title = BuyerViewModel.Name;
                var allLot = ForBuyerClient.GetAllProduct();
                foreach (var item in allLot)
                {

                var tempLot = lotMaper.Map<ServerLotDTO, Lot>(item);
                    BuyerViewModel.BuyerLots.Add(tempLot);

                }
                    lstAuction.ItemsSource = BuyerViewModel.BuyerLots;
            }
            else
            {
                this.Close();
            }

        }

        private void MakeBet_BtnClick(object sender, RoutedEventArgs e)
        {

        }

        private void DisconectBuyer(object sender, EventArgs e)
        {
            var tempBuyer = buyerMaper.Map<AuctionViewModelBuyer, ServerBuyerDTO>(BuyerViewModel);
            ForBuyerClient.DisconectionForBuyer(tempBuyer);
        }

        private void RebuyBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        public void ReturnBuyerCash(decimal cash)
        {
            BuyerViewModel.Cash=cash;
        }
    }
}
