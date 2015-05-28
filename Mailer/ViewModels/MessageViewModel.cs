using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Mailer.ViewModels
{
	internal class MessageViewModel : BaseViewModel
	{
		public String Subject { get; set; }

		public bool HasAttachments { get { return Attachments.Count > 0; } }

		public ObservableCollection<RecipientViewModel> Recipients { get; protected set; }

		public ObservableCollection<AttachmentViewModel> Attachments { get; protected set; }

		public MessageViewModel()
		{
			Recipients = new ObservableCollection<RecipientViewModel>();
			Attachments = new ObservableCollection<AttachmentViewModel>();
			Attachments.CollectionChanged += Attachments_CollectionChanged;
		}

		void Attachments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			OnPropertyChanged("HasAttachments");
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
