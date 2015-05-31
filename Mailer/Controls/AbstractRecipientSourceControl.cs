using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Mailer.ViewModels;

namespace Mailer.Controls
{
	/// <summary>
	///     Provides an abstract base for controls that host ViewModels that inherit from
	///     RecipientViewModel. Provides behavior to the control for it to act as a drag source.
	/// </summary>
	public abstract class AbstractRecipientSourceControl : UserControl
	{
		protected AbstractRecipientSourceControl()
		{
			MouseMove += AbstractRecipientSourceControl_MouseMove;
		}

		private void AbstractRecipientSourceControl_MouseMove(object sender, MouseEventArgs e)
		{
			var rsvm = DataContext as RecipientSourceViewModel;

			if (rsvm != null && e.LeftButton == MouseButtonState.Pressed)
			{
				var data = new DataObject();
				data.SetData("Object", rsvm.Recipient);
				DragDrop.DoDragDrop(this, data, DragDropEffects.Link);
			}
		}
	}
}
