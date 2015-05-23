using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Mailer
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
