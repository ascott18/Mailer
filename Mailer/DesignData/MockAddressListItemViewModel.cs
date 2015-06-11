using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailer.ViewModels;

namespace Mailer.DesignData
{
	internal class MockAddressListItemViewModel : AddressListItemViewModel
	{
		public MockAddressListItemViewModel()
			: base(new Address
			{
				Email = "test@google.com",
				FirstName = "Andrew",
				LastName = "Scott",
			})
		{
		}
	}
}
