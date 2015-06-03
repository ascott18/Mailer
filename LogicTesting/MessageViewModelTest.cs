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

            Assert.AreEqual(1, mvm.Recipients.Count);

        }

        [TestMethod]
        public void TestAddRecipientIndividual()
        {
            MessageViewModel mvm = new MessageViewModel();

            mvm.AddRecipient(new Address());

            Assert.AreEqual(1, mvm.Recipients.Count);

        }

        [TestMethod]
        public void TestRemoveRecipient()
        {
            MessageViewModel mvm = new MessageViewModel();
            
            Address addr = new Address();

            RecipientViewModel rvm = mvm.AddRecipient(addr);

            mvm.RemoveRecipient(rvm);

			Assert.AreEqual(0, mvm.Recipients.Count);
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
				StringAssert.Contains("Must input a From name.", exc.Message);
            }

            //check if there is an email
            try
            {
                fromName = "Bob";
                mvm.Send(fromName, fromEmail);
            }
            catch (ArgumentException exc)
            {
				StringAssert.Contains("Must input a From email.", exc.Message);
            }

            //check the subject
            try
			{
				fromEmail = "invalidemail";
                mvm.Send(fromName, fromEmail);
            }
            catch (ArgumentException exc)
            {
				StringAssert.Contains("Subject cannot be empty.", exc.Message);
            }

            //check if the body is empty.
            try
            {
                mvm.Subject = "Subject";
                mvm.Send(fromName, fromEmail);
            }
            catch (ArgumentException exc)
            {
				StringAssert.Contains("Body cannot be empty.", exc.Message);
			}

			// check for missing recipients
			try
			{
				mvm.Body = "Hello, friend!";
				mvm.Send(fromName, fromEmail);
			}
			catch (ArgumentException exc)
			{
				StringAssert.Contains("Must specify recipients.", exc.Message);
			}

            //check if the from address is valid
            try
            {
	            mvm.AddRecipient(new Address());
				fromEmail = "invalidemail";
                mvm.Send(fromName, fromEmail);
            }
            catch (ArgumentException exc)
            {
				StringAssert.Contains("From address is not valid.", exc.Message);
            }

			// Should succeed (recipient address is an empty address at this point).
			fromEmail = "nobody@google.com";
            mvm.Send(fromName, fromEmail);                
        }


    }
}
