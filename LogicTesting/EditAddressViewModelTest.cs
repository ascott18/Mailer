using System;
using System.Linq;
using Mailer;
using Mailer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicTesting
{
	[TestClass]
	public class EditAddressViewModelTest
	{
		private Address MakeAddress()
		{
			return new Address
			{
				FirstName = "Bob",
				LastName = "Newbie",
				Email = "bob@gmail.com:"
			};
		}

		private Address MakeDatabaseAddress()
		{
			using (var db = new MailerEntities())
			{
				var addr = MakeAddress();
				db.Addresses.Add(addr);
				db.SaveChanges();

				return addr;
			}
		}


		[TestMethod]
		public void TestProperties()
		{
			var addr = MakeDatabaseAddress();

			var eavm = new EditAddressViewModel(addr);

			// These will not be the same address.
			// EditAddressViewModel re-retrives the address from the database
			// since it has to get the RecievedMails from it too.
			// So, just check that the name is the same.
			Assert.AreEqual(addr.FirstName, eavm.Address.FirstName);


			Assert.IsFalse(eavm.ReceivedMails.Any());
		}

		
		[TestMethod]
		public void TestAddYear()
		{
			var addr = MakeDatabaseAddress();

			var eavm = new EditAddressViewModel(addr);

			try
			{
				eavm.AddYear(2000);
				eavm.AddYear(2000);
				Assert.Fail("No execption thrown");
			}
			catch (Exception)
			{
				// Expected
			}


			// Year too old
			try
			{
				eavm.AddYear(1969);
				Assert.Fail("No execption thrown");
			}
			catch (Exception)
			{
				// Expected
			}


			// Year in future
			try
			{
				eavm.AddYear(DateTime.Now.Year + 10);
				Assert.Fail("No execption thrown");
			}
			catch (Exception)
			{
				// Expected
			}

			// there should be 1 from the duplicate address test
			Assert.AreEqual(1, eavm.ReceivedMails.Count);

			// and now there will be 2
			eavm.AddYear(2012);
			Assert.AreEqual(2, eavm.ReceivedMails.Count);

			using (var db = new MailerEntities())
			{
				// Check that they're there in the database.
				var addrFromDb = db.Addresses.Find(addr.AddressID);
				Assert.AreEqual(2, addrFromDb.ReceivedMails.Count);

				db.Addresses.Remove(addrFromDb);
				db.SaveChanges();
			}
			
			eavm.RemoveYear(2000);
			eavm.RemoveYear(2012);
		}

		[TestMethod]
		public void TestRemoveYear()
		{
			var addr = MakeDatabaseAddress();

			var eavm = new EditAddressViewModel(addr);

			eavm.AddYear(2000);
			Assert.AreEqual(1, eavm.ReceivedMails.Count);

			eavm.RemoveYear(2000);
			Assert.AreEqual(0, eavm.ReceivedMails.Count);

			using (var db = new MailerEntities())
			{
				var addrFromDb = db.Addresses.Find(addr.AddressID);
				Assert.AreEqual(0, addrFromDb.ReceivedMails.Count);

				db.Addresses.Remove(addrFromDb);
				db.SaveChanges();
			}
		}

		[TestMethod]
		public void TestSave()
		{
			var addr = MakeDatabaseAddress();

			var eavm = new EditAddressViewModel(addr);


			eavm.Address.FirstName = "";
			try
			{
				eavm.Save();
			}
			catch (Exception)
			{
				// expected
			}


			eavm.Address.FirstName = "Bob";


			foreach (var s in new[]
			{
				"",
				"bob",
				"@",
				".@",
				"123123",
			})
			{
				eavm.Address.Email = s;
				try
				{
					eavm.Save();
				}
				catch (Exception)
				{
					// expected
				}
			}

			eavm.Address.Email = "bob@gmail.com";

			eavm.Save();

			using (var db = new MailerEntities())
			{
				var addrFromDb = db.Addresses.Find(addr.AddressID);
				Assert.AreEqual("Bob", addrFromDb.FirstName);
				Assert.AreEqual("bob@gmail.com", addrFromDb.Email);

				db.Addresses.Remove(addrFromDb);
				db.SaveChanges();
			}
		}
	}
}
