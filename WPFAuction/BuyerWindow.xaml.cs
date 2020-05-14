using AuctionBLLService.DTOClasses;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class BuyerWindow : Window, IForBuyerCallback
    {
        ForBuyerClient ForBuyerClient;
        public AuctionViewModelBuyer BuyerViewModel { get; set; }
        public IMapper buyerMaper = null;
        public IMapper lotMaper = null;
        public BuyerWindow(string name, string password)
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


            TimerCheck();
        }

        private async void ConnectionForBuyer()
        {
            var tempBuyer = buyerMaper.Map<AuctionViewModelBuyer, ServerBuyerDTO>(BuyerViewModel);

            if (await ForBuyerClient.ConnectionForBuyerAsync(tempBuyer))
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


        public async void TimerCheck()
        {
           await Task.Run(() =>
              {
                  do
                  {
                      for (int i = 0; i < BuyerViewModel.BuyerLots.Count; i++)
                      {
                          if (BuyerViewModel.BuyerLots[i].MyTimeClass.IsEn)
                          {
                              if (BuyerViewModel.BuyerLots[i].IsSold == true)
                                  BuyerViewModel.BuyerLots[i].MyTimeClass.IsEn = false;
                              else
                              {
                                  
                                      BuyerViewModel.BuyerLots[i].IsSold = true;
                                      var tempBuyer = buyerMaper.Map<AuctionViewModelBuyer, ServerBuyerDTO>(BuyerViewModel);
                                      var tempLot = lotMaper.Map<Lot, ServerLotDTO>(BuyerViewModel.BuyerLots[i]);
                                      ForBuyerClient.BoughtLotAsync(tempBuyer, tempLot);
                                  
                              }
                          }

                      }


                  } while (true);
              }
                );
        }


        int indexLot;
        private async void MakeBet_BtnClick(object sender, RoutedEventArgs e)
        {
            indexLot = lstAuction.SelectedIndex;
            var selectLot = (lstAuction.SelectedItem as Lot);
            if (selectLot.IsSold == false)
            {
                var tempBuyer = buyerMaper.Map<AuctionViewModelBuyer, ServerBuyerDTO>(BuyerViewModel);
                var tempLot = lotMaper.Map<Lot, ServerLotDTO>(selectLot);
                decimal newPRice = Decimal.Parse(tbBet.Text);

                if (CheckBuyerBet(selectLot, tempBuyer, newPRice))
                    await ForBuyerClient.MakeBetAsync(tempLot, tempBuyer, newPRice);
                else
                    MessageBox.Show("You can`t make bet to lot");
            }
            else
            {
                MessageBox.Show("Lot is sold");
            }


        }

        private bool CheckBuyerBet(Lot selectLot, ServerBuyerDTO tempBuyer, decimal newPRice)
        {
            if (selectLot.StartPrice < newPRice && selectLot.BuyerName != tempBuyer.Name)
            {
                return true;
            }
            else
                return false;
        }

        private void DisconectBuyer(object sender, EventArgs e)
        {
            var tempBuyer = buyerMaper.Map<AuctionViewModelBuyer, ServerBuyerDTO>(BuyerViewModel);
            ForBuyerClient.DisconectionForBuyer(tempBuyer);
        }

        private async void RebuyBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tempBuyer = buyerMaper.Map<AuctionViewModelBuyer, ServerBuyerDTO>(BuyerViewModel);
                decimal newPRice = Decimal.Parse(tbRebuyCash.Text);
                await ForBuyerClient.AddCashForBuyerAsync(tempBuyer, newPRice);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Enter correct sum");
            }
        }

        public void ReturnBuyerCash(decimal cash)
        {

            BuyerViewModel.Cash = cash;

        }

        public void UpdateBuyerLots(ServerLotDTO allLots, ServerLotDTO[] selectedBuyersLots)
        {
            if (allLots != null)
            {
                var tempLot = lotMaper.Map<ServerLotDTO, Lot>(allLots);
                for (int i = 0; i < BuyerViewModel.BuyerLots.Count; i++)
                {
                    if (BuyerViewModel.BuyerLots[i].Name == allLots.Name)
                    {
                        BuyerViewModel.BuyerLots[i] = tempLot;
                        BuyerViewModel.MyTimerStart(i);
                    }
                }
            }
            if (selectedBuyersLots.Length>0)
            {
                ObservableCollection<Lot> selectedLot = new ObservableCollection<Lot>();
                foreach (ServerLotDTO item in selectedBuyersLots)
                {
                    Lot obj = new Lot() { Name = item.Name, StartPrice = item.StartPrice, Photo = item.Photo };
                    selectedLot.Add(obj);
                }
                BuyerViewModel.BuyerSelectedLots = selectedLot;
            }
            //lstBuyerLots.ItemsSource = BuyerViewModel.BuyerSelectedLots;
            lstAuction.ItemsSource = BuyerViewModel.BuyerLots;
        }

        public void UpdateBuyerBoughtLots(ServerLotDTO boughtLot, ServerLotDTO[] boughtBuyersLots)
        {
          

           

           
        }

        public void UpdateBuyerBoughtLots(ServerLotDTO boughtLot)
        {
            for (int i = 0; i < BuyerViewModel.BuyerLots.Count; i++)
            {
                if (BuyerViewModel.BuyerLots[i].Name == boughtLot.Name)
                {
                    BuyerViewModel.BuyerLots[i].IsSold = true;
                }
            }
            lstAuction.ItemsSource = BuyerViewModel.BuyerLots;
        }

        public void UpdateBuyerBoughtLotsAfter(ServerLotDTO[] boughtBuyersLots)
        {
            ObservableCollection<Lot> boughtLots = new ObservableCollection<Lot>();
            foreach (ServerLotDTO item in boughtBuyersLots)
            {
                Lot obj = new Lot() { Name = item.Name, StartPrice = item.StartPrice, Photo = item.Photo };
                boughtLots.Add(obj);
            }
            BuyerViewModel.BuyerBoughtLots = boughtLots;
            lstBoughtLots.ItemsSource = BuyerViewModel.BuyerBoughtLots;
        }
    }
}
