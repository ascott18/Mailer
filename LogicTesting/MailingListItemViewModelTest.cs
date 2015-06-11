using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Migrations;
using System.Linq;
using Mailer;
using Mailer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicTesting
{
	[TestClass]
	public class MailingListItemViewModelTest
	{
		[TestMethod]
		public void TestDelete()
		{
			var testList = new MailingList
			{
				Name = "a",
				MailingListLines = new Collection<MailingListLine>
				{
					new MailingListLine
					{
						Address = new Address
						{
							Email = "asdf@test.com",
							FirstName = "yay",
							LastName = "demmetasdfadsf",
							ReceivedMails = new List<ReceivedMail>()
						}
					}
				}
			};
			using (var db = new MailerEntities())
			{
				db.MailingLists.Add(testList);
				db.SaveChanges();
				var mlivmTest = new MailingListItemViewModel(testList, true);

				mlivmTest.Delete();

				Assert.IsFalse(db.MailingLists.Any(ml => ml.ListID == testList.ListID));
			}
		}
	}
}
