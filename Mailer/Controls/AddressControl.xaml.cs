using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Mailer.ViewModels;

namespace Mailer.Controls
{
	/// <summary>
	/// Interaction logic for AddressControl.xaml
	/// </summary>
	public partial class AddressControl : UserControl
	{
		public AddressControl()
		{
			InitializeComponent();
		}

		private void Edit_OnClick(object sender, RoutedEventArgs e)
		{
			var vm = (AddressListItemViewModel)DataContext;
			vm.Edit();
		}

		private void Delete_OnClick(object sender, RoutedEventArgs e)
		{
			var res = MessageBox.Show(
				"Are you sure you wish to delete this address?",
				"Are you sure?",
				MessageBoxButton.YesNo);

			if (res != MessageBoxResult.Yes)
				return;

			var vm = (AddressListItemViewModel)DataContext;
			vm.Delete();
		}
	}
}
