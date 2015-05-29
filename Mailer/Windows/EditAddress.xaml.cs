using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Mailer.ViewModels;

namespace Mailer.Windows
{
	/// <summary>
	/// Interaction logic for EditAddress.xaml
	/// </summary>
	public partial class EditAddress : Window
	{
		private EditAddressViewModel viewModel;

		public EditAddress(EditAddressViewModel vm)
		{
			InitializeComponent();

			viewModel = vm;
			DataContext = vm;

			YearsListBox.SelectionChanged += YearsListBox_SelectionChanged;
		}

		void YearsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			RemoveButton.IsEnabled = YearsListBox.SelectedIndex >= 0;
		}

		private void AddYear_OnClick(object sender, RoutedEventArgs e)
		{
			try
			{
				var year = int.Parse(AddYearTextBox.Text);
				viewModel.AddYear(year);
			}
			catch (Exception)
			{
				MessageBox.Show(this, "Invalid year!");
			}
		}

		private void RemoveYear_OnClick(object sender, RoutedEventArgs e)
		{
			var selectedItem = YearsListBox.SelectedItem;

			if (selectedItem != null)
				viewModel.RemoveYear((int)((ReceivedMail)selectedItem).Year);
		}

		private void Okay_OnClick(object sender, RoutedEventArgs e)
		{
            try
            {
                viewModel.Save();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
			
		}
	}
}
