using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mailer.ViewModels;

namespace LogicTesting
{
	[TestClass]
	public class AttachmentViewModelTest
	{
		[TestMethod]
		public void TestProperties()
		{
			var mvm = new MessageViewModel();

			var avm = mvm.AddAttachment("c:/test/test.txt");

			Assert.AreEqual("test.txt", avm.FileName);
			Assert.AreEqual("c:/test/test.txt", avm.FullPath);
		}

		[TestMethod]
		public void TestRemove()
		{
			var mvm = new MessageViewModel();

			var avm = mvm.AddAttachment("c:/test/test.txt");
			Assert.AreEqual(1, mvm.Attachments.Count);

			avm.Remove();

			Assert.AreEqual(0, mvm.Attachments.Count);
		}
	}
}
