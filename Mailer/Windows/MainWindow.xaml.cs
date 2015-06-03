using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
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
			MessagePump.OnMessage += MessagePump_OnMessage;
		}

		void MessagePump_OnMessage(object sender, string msg)
		{
			if (msg == "MessageSent")
				ComposePanel.DataContext = new MessageViewModel();
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			var mlvm = new MailingListViewModel();
			mlvm.LoadFromDatabase();
			MailingListDockPanel.DataContext = mlvm;


			var alvm = new AddressListViewModel();
			alvm.StartAutoUpdating();
			AddressDockPanel.DataContext = alvm;


			ComposePanel.DataContext = new MessageViewModel();
		}

		private void AddAddressButton_OnClick(object sender, RoutedEventArgs e)
		{
			((AddressListViewModel)AddressDockPanel.DataContext).Add();
		}

		private void Send_OnClick(object sender, RoutedEventArgs e)
		{
            try
            {
                var mvm = ComposePanel.DataContext as MessageViewModel;

                if (mvm != null)
                {
					mvm.Send(Properties.Settings.Default["FromName"] as string,
						Properties.Settings.Default["FromEmail"] as string);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                var mvm = ComposePanel.DataContext as MessageViewModel;

                if (mvm != null)
                {
                    var ofd = new OpenFileDialog();

                    if (ofd.ShowDialog() == true)
                    {
                        mvm.AddAttachment(ofd.FileName);
                    }
                }
                    
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
             
        }

		private void ComposePanel_OnDragOver(object sender, DragEventArgs e)
		{
			var data = e.Data.GetData("Object");
			if (data is Address || data is MailingList)
			{
				e.Effects = DragDropEffects.Link;
				e.Handled = true;
			}
		}

		private void ComposePanel_OnDrop(object sender, DragEventArgs e)
		{
			var data = e.Data.GetData("Object");

			var mvm = ComposePanel.DataContext as MessageViewModel;
			if (mvm == null)
				return;

			if (data is Address)
			{
				mvm.AddRecipient(data as Address);
				e.Handled = true;
			}
			else if (data is MailingList)
			{
				mvm.AddRecipient(data as MailingList);
				e.Handled = true;
			}
		}
	}
}
