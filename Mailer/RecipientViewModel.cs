using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mailer
{
	internal abstract class RecipientViewModel : BaseViewModel
	{
		public MessageViewModel Parent { get; set; }

		public string Display { get; set; }

		protected RecipientViewModel(MessageViewModel parent)
		{
			Parent = parent;
		}
	}

	internal class AddressRecipientViewModel : RecipientViewModel
	{
		public Address Address { get; set; }

		public AddressRecipientViewModel(MessageViewModel parent, Address address) : base(parent)
		{
			Address = address;
			Display = address.FirstName + " " + address.LastName;
		}
	}

	internal class MailingListRecipientViewModel : RecipientViewModel
	{
		public MailingList MailingList { get; set; }

		public MailingListRecipientViewModel(MessageViewModel parent, MailingList list)
			: base(parent)
		{
			MailingList = list;
			Display = list.Name;
		}
	}
}
