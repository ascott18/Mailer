using System;
using System.Linq;
using System.Windows;
using Mailer.ViewModels;

namespace Mailer.Windows
{
	/// <summary>
	///     Interaction logic for EditMailingList.xaml
	/// </summary>
	public partial class EditMailingList : Window
	{
		private readonly EditMailingViewModel viewModel;

		public EditMailingList(EditMailingViewModel editMailingViewModel)
		{
			InitializeComponent();
			DataContext = editMailingViewModel;
			viewModel = editMailingViewModel;
		}

		private void Okay_OnClick(object sender, RoutedEventArgs e)
		{
			viewModel.Save();
			Close();
		}

		private void Remove_OnClick(object sender, RoutedEventArgs e)
		{
			var addresses = CurrentListBox.SelectedItems.OfType<Address>().ToList();

			foreach (var address in addresses)
			{
				viewModel.RemoveAddressId(address.AddressID);
			}
		}

		private void Add_OnClick(object sender, RoutedEventArgs e)
		{
			var addresses = AvailableListBox.SelectedItems.OfType<Address>().ToList();
			foreach (var address in addresses)
			{
				viewModel.AddAddressId(address.AddressID);
			}
		}
	}
}
