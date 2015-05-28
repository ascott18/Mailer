using System;
using System.Linq;

namespace Mailer.ViewModels
{
	internal class AddressRecipientViewModel : RecipientViewModel
	{
		public Address Address { get; set; }

		public AddressRecipientViewModel(MessageViewModel parent, Address address) : base(parent)
		{
			Address = address;
			Display = address.FirstName + " " + address.LastName;
		}
	}
}