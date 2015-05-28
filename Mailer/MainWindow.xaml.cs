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
using Mailer.DesignData;

namespace Mailer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			Loaded += MainWindow_Loaded;
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			var vm = new AddressListViewModel();

			using (var db = new MailerEntities())
			{
				foreach (var address in db.Addresses)
				{
					vm.AddAddressListItemViewModel(new AddressListItemViewModel(address));
				}
			}

			AddressesItemsControl.DataContext = vm;





			var mvm = new SampleMessageViewModel();
			ComposePanel.DataContext = mvm;
		}

		private void AddAddressButton_OnClick(object sender, RoutedEventArgs e)
		{
			((AddressListViewModel)AddressesItemsControl.DataContext).Add();
		}
	}
}
