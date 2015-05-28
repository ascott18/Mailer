using System;
using System.Linq;

namespace Mailer.ViewModels
{
	/// <summary>
	///     A RecipientViewModel with an Address entity as the represented recipient.
	/// </summary>
	internal class AddressRecipientViewModel : RecipientViewModel
	{
		public AddressRecipientViewModel(MessageViewModel parent, Address address) : base(parent)
		{
			Address = address;
			Display = address.FirstName + " " + address.LastName;
		}

		/// <summary>
		///     The Address entity that is the recipient represented by this RecipientViewModel.
		/// </summary>
		public Address Address { get; set; }
	}
}
