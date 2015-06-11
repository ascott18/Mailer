using System;
using System.Linq;
using System.Windows.Media.Animation;

namespace Mailer.ViewModels
{
	/// <summary>
	///     A ViewModel that represents an email attachment belonging to a MessageViewModel.
	/// </summary>
	public class AttachmentViewModel : BaseViewModel
	{
		public AttachmentViewModel()
		{
		}

		/// <summary>
		///     Create an AttachmentViewModel with the specified parent.
		/// </summary>
		/// <param name="parent"></param>
		public AttachmentViewModel(MessageViewModel parent)
		{
			Parent = parent;
		}

		/// <summary>
		///     The MessageViewModel that this AttachmentViewModel belongs to.
		/// </summary>
		public MessageViewModel Parent { get; set; }

		/// <summary>
		///     The FileName (and path) of the file that is the attachment.
		/// </summary>
		public String FileName { get; set; }

		public String FullPath { get; set; }

		/// <summary>
		///     Remove this AttachmentViewModel from its parent MessageViewModel.
		/// </summary>
		public void Remove()
		{
			if (Parent == null)
				throw new InvalidOperationException(
					"Cannot remove an AttachmentViewModel that doesn't have a parent, or that has already been removed.");

			Parent.RemoveAttachment(this);
			Parent = null;
		}
	}
}
