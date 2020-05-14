using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFAuction.ViewModel
{
    public class AuctionViewModelBuyer:INotifyPropertyChanged
    {

		public string Name { get; set; }
		public string Password { get; set; }
		private decimal _cash;
		public decimal Cash
		{
			get { return _cash; }
			set
			{
				_cash = value;
				PropertyChanger("Cash");
			}
		}
		private ObservableCollection<Lot> _buyerLots;

		public event PropertyChangedEventHandler PropertyChanged;
		public void PropertyChanger(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));

		}
		public ObservableCollection<Lot> BuyerLots
		{
			get { return _buyerLots; }
			set { _buyerLots = value; }
		}
		public AuctionViewModelBuyer()
		{
			BuyerLots = new ObservableCollection<Lot>();
		}
	}
}
