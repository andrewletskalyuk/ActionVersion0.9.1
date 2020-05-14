using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

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
		public void MyTimerStart(int value)
		{
			var timer = new DispatcherTimer();
			timer.Tick += new EventHandler(BuyerLots[value].MyTimeClass.counterOne_Tick);
			timer.Interval = new TimeSpan(0, 0, 1);
			timer.Start();


		}
		public event PropertyChangedEventHandler PropertyChanged;
		public void PropertyChanger(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		private ObservableCollection<Lot> _buyerBoughtLots;
		public ObservableCollection<Lot> BuyerBoughtLots
		{
			get { return _buyerBoughtLots; }
			set { _buyerBoughtLots = value; }
		}
		private ObservableCollection<Lot> _buyerSelectedLots;
		public ObservableCollection<Lot> BuyerSelectedLots
		{
			get { return _buyerSelectedLots; }
			set { _buyerSelectedLots = value; }
		}
		private ObservableCollection<Lot> _buyerLots;
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
