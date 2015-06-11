using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Mailer;
using Mailer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicTesting
{
	[TestClass]
	public class MailingListViewModelTest
	{
		[TestMethod]
		public void AddMailingListItemViewModelTest()
		{
			var mlvmTest = new MailingListViewModel();


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

			var mlivmTemp = new MailingListItemViewModel(testList, true);

			mlvmTest.AddMailingListItemViewModel(mlivmTemp);
			Assert.IsTrue(mlvmTest.MailingListItemViewModels.Contains(mlivmTemp));
		}


		[TestMethod]
		public void MessagePumpTest()
		{
			var mlvmTest = new MailingListViewModel();

			using (var db = new MailerEntities())
			{
				var mlist = new MailingList
				{
					Name = "TestList"
				};
				db.MailingLists.Add(mlist);
				db.SaveChanges();


				var vm = new MailingListItemViewModel(mlist, true);
				mlvmTest.AddMailingListItemViewModel(vm);
				vm.Delete();

				Assert.IsFalse(mlvmTest.MailingListItemViewModels.Contains(vm));
			}
		}
	}
}
