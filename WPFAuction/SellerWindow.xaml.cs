using AuctionBLLService;
using AuctionBLLService.DTOClasses;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFAuction.ViewModel;
using WPFAuction.ServiceReferenceSeller;
using AutoMapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AuctionClient
{
    /// <summary>
    /// Interaction logic for SellerWindow.xaml
    /// </summary> //changes
    public partial class SellerWindow : Window, IForSellerCallback
    {

        private int startPriceOfLot;
        ForSellerClient ForSellerClient;
        public AuctionViewModelSeller Sellerviewmodel { get; set; }
        public IMapper sellerMaper = null;
        public IMapper lotMaper = null;
        //public SellerWindow()
        //{
        //    viewmodel = new AuctionViewModel();
        //    InitializeComponent();
        //    sellerName = "NoName";
        //    sellerCash = 0;
        //    lbLots.ItemsSource = viewmodel.ClientLots; 
        //}

        /// <summary>
        /// Start window with parameters
        /// </summary>
        /// <param name="sellerName"></param>
        /// <param name="sellerCash"></param>
        public SellerWindow(string sellerName, string sellerPassword)
        {
            ForSellerClient = new ForSellerClient(new System.ServiceModel.InstanceContext(this));
            InitializeComponent();
            var sellerconfig = new MapperConfiguration(x =>
             {
                 x.CreateMap<ServerSellerDTO, AuctionViewModelSeller>();
                 x.CreateMap<AuctionViewModelSeller, ServerSellerDTO>();
             });
            sellerMaper = sellerconfig.CreateMapper();

            var lotconfig = new MapperConfiguration(x =>
            {
                x.CreateMap<ServerLotDTO, Lot>();
                x.CreateMap<Lot, ServerLotDTO>();
            });

            lotMaper = lotconfig.CreateMapper();

            Sellerviewmodel = new AuctionViewModelSeller() { Name = sellerName, Password = sellerPassword };
            this.DataContext = Sellerviewmodel;
            Connection();
        }
        /// <summary>
        /// ConnectionForSeller - add data for view, and make datasource for listbox
        /// </summary>
        private async void Connection()
        {
            var tempSeller = sellerMaper.Map<AuctionViewModelSeller, ServerSellerDTO>(Sellerviewmodel);

            if (await ForSellerClient.ConnectionForSellerAsync(tempSeller))
            {
                sellerWindowTitle.Title = Sellerviewmodel.Name;
            }
            else
            {
                this.Close();
            }
            //При конекшенні повертати лоти які уже є у селлера
            // lbLots.ItemsSource = Sellerviewmodel.SellerLots;
        }

        private void btnChooseThPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.png;*.jpeg;*.jpg";
            openFileDialog.ShowDialog();
            var sourceFilePath = openFileDialog.FileName;
            string destinantionFilePath;
            if (!Directory.Exists((Directory.GetCurrentDirectory() +
                                        $"\\ImagesForLots")))
                Directory.CreateDirectory("ImagesForLots");

            destinantionFilePath = Directory.GetCurrentDirectory() +
                                        $"\\ImagesForLots\\" +
                                        System.IO.Path.GetFileName(sourceFilePath);
            File.Copy(sourceFilePath, destinantionFilePath, true);

            Image lotImage = new Image();
            //lotImage.Width = 100;
            //lotImage.Height = 100;

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(destinantionFilePath);
            bitmapImage.EndInit();

            lotImage.Source = bitmapImage;

            imageForLot.Source = bitmapImage;
        }

        private void btnCreateNewLot_Click(object sender, RoutedEventArgs e)
        {
            if (IsCorrectDataNewProduct(tbNameProduct.Text, tbNameProductStartPrice.Text, imageForLot.Source))
            {
                var tempPriceLot = Int32.Parse(tbNameProductStartPrice.Text);

                Lot sellerLot = new Lot()
                {
                    Name = tbNameProduct.Text,
                    BuyerName = "No buyer",
                    StartPrice = tempPriceLot,
                    //SoldPrice = tempPriceLot,
                    Photo = imageForLot.Source.ToString()
                };
                var tempLot = lotMaper.Map<Lot, ServerLotDTO>(sellerLot);
                var tempSeller = sellerMaper.Map<AuctionViewModelSeller, ServerSellerDTO>(Sellerviewmodel);
                //   var tempSeller = sellerMaper.Map<AuctionViewModelSeller, ServerSellerDTO>(Sellerviewmodel);

                ForSellerClient.AddLot(tempSeller, tempLot);

                Sellerviewmodel.SellerLots.Add(sellerLot);


                //lbLots.ItemsSource = Sellerviewmodel.SellerLots;
            }
            else
            {
                MessageBox.Show("Incorrect data's put in");
            }
        }

        /// <summary>
        /// Check out for correct data which putting in
        /// </summary>
        /// <param name="nameProduct"></param>
        /// <param name="startPrice"></param>
        /// <param name="sourceOfProduct"></param>
        /// <returns></returns>
        private bool IsCorrectDataNewProduct(string nameProduct, string startPrice, ImageSource sourceOfProduct)
        {
            if (!String.IsNullOrEmpty(nameProduct)
                && Int32.TryParse(startPrice, out startPriceOfLot)
                && sourceOfProduct != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void ReturnSellerLot(ServerLotDTO[] lots)
        {
            ObservableCollection<Lot> lots1 = new ObservableCollection<Lot>();
            foreach (ServerLotDTO item in lots)
            {
                lots1.Add(lotMaper.Map<ServerLotDTO, Lot>(item));
            }
            Sellerviewmodel.SellerLots = lots1;
            lbLots.ItemsSource = Sellerviewmodel.SellerLots;
        }
    }
}

