using System;
using System.Collections.ObjectModel;
using System.Linq;
using Mailer;
using Mailer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicTesting
{
	[TestClass]
	public class AddressListItemViewModelTest
	{
		[TestMethod]
		public void PropertiesTest()
		{
			var addr = new Address
			{
				FirstName = "Bob",
				LastName = "Newbie",
				Email = "bob@gmail.com",
			};

			var alivm = new AddressListItemViewModel(addr);


			Assert.AreEqual("Bob", alivm.FirstName);
			Assert.AreEqual("Newbie", alivm.LastName);
			Assert.AreEqual("bob@gmail.com", alivm.Email);
			Assert.AreSame(addr, alivm.Address);
			Assert.AreSame(addr, alivm.Recipient);
		}


		[TestMethod]
		public void DeleteTest()
		{
			var ml = new MailingList
			{
				Name = "TestMailingList",
			};

			var newAddress = new Address
			{
				FirstName = "Bob",
				LastName = "Newbie",
				Email = "bob@gmail.com",
				ReceivedMails = new Collection<ReceivedMail>
				{
					new ReceivedMail{Year = 2013},
					new ReceivedMail{Year = 2014},
				}
			};

			var mll = new MailingListLine {
				Address = newAddress,
				MailingList = ml
			};


			using (var db = new MailerEntities())
			{
				db.Addresses.Add(newAddress);
				db.MailingLists.Add(ml);
				db.MailingListLines.Add(mll);
				db.SaveChanges();

				// check that the two receievedmails were added
				Assert.AreEqual(2, db.ReceivedMails.Count(rm => rm.AddressID == newAddress.AddressID));
			}

			var alivm = new AddressListItemViewModel(newAddress);

			var pumpFired = 0;
			MessagePump.OnMessage += (sender, msg) =>
			{
				if (msg == "AddressDeleted" && sender == alivm)
					pumpFired++;
			};


			alivm.Delete();

			Assert.AreEqual(1, pumpFired);

			using (var db = new MailerEntities())
			{
				// ensure that the database is empty once again
				Assert.IsFalse(db.Addresses.Any(addr => addr.AddressID == newAddress.AddressID));
				Assert.IsFalse(db.MailingListLines.Any(line => line.AddressID == newAddress.AddressID));
				Assert.IsFalse(db.ReceivedMails.Any(rm => rm.AddressID == newAddress.AddressID));

				// delete the mailing list
				db.MailingLists.Attach(ml);
				db.MailingLists.Remove(ml);
				db.SaveChanges();
			}
		}
	}
}
