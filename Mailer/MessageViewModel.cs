using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer
{
	internal class MessageViewModel : BaseViewModel
	{
		public String Subject { get; set; }

		public ObservableCollection<RecipientViewModel> Recipients { get; set; }

		public ObservableCollection<AttachmentViewModel> Attachments { get; set; }

		public MessageViewModel()
		{
			Recipients = new ObservableCollection<RecipientViewModel>();
			Attachments = new ObservableCollection<AttachmentViewModel>();
		}

		public void AddAttachment(string fileName)
		{
			Attachments.Add(new AttachmentViewModel(this)
			{
				FileName = fileName
			});
		}

		public void RemoveAttachment(AttachmentViewModel attachmentViewModel)
		{
			Attachments.Remove(attachmentViewModel);
		}

		public void Remove(RecipientViewModel vm)
		{
			Recipients.Remove(vm);
		}

		public void AddRecipient(Address address)
		{
			Recipients.Add(new AddressRecipientViewModel(this, address));
		}

		public void AddRecipient(MailingList list)
		{
			Recipients.Add(new MailingListRecipientViewModel(this, list));
		}
	}
}
