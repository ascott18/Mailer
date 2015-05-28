using System;
using System.Linq;

namespace Mailer.ViewModels
{
	class AttachmentViewModel
	{
		public MessageViewModel Parent { get; set; }

		public String FileName { get; set; }

		public String FullPath { get; set; }

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
