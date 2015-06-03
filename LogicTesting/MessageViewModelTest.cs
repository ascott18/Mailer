using System;
using Mailer.Mail;
using Mailer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mailer;

namespace LogicTesting
{
    [TestClass]
    public class MessageViewModelTest
    {
        [TestMethod]
        public void TestAddRecipientMailingList()
        {
            MessageViewModel mvm = new MessageViewModel();
            
            mvm.AddRecipient(new MailingList());

            Assert.AreEqual(mvm.Recipients.Count, 1);

        }

        [TestMethod]
        public void TestAddRecipientIndividual()
        {
            MessageViewModel mvm = new MessageViewModel();

            mvm.AddRecipient(new Address());

            Assert.AreEqual(mvm.Recipients.Count, 1);

        }

        [TestMethod]
        public void TestRemoveRecipient()
        {
            MessageViewModel mvm = new MessageViewModel();
            
            Address addr = new Address();

            RecipientViewModel rvm = mvm.AddRecipient(addr);

            mvm.RemoveRecipient(rvm);
        }

        [TestMethod]
        public void TestSendMessage()
        {
            MessageViewModel mvm = new MessageViewModel();

            //everything is invalid
            string fromName = "";
            string fromEmail = "";
            mvm.Subject = "";
            mvm.Body = "";

            //check for name
            try
            {
                mvm.Send(fromName, fromEmail);
            }
            catch (ArgumentException exc)
            {
                StringAssert.Contains(exc.Message, "Must input a From name.");
            }

            //check if there is an email
            try
            {
                fromName = "Bob";
                mvm.Send(fromName, fromEmail);
            }
            catch (ArgumentException exc)
            {
                StringAssert.Contains(exc.Message, "Must input a From email.");
            }

            //check the subject
            try
            {
                fromEmail = "invalidemail";
                mvm.Send(fromName, fromEmail);
            }
            catch (ArgumentException exc)
            {
                StringAssert.Contains(exc.Message, "Subject cannot be empty.");
            }

            //check if the body is empty.
            try
            {
                mvm.Subject = "Subject";
                mvm.Send(fromName, fromEmail);
            }
            catch (ArgumentException exc)
            {
                StringAssert.Contains(exc.Message, "Body cannot be empty.");
            }

            //check if the from address is valid
            try
            {
                mvm.Body = "Hello, friend!";
                mvm.Send(fromName, fromEmail);
            }
            catch (ArgumentException exc)
            {
                StringAssert.Contains(exc.Message, "From address is not valid.");
            }

            try
            {
                fromEmail = "bnewbie@google.com";
                mvm.Send(fromName, fromEmail);
            }
            catch (ArgumentException exc)
            {
                StringAssert.Contains(exc.Message, "Must specify recipients.");
            }

            mvm.Send(fromName, fromEmail);                
        }


    }
}
