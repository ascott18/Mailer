using System;
using System.Linq;

namespace Mailer.ViewModels
{
	/// <summary>
	///     An abstract base for ViewModels that represent a recipient of a message that the user is
	///     composing.
	/// </summary>
	public abstract class RecipientViewModel : BaseViewModel
	{
		/// <summary>
		///     Create a new RecipientViewModel with the given owner.
		/// </summary>
		/// <param name="parent">The MessageViewModel that owns this RecipientViewModel.</param>
		protected RecipientViewModel(MessageViewModel parent)
		{
			Parent = parent;
		}

		/// <summary>
		///     The MessageViewModel that owns this RecipientViewModel.
		/// </summary>
		public MessageViewModel Parent { get; set; }

		/// <summary>
		///     The text that represents the recipient to the user.
		/// </summary>
		public string Display { get; set; }

		/// <summary>
		///     Remove this RecipientViewModel from its parent.
		/// </summary>
		public void Remove()
		{
			if (Parent == null)
				throw new InvalidOperationException(
					"Cannot remove a RecipientViewModel that doesn't have a parent, or that has already been removed.");

			Parent.RemoveRecipient(this);
			Parent = null;
		}
	}
}
