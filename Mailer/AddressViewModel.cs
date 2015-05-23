using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer
{
	public class AddressViewModel : BaseViewModel
	{
		public AddressViewModel(Address address)
		{
			Address = address;
		}

		public Address Address { get; private set; }


		public void Save()
		{
			using (var db = new MailerEntities())
			{
				db.Entry(Address).State = EntityState.Modified;
				db.SaveChanges();
			}
		}
	}
}
