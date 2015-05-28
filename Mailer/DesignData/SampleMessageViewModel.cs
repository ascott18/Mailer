using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.DesignData
{
	class SampleMessageViewModel : MessageViewModel
	{
		public SampleMessageViewModel()
		{
			Subject = "subjectLine";
			AddAttachment("tpsReport-5-27-2015.pdf");
			AddAttachment("C00lSCreensaver.exe");
		}
	}
}
