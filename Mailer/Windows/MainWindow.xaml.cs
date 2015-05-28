using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
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





			var mvm = new MockMessageViewModel();
			ComposePanel.DataContext = mvm;
		}

		private void AddAddressButton_OnClick(object sender, RoutedEventArgs e)
		{
			((AddressListViewModel)AddressesItemsControl.DataContext).Add();
		}

		private void Send_OnClick(object sender, RoutedEventArgs e)
		{
			var mvm = ComposePanel.DataContext as MockMessageViewModel;

			if (mvm != null)
			{
				mvm.Send();
			}
		}
	}
}
