using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailer.ViewModels;

namespace Mailer.DesignData
{
	class SampleAddressListViewModel : AddressListViewModel
	{
		public SampleAddressListViewModel()
		{
			AddressViewModels = new ObservableCollection<AddressListItemViewModel>
			{
				new SampleAddressListItemViewModel(),
				new SampleAddressListItemViewModel(),
				new SampleAddressListItemViewModel()
			};
		}
	}
}
