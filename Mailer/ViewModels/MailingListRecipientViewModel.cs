using System;
using System.Linq;

namespace Mailer.ViewModels
{
	/// <summary>
	///     A RecipientViewModel with a MailingList entity as the represented recipient.
	/// </summary>
	internal class MailingListRecipientViewModel : RecipientViewModel
	{
		public MailingListRecipientViewModel(MessageViewModel parent, MailingList list)
			: base(parent)
		{
			MailingList = list;
			Display = list.Name;
		}

		/// <summary>
		///     The MailingList entity that is the recipient represented by this RecipientViewModel.
		/// </summary>
		public MailingList MailingList { get; set; }
	}
}
