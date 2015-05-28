using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer
{
	class AttachmentViewModel
	{
		public MessageViewModel Parent { get; set; }

		public String FileName { get; set; }

		public AttachmentViewModel()
		{
			
		}

		public AttachmentViewModel(MessageViewModel parent)
		{
			Parent = parent;
		}

		public void Delete()
		{
			Parent.RemoveAttachment(this);
		}

	}
}
