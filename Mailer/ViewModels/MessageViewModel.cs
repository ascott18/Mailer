using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Mailer.Mail;
using System.IO;

namespace Mailer.ViewModels
{
	/// <summary>
	///     A ViewModel that represents an email message.
	/// </summary>
	internal class MessageViewModel : BaseViewModel
	{
		public MessageViewModel()
		{
			Recipients = new ObservableCollection<RecipientViewModel>();
			Attachments = new ObservableCollection<AttachmentViewModel>();
			Attachments.CollectionChanged += Attachments_CollectionChanged;
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
		///     The email address to put at the sender of the message.
		/// </summary>
		public String From { get; set; }

		/// <summary>
		///     Returns whether the Attachments collection is empty or not. Functions with
		///     INotifyPropertyChange.
		/// </summary>
		public bool HasAttachments
		{
			get { return Attachments.Count > 0; }
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

		/// <summary>
		///     Add an attachment with the specified fileName to the message.
		/// </summary>
		/// <param name="fileName"></param>
		public void AddAttachment(string fileName)
		{
			Attachments.Add(new AttachmentViewModel(this)
			{
				FileName = Path.GetFileName(fileName),
                FullPath = fileName
			});
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
		public void AddRecipient(Address address)
		{
			Recipients.Add(new AddressRecipientViewModel(this, address));
		}

		/// <summary>
		///     Add the specified MailingList entity as a recipient of this message.
		/// </summary>
		/// <param name="list">The MailingList entity to add as a recipient.</param>
		public void AddRecipient(MailingList list)
		{
			Recipients.Add(new MailingListRecipientViewModel(this, list));
		}

		public void Send()
		{
            
            try
            {
                var client = new Client("smtp.mailgun.org", 587)
                {
                    UserName = "mailer@mg.scotta.me",
                    Password = "rUvVxR7rvnzRQTT8q9jznHpgHcuMxx"
                };

                var message = new Mail.Message(client)
                {
                    Subject = Subject,
                    Body = Body,
                    From = new MailAddress(From)
                };

                foreach (var rvm in Recipients)
                {
                    if (rvm is AddressRecipientViewModel)
                        message.AddRecipient((rvm as AddressRecipientViewModel).Address);
                    else if (rvm is MailingListRecipientViewModel)
                        message.AddRecipient((rvm as MailingListRecipientViewModel).MailingList);
                }

                foreach (var attachment in Attachments)
                {
                    message.AddAttachment(attachment.FullPath);
                }

                if (Body == null)
                    throw new ArgumentException();

                message.Send();
            }
            catch (ArgumentException)
            {
                if (Body == null)
                    throw new ArgumentException("Body can not be empty.");
                else 
                    throw new ArgumentException("Must input a From address");
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("Invalid recipient address");
            }
            
			
		}
	}
}
