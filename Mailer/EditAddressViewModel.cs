using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer
{
	public class EditAddressViewModel : BaseViewModel
	{
		public EditAddressViewModel(Address address)
		{
			using (var db = new MailerEntities())
			{
				Address = db.Addresses.Find(address.AddressID);
				ReceivedMails = new ObservableCollection<ReceivedMail>(Address.ReceivedMails);
			}
		}

		public Address Address { get; private set; }

		public ObservableCollection<ReceivedMail> ReceivedMails { get; private set; }

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

		public void RemoveYear(int year)
		{
			using (var db = new MailerEntities())
			{
				db.Addresses.Attach(Address);

				var rmToRemove = ReceivedMails.First(rm => rm.Year == year);
				Address.ReceivedMails.Remove(rmToRemove);
				ReceivedMails.Remove(rmToRemove);
				db.SaveChangesAsync();
			}
		}

		public void Save()
		{
			using (var db = new MailerEntities())
			{
				db.Addresses.Attach(Address);
				db.Entry(Address).State = EntityState.Modified;
				db.SaveChanges();
			}
		}
	}
}
