using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailer.ViewModels;

namespace Mailer.DesignData
{
	internal class MockAddressListViewModel : AddressListViewModel
	{
		public MockAddressListViewModel()
		{
			AddressListItemViewModels = new ObservableCollection<AddressListItemViewModel>
			{
				new MockAddressListItemViewModel(),
				new MockAddressListItemViewModel(),
				new MockAddressListItemViewModel()
			};
		}
	}
}
