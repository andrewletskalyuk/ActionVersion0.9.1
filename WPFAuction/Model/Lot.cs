using System.ComponentModel;

namespace WPFAuction.ViewModel
{
    public class Lot : INotifyPropertyChanged
    {
        //public TimeClass MyTimeClass { get; set; }
        
        private string _buyerName;
        public string BuyerName
        {
            get { return _buyerName; }
            set
            {
                _buyerName = value;
                OnPropertyChanged("BuyerName");
            }
        }

        // public int Id { get; set; }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private decimal _price;
        public decimal StartPrice
        {
            get { return _price; }
            set
            {
                _price = value;
                OnPropertyChanged("StartPrice");
            }
        }

        private decimal _soldPrice;
        public decimal SoldPrice
        {
            get { return _soldPrice; }
            set
            {
                _soldPrice = value;
                OnPropertyChanged("SoldPrice");
            }
        }

        private int _sellerId;
        public int SellerId
        {
            get { return _sellerId; }
            set
            {
                _sellerId = value;
                OnPropertyChanged("SellerId");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _photo;
        public string Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
                OnPropertyChanged("Photo");
            }
        }
        public Lot()
        {
            //MyTimeClass = new TimeClass();
        }
    }
}