using System;
using System.Linq;

namespace Mailer.ViewModels
{
	internal abstract class RecipientViewModel : BaseViewModel
	{
		public MessageViewModel Parent { get; set; }

		public string Display { get; set; }

		protected RecipientViewModel(MessageViewModel parent)
		{
			Parent = parent;
		}
	}
}
