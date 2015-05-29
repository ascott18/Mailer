using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace Mailer.ViewModels
{
	/// <summary>
	///     A ViewModel that represents an Address currently being edited.
	/// </summary>
	public class EditAddressViewModel : BaseViewModel
	{
		/// <summary>
		///     Create a new EditAddressViewModel for the given Address entity. A database connection will be
		///     established to retrieve any missing data.
		/// </summary>
		/// <param name="address">The Address entity being edited.</param>
		public EditAddressViewModel(Address address)
		{
			using (var db = new MailerEntities())
			{
				Address = db.Addresses.Find(address.AddressID);
				ReceivedMails = new ObservableCollection<ReceivedMail>(Address.ReceivedMails);
			}
		}

		/// <summary>
		///     The Address entity being edited.
		/// </summary>
		public Address Address { get; private set; }

		/// <summary>
		///     A locally cached version of RecievedMail entities that correspond to the Address entity
		///     for which this EditAddressViewModel was created.
		/// </summary>
		public ObservableCollection<ReceivedMail> ReceivedMails { get; private set; }

		/// <summary>
		///     Add a RecievedMail entity to the database corresponding to the given year and the Address
		///     entity for which this EditAddressViewModel was created. Throws an InvalidOperationException if
		///     the given year already exists in the database.
		/// </summary>
		/// <param name="year">The year to create a RecievedMail entity for in the database.</param>
		public void AddYear(int year)
		{
			if (ReceivedMails.Any(rm => rm.Year == year))
				throw new InvalidOperationException("Year already exists!");

			using (var db = new MailerEntities())
			{
				db.Addresses.Attach(Address);
				var newRm = new ReceivedMail
				{
					Year = year
				};
				Address.ReceivedMails.Add(newRm);
				ReceivedMails.Add(newRm);
				db.SaveChangesAsync();
			}
		}

		/// <summary>
		///     Remove the RecievedMail entity from the database corresponding to the given year and the
		///     Address entity for which this EditAddressViewModel was created. Will throw an exception if the
		///     year is not found in the database.
		/// </summary>
		/// <param name="year">The year to remove the RecievedMail entity for from the database.</param>
		public void RemoveYear(int year)
		{
			using (var db = new MailerEntities())
			{
				db.Addresses.Attach(Address);

				var rmToRemove = ReceivedMails.Single(rm => rm.Year == year);
				Address.ReceivedMails.Remove(rmToRemove);
				ReceivedMails.Remove(rmToRemove);
				db.SaveChangesAsync();
			}
		}

		/// <summary>
		///     Save the address to the database. Note that RecievedMail entities are saved as they are created
		///     and removed by their correponding methods. This method only saves the address itself - names
		///     and email.
		/// </summary>
		public void Save()
		{
            if (Address.FirstName == "")
                throw new ArgumentException("First name must not be empty");
            else if (Address.Email == "")
                throw new ArgumentException("Email address must not be empty");

			using (var db = new MailerEntities())
			{
				db.Addresses.Attach(Address);
				db.Entry(Address).State = EntityState.Modified;
				db.SaveChanges();

               
			}
		}
	}
}
