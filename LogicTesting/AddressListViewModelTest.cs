using System;
using System.Linq;
using Mailer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mailer.ViewModels;

namespace LogicTesting
{
	[TestClass]
	public class AddressListViewModelTest
	{
		[TestMethod]
		public void TestSort()
		{
			var alvm = new AddressListViewModel();


			var alivm1 = new AddressListItemViewModel(new Address
			{
				FirstName = "Bob",
				// lowercase to make sure case is ignored - steverson would go before newbie if it wasn't
				LastName = "newbie",
				Email = "bob@gmail.com"
			});
			var alivm2 = new AddressListItemViewModel(new Address
			{
				FirstName = "Steve",
				// lowercase to make sure case is ignored - steverson would go after newbie if it wasn't
				LastName = "Steverson",
				Email = "steve@gmail.com"
			});

			// Insert them in backwards sorted order (S before N)
			alvm.AddAddressListItemViewModel(alivm2);
			alvm.AddAddressListItemViewModel(alivm1);

			// Check that they were automatically sorted - Newbie comes before Steverson 
			Assert.AreEqual(alivm1, alvm.AddressListItemViewModels[0]);
			Assert.AreEqual(alivm2, alvm.AddressListItemViewModels[1]);
		}


		[TestMethod]
		public void TestFilter()
		{
			var alvm = new AddressListViewModel();

			var alivm1 = new AddressListItemViewModel(new Address
			{
				FirstName = "Bob",
				LastName = "Newbie",
				Email = "bob@gmail.com"
			});
			var alivm2 = new AddressListItemViewModel(new Address
			{
				FirstName = "Steve",
				LastName = "steverson",
				Email = "steve@gmail.com"
			});

			alvm.AddAddressListItemViewModel(alivm1);
			alvm.AddAddressListItemViewModel(alivm2);

			alvm.Filter = "newb";
			Assert.AreSame(alivm1, alvm.AddressListItemViewModels.Single());

			alvm.Filter = "";
			Assert.AreEqual(2, alvm.AddressListItemViewModels.Count);

			alvm.Filter = "ERSON";
			Assert.AreSame(alivm2, alvm.AddressListItemViewModels.Single());

			alvm.Filter = "foo bar";
			Assert.IsFalse(alvm.AddressListItemViewModels.Any());
		}

		[TestMethod]
		public void TestLoadFromDatabase()
		{
			var alvm = new AddressListViewModel();

			var address = new Address
			{
				FirstName = "Bob",
				LastName = "Newbie",
				Email = "bob@gmail.com"
			};

			using (var db = new MailerEntities())
			{
				db.Addresses.Add(address);
				db.SaveChanges();
			}

			alvm.LoadFromDatabase();

			// Check that it was loaded.
			// Don't check that this is equal to 1, because there might be more
			// from the other tests
			Assert.IsTrue(alvm.AddressListItemViewModels.Count > 0);

			foreach (var addressListItemViewModel in alvm.AddressListItemViewModels)
			{
				addressListItemViewModel.Delete();
			}
		}


		[TestMethod]
		public void TestExternalDeletion()
		{
			var alvm = new AddressListViewModel();

			var address = new Address
			{
				FirstName = "Bob",
				LastName = "Newbie",
				Email = "bob@gmail.com"
			};

			using (var db = new MailerEntities())
			{
				db.Addresses.Add(address);
				db.SaveChanges();
			}

			var alivm1 = new AddressListItemViewModel(address);

			alvm.AddAddressListItemViewModel(alivm1);
			Assert.AreEqual(1, alvm.AddressListItemViewModels.Count);

			alivm1.Delete();
			Assert.AreEqual(0, alvm.AddressListItemViewModels.Count);

			using (var db = new MailerEntities())
			{
				// ensure that it was removed
				Assert.IsFalse(db.Addresses.Any(addr => addr.AddressID == address.AddressID));
			}
		}


		[TestMethod]
		public void TestRemoval()
		{
			var alvm = new AddressListViewModel();

			var address = new Address
			{
				FirstName = "Bob",
				LastName = "Newbie",
				Email = "bob@gmail.com"
			};


			var alivm1 = new AddressListItemViewModel(address);

			alvm.AddAddressListItemViewModel(alivm1);
			Assert.AreEqual(1, alvm.AddressListItemViewModels.Count);

			alvm.RemoveAddressListItemViewModel(alivm1);
			Assert.AreEqual(0, alvm.AddressListItemViewModels.Count);
		}
	}
}
