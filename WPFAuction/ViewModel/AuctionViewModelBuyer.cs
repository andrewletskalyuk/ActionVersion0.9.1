using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFAuction.ViewModel
{
    public class AuctionViewModelBuyer
    {
		public decimal Cash { get; set; }
		public string Name { get; set; }
		public string Password { get; set; }

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
