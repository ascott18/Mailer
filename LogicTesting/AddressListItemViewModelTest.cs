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


			Assert.AreEqual(alivm.FirstName, "Bob");
			Assert.AreEqual(alivm.LastName, "Newbie");
			Assert.AreEqual(alivm.Email, "bob@gmail.com");
			Assert.AreSame(alivm.Address, addr);
			Assert.AreSame(alivm.Recipient, addr);
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
					new ReceivedMail{Year = 2015},
					new ReceivedMail{Year = 2016},
				}
			};

			var mll = new MailingListLine {
				Address = newAddress,
				MailingList = ml
			};


			using (var db = new MailerEntities())
			{
				// ensure that the database is empty
				Assert.IsFalse(db.Addresses.Any());
				Assert.IsFalse(db.MailingLists.Any());
				Assert.IsFalse(db.MailingListLines.Any());
				Assert.IsFalse(db.ReceivedMails.Any());

				db.Addresses.Add(newAddress);
				db.MailingLists.Add(ml);
				db.MailingListLines.Add(mll);
				db.SaveChanges();

				// check that the two receievedmails were added
				Assert.AreEqual(db.ReceivedMails.Count(), 2);
			}

			var alivm = new AddressListItemViewModel(newAddress);

			var pumpFired = 0;
			MessagePump.OnMessage += (sender, msg) =>
			{
				if (msg == "AddressDeleted" && sender == alivm)
					pumpFired++;
			};


			alivm.Delete();

			Assert.AreEqual(pumpFired, 1);

			using (var db = new MailerEntities())
			{
				// ensure that the database is empty once again
				Assert.IsFalse(db.Addresses.Any());
				Assert.IsFalse(db.MailingListLines.Any());
				Assert.IsFalse(db.ReceivedMails.Any());

				// delete the mailing list
				db.MailingLists.Attach(ml);
				db.MailingLists.Remove(ml);
				db.SaveChanges();

				Assert.IsFalse(db.MailingLists.Any());
			}
		}
	}
}
