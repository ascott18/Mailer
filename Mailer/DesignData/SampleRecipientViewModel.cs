using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailer.ViewModels;

namespace Mailer.DesignData
{
	class SampleRecipientViewModel : RecipientViewModel
	{
		public SampleRecipientViewModel() : base(null)
		{
			Display = "Amazing Recipient";
		}
	}
}
