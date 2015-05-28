using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailer.ViewModels;

namespace Mailer.DesignData
{
	class MockRecipientViewModel : RecipientViewModel
	{
		public MockRecipientViewModel() : base(null)
		{
			Display = "Amazing Recipient";
		}
	}
}
