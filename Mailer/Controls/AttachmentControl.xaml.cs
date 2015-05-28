using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Mailer.ViewModels;

namespace Mailer.Controls
{
	/// <summary>
	/// Interaction logic for AttachmentControl.xaml
	/// </summary>
	public partial class AttachmentControl : UserControl
	{
		public AttachmentControl()
		{
			InitializeComponent();
		}

		private void Delete_OnClick(object sender, RoutedEventArgs e)
		{
			var vm = DataContext as AttachmentViewModel;
			if (vm != null)
			{
				vm.Remove();
			}
		}
	}
}
