using System;
using System.Windows;
using WPFAuction;

namespace AuctionClient
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void EnterByBuyer_BtnClick(object sender, RoutedEventArgs e)
        {
            stpBuyerInfo.Visibility = Visibility.Visible;
            stpEnter.IsEnabled = false;
        }

        private void EnterBySeller_BtnClick(object sender, RoutedEventArgs e)
        {
            stpSellerInfo.Visibility = Visibility.Visible;
            stpEnter.IsEnabled = false;
        }

        private void Enter_BtnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                //int buyerCash = Int32.Parse(tbBuyerCash.Text);
                BuyerWindow window = new BuyerWindow(tbBuyerName.Text, tbBuyerCash.Text);
                window.Owner = this; //головне вікно - це.
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                window.Show();
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnForSeller_Click(object sender, RoutedEventArgs e)
        {
            //int sellerCash = Int32.Parse(tbSellerCash.Text);
            try
            {
                SellerWindow sellerWindow = new SellerWindow(tbSellerName.Text, tbSellerCash.Text);
                sellerWindow.Owner = this;
                sellerWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                sellerWindow.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
