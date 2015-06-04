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
    public class EditMailingViewModelTest
    {
        [TestMethod]
        public void AddAddressIdTest()
        {

            var testAddress = new Address
            {
                Email = "asadsfasdfadsfdf@test.com",
                FirstName = "yayheythere",
                LastName = "demmetasdfadsfohyeah",
                ReceivedMails = new List<ReceivedMail>()
            };

            var testList = new MailingList
            {
                Name = "heythere",
                MailingListLines = new Collection<MailingListLine>()
                   
            };

            using (var db = new MailerEntities())
            {
                db.MailingLists.Add(testList);
                db.Addresses.Add(testAddress);
              
                db.SaveChanges();

                var testEavm = new EditMailingViewModel(testList);

                Assert.AreEqual(1, testEavm.AvailAddresses.Count(addr => addr.AddressID == testAddress.AddressID));

                testEavm.AddAddressId(testAddress.AddressID);

                Assert.AreEqual(1, testEavm.CurrAddresses.Count(addr => addr.AddressID == testAddress.AddressID));
                Assert.AreEqual(0, testEavm.AvailAddresses.Count(addr => addr.AddressID == testAddress.AddressID));

                db.MailingLists.Remove(testList);
                db.SaveChanges();

            }
        }

         [TestMethod]
        public void RemoveAddressIdTest()
        {

            var testAddress = new Address
            {
                Email = "asadsfasdfadsfdf@test.com",
                FirstName = "yayheythere",
                LastName = "demmetasdfadsfohyeah",
                ReceivedMails = new List<ReceivedMail>()
            };

            var testList = new MailingList
            {
                Name = "heythere",
                MailingListLines = new Collection<MailingListLine>()
                   
            };

            using (var db = new MailerEntities())
            {
                db.MailingLists.Add(testList);
                db.Addresses.Add(testAddress);
              
                db.SaveChanges();

                var testEavm = new EditMailingViewModel(testList);

                testEavm.AddAddressId(testAddress.AddressID);


                Assert.AreEqual(0, testEavm.AvailAddresses.Count(addr => addr.AddressID == testAddress.AddressID));

                testEavm.RemoveAddressId(testAddress.AddressID);

                Assert.AreEqual(0, testEavm.CurrAddresses.Count(addr => addr.AddressID == testAddress.AddressID));
                Assert.AreEqual(1, testEavm.AvailAddresses.Count(addr => addr.AddressID == testAddress.AddressID));

                db.MailingLists.Remove(testList);
                db.SaveChanges();

            }
        }

        [TestMethod]
        public void SaveTest()
        {
            var testList = new MailingList
            {
                Name = "TestDBSave",
            };

            using (var db = new MailerEntities())
            {
                db.MailingLists.Add(testList);
                db.SaveChanges();
                var TestEavm = new EditMailingViewModel(testList);

                TestEavm.Save();

                Assert.IsTrue(db.MailingLists.Any(ml => testList.Name.Equals(ml.Name) && testList.ListID == ml.ListID));

                db.MailingLists.Remove(testList);
                db.SaveChanges();
            }
        }
    }
}
