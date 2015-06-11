using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailer.ViewModels;

namespace Mailer.DesignData
{
	internal class MockAttachmentViewModel : AttachmentViewModel
	{
		public MockAttachmentViewModel()
		{
			FileName = "CoolFile.png";
		}
	}
}
