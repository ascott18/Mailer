using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using Mailer.DesignData;
using Mailer.ViewModels;
using Microsoft.Win32;
using Mailer.Controls;
using System.IO;

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


            var alvm = new AddressListViewModel();
            var mlvm = new MailingListViewModel();

			using (var db = new MailerEntities())
			{
				foreach (var address in db.Addresses)
				{
					alvm.AddAddressListItemViewModel(new AddressListItemViewModel(address));
				}

                foreach (var mlist in db.MailingLists)
                {
                    mlvm.AddMailingListItemViewModel(new MailingListItemViewModel(mlist));
			}
			}

            AddressDockPanel.DataContext = alvm;
            MailingListDockPanel.DataContext = mlvm;



			var mvm = new MockMessageViewModel();
			ComposePanel.DataContext = mvm;
		}

		private void AddAddressButton_OnClick(object sender, RoutedEventArgs e)
		{
			((AddressListViewModel)AddressDockPanel.DataContext).Add();
		}

		private void Send_OnClick(object sender, RoutedEventArgs e)
		{
            try
            {
                var mvm = ComposePanel.DataContext as MockMessageViewModel;

                if (mvm != null)
                {
                    mvm.Send();
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
		}

	    private void AddMailingListButton_OnClick(object sender, RoutedEventArgs e)
	    {
            ((MailingListViewModel)MailingListDockPanel.DataContext).Add();
	    }


        private void AddAttachment_OnClick(object sender, RoutedEventArgs e)
        {
            
            try
            {
                var mvm = ComposePanel.DataContext as MockMessageViewModel;

                if (mvm != null)
                {
                    OpenFileDialog ofd = new OpenFileDialog();

                    if (ofd.ShowDialog() == true)
                    {
                        mvm.AddAttachment(ofd.FileName);
                    }
                }
                    
            }
            catch (Exception exc) 
            {
                MessageBox.Show(exc.Message);
            }
             
        }
	}
}
