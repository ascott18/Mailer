using System;
using System.Linq;
using System.Windows;
using Mailer.DesignData;
using Mailer.ViewModels;

namespace Mailer.Windows
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
