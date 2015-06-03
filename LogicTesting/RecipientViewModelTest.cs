using System;
using System.Collections.ObjectModel;
using Mailer;
using Mailer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicTesting
{
	[TestClass]
	public class RecipientViewModelTest
	{
		[TestMethod]
		public void TestAddress()
		{
			var mvm = new MessageViewModel();

			var addr = new Address
			{
				FirstName = "bob",
				LastName = "newbie",
				Email = "bob@gmail.com"
			};

			var rvm = mvm.AddRecipient(addr);

			Assert.AreEqual(1, mvm.Recipients.Count);
			Assert.AreEqual("bob newbie", rvm.Display);

			rvm.Remove();

			Assert.AreEqual(0, mvm.Recipients.Count);
		}

		[TestMethod]
		public void TestMailingList()
		{
			var mvm = new MessageViewModel();


			var list = new MailingList
			{
				Name = "TestList",
				MailingListLines = new Collection<MailingListLine>
				{
					new MailingListLine
					{
						Address = new Address
						{
							FirstName = "bob",
							LastName = "newbie",
							Email = "bob@gmail.com"
						}
					},
					new MailingListLine
					{
						Address = new Address
						{
							FirstName = "betty",
							LastName = "newbie",
							Email = "betty@gmail.com"
						}
					}
				}
			};

			var rvm = mvm.AddRecipient(list);

			Assert.AreEqual(1, mvm.Recipients.Count);
			Assert.AreEqual("TestList", rvm.Display);

			rvm.Remove();

			Assert.AreEqual(0, mvm.Recipients.Count);
		}
	}
}
