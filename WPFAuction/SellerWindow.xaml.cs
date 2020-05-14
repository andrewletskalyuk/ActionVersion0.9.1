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

namespace AuctionClient
{
    /// <summary>
    /// Interaction logic for SellerWindow.xaml
    /// </summary> //changes
    public partial class SellerWindow : Window
    {

        private int startPriceOfLot;
        ForSellerClient ForSellerClient;
        public AuctionViewModelSeller Sellerviewmodel { get; set; }
        public IMapper sellerMaper=null;
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
            ForSellerClient = new ForSellerClient();
            InitializeComponent();
            var sellerconfig= new MapperConfiguration(x =>
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

            Sellerviewmodel = new AuctionViewModelSeller() {Name=sellerName,Password=sellerPassword};
             this.DataContext = Sellerviewmodel;
            ConnectionForSeller();
        }
        /// <summary>
        /// ConnectionForSeller - add data for view, and make datasource for listbox
        /// </summary>
        private void ConnectionForSeller()
        {
            var tempSeller = sellerMaper.Map<AuctionViewModelSeller, ServerSellerDTO>(Sellerviewmodel);

            if(ForSellerClient.ConnectionForSeller(tempSeller))
            {
            sellerWindowTitle.Title = Sellerviewmodel.Name;
            }
            else
            {
                this.Close();
            }      
            //При конекшенні повертати лоти які уже є у селлера
           lbLots.ItemsSource = Sellerviewmodel.SellerLots;
        }

        private void btnChooseThPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files|*.png;*.jpeg;*.jpg";
            openFileDialog.ShowDialog();
            var sourceFilePath = openFileDialog.FileName;
            var destinantionFilePath = Directory.GetCurrentDirectory() +
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
                    BuyerName = "Just added product",
                    StartPrice = tempPriceLot,
                    SoldPrice = tempPriceLot,
                    Photo = imageForLot.Source.ToString()
                };

                var tempSeller = sellerMaper.Map<AuctionViewModelSeller, ServerSellerDTO>(Sellerviewmodel);
                var tempLot = lotMaper.Map<Lot , ServerLotDTO>(sellerLot);

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

        public void UpdateLot(ServerLotDTO lot)
        {
            throw new NotImplementedException();
        }
    }
}

