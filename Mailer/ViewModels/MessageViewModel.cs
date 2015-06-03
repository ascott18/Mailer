using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Mailer.Mail;
using System.IO;

namespace Mailer.ViewModels
{
	/// <summary>
	///     A ViewModel that represents an email message.
	/// </summary>
	public class MessageViewModel : BaseViewModel
	{
		public MessageViewModel()
		{
			Recipients = new ObservableCollection<RecipientViewModel>();
			Attachments = new ObservableCollection<AttachmentViewModel>();
			Attachments.CollectionChanged += Attachments_CollectionChanged;
			Recipients.CollectionChanged += Recipients_CollectionChanged;

			Subject = "";
			Body = "";
		}

		/// <summary>
		///     The subject line of the email message.
		/// </summary>
		public String Subject { get; set; }

		/// <summary>
		///     The body text of the email message.
		/// </summary>
		public String Body { get; set; }

		/// <summary>
		///     Returns whether the Attachments collection is empty or not. Functions with
		///     INotifyPropertyChange.
		/// </summary>
		public bool HasAttachments
		{
			get { return Attachments.Count > 0; }
		}

		/// <summary>
		///     Returns whether the Recipients collection is empty or not. Functions with
		///     INotifyPropertyChange.
		/// </summary>
		public bool HasNoRecipients
		{
			get { return Recipients.Count == 0; }
		}

		/// <summary>
		///     The collection of RecipientViewModels that represent the recipients of this message.
		/// </summary>
		public ObservableCollection<RecipientViewModel> Recipients { get; protected set; }

		/// <summary>
		///     The collection of AttachmentViewModels that represent the attachments of this message.
		/// </summary>
		public ObservableCollection<AttachmentViewModel> Attachments { get; protected set; }

		private void Attachments_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			OnPropertyChanged("HasAttachments");
		}

		private void Recipients_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			OnPropertyChanged("HasNoRecipients");
		}

		/// <summary>
		///     Add an attachment with the specified path to the message.
		/// </summary>
		/// <param name="path"></param>
		public AttachmentViewModel AddAttachment(string path)
		{
			var avm = new AttachmentViewModel(this)
			{
				FileName = Path.GetFileName(path),
				FullPath = path
			};

			Attachments.Add(avm);

			return avm;
		}

		/// <summary>
		///     Removes the specified AttachmentViewModel from the message.
		/// </summary>
		/// <param name="attachmentViewModel">The AttachmentViewModel to remove.</param>
		public void RemoveAttachment(AttachmentViewModel attachmentViewModel)
		{
			Attachments.Remove(attachmentViewModel);
		}

		/// <summary>
		///     Remove the specified RecipientViewModel from the message.
		/// </summary>
		/// <param name="vm">The RecipientViewModel to remove.</param>
		public void RemoveRecipient(RecipientViewModel vm)
		{
			Recipients.Remove(vm);
		}

		/// <summary>
		///     Add the specified Address entity as a recpient of this message.
		/// </summary>
		/// <param name="address">The Address entity to add as a recpient.</param>
		public RecipientViewModel AddRecipient(Address address)
		{
            RecipientViewModel vm = new AddressRecipientViewModel(this, address);
			Recipients.Add(vm);
            return vm;
		}

		/// <summary>
		///     Add the specified MailingList entity as a recipient of this message.
		/// </summary>
		/// <param name="list">The MailingList entity to add as a recipient.</param>
		public RecipientViewModel AddRecipient(MailingList list)
		{
            RecipientViewModel vm = new MailingListRecipientViewModel(this, list);
			Recipients.Add(vm);
            return vm;
		}

		public void Send(string fromName, string fromEmail)
		{

			if (fromName == "")
				throw new ArgumentException("Must input a From name.");

			if (fromEmail == "")
                throw new ArgumentException("Must input a From email.");

			if (Subject == "")
				throw new ArgumentException("Subject cannot be empty.");

			if (Body == "")
				throw new ArgumentException("Body cannot be empty.");
            if (Recipients.Count == 0)
            {
                throw new ArgumentException("Must specify recipients.");
            }

			MailAddress fromAddress;
			try
			{
				fromAddress = new MailAddress(fromEmail, fromName);
			}
			catch (Exception)
			{
				throw new ArgumentException("From address is not valid.");
			}

			var client = new Client("smtp.mailgun.org", 587)
			{
				UserName = "mailer@mg.scotta.me",
				Password = "rUvVxR7rvnzRQTT8q9jznHpgHcuMxx"
			};

			var message = new Mail.Message(client)
			{
				Subject = Subject,
				Body = Body,
				From = fromAddress
			};

			foreach (var rvm in Recipients)
			{
				if (rvm is AddressRecipientViewModel)
				{
					message.AddRecipient((rvm as AddressRecipientViewModel).Address);
				}
				else if (rvm is MailingListRecipientViewModel)
				{
					var mailingList = (rvm as MailingListRecipientViewModel).MailingList;

					if (mailingList is DynamicMailingList)
						message.AddRecipient(mailingList as DynamicMailingList);
					else
						message.AddRecipient(mailingList);
				}
			}

			foreach (var attachment in Attachments)
			{
				message.AddAttachment(attachment.FullPath);
			}

			Task.Run(() =>
			{
				message.Send();
				MessageBox.Show("Sent!");


				Application.Current.Dispatcher.BeginInvoke(
					new Action(() => MessagePump.Dispatch(this, "MessageSent")),
					new object[0]
				);
			});
		}
	}
}
