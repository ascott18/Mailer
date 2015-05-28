using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailer.ViewModels;

namespace Mailer.DesignData
{
	class MockMessageViewModel : MessageViewModel
	{
		public MockMessageViewModel()
		{
			Subject = "subjectLine";
			AddAttachment("tpsReport-5-27-2015.pdf");
			AddAttachment("C00LSCreensaver.exe");

			AddRecipient(new Address
			{
				FirstName = "Bob",
				LastName = "Newbie",
				Email = "Bob@zombo.com"
			});
			AddRecipient(new Address
			{
				FirstName = "Andrew",
				LastName = "Scott",
				Email = "ascott18@gmail.com"
			});
			AddRecipient(new MailingList
			{
				Name = "Bob's cool friends",
			});
		}
	}
}
