using System;
using System.Linq;

namespace Mailer.ViewModels
{
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