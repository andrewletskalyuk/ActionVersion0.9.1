using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFAuction.ViewModel
{
    public class AuctionViewModelSeller
    {
		public string Name { get; set; }
		public string Password { get; set; }

		private ObservableCollection<Lot> _sellerLots;
		public ObservableCollection<Lot> SellerLots
		{
			get { return _sellerLots; }
			set { _sellerLots = value; }
		}
		public AuctionViewModelSeller()
		{
			SellerLots = new ObservableCollection<Lot>();
		}
	}
}
