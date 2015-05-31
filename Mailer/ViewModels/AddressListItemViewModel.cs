using System;
using System.Linq;
using Mailer.Windows;

namespace Mailer.ViewModels
{
	/// <summary>
	///     A ViewModel that represents an Address belonging to an AddressListViewModel.
	/// </summary>
	public class AddressListItemViewModel : RecipientSourceViewModel
	{
		protected AddressListItemViewModel()
		{
		}

		/// <summary>
		///     Creates a new AddressListItemViewModel for the specified Address entity.
		/// </summary>
		/// <param name="address">The Address entity to create for.</param>
		public AddressListItemViewModel(Address address)
		{
			Address = address;
		}

		/// <summary>
		///     The Address entity that is the basis for this AddressListItemViewModel.
		/// </summary>
		public Address Address { get; private set; }

		public override object Recipient { get { return Address; } }

		/// <summary>
		///     A mirror of the FirstName property on the address. Works with INotifyPropertyChanged.
		/// </summary>
		public string FirstName
		{
			get { return Address.FirstName; }
		}

		/// <summary>
		///     A mirror of the LastName property on the address. Works with INotifyPropertyChanged.
		/// </summary>
		public string LastName
		{
			get { return Address.LastName; }
		}

		/// <summary>
		///     A mirror of the Email property on the address. Works with INotifyPropertyChanged.
		/// </summary>
		public string Email
		{
			get { return Address.Email; }
		}

		/// <summary>
		///     Open an EditAddress dialog for editing this address.
		/// </summary>
		public void Edit()
		{
			var addrWind = new EditAddress(new EditAddressViewModel(Address));
			addrWind.ShowDialog();

			using (var db = new MailerEntities())
			{
				Address = db.Addresses.Find(Address.AddressID);
			}

			OnPropertyChanged("FirstName");
			OnPropertyChanged("LastName");
			OnPropertyChanged("Email");

			MessagePump.Dispatch(this, "AddressChanged");
		}

		/// <summary>
		///     Delete this address from the database, and fire the Deleted event.
		/// </summary>
		public void Delete()
		{
			using (var db = new MailerEntities())
			{
				db.Addresses.Attach(Address);
				db.MailingListLines.RemoveRange(Address.MailingListLines);
				db.ReceivedMails.RemoveRange(Address.ReceivedMails);
				db.Addresses.Remove(Address);
				db.SaveChanges();
			}

			MessagePump.Dispatch(this, "AddressDeleted");
		}
	}
}
