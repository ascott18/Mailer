using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Mailer.ViewModels;

namespace Mailer.Windows
{
	/// <summary>
	///     Interaction logic for EditAddress.xaml
	/// </summary>
	public partial class EditAddress : Window
	{
		private readonly EditAddressViewModel viewModel;

		public EditAddress(EditAddressViewModel vm)
		{
			InitializeComponent();

			viewModel = vm;
			DataContext = vm;

			YearsListBox.SelectionChanged += YearsListBox_SelectionChanged;
		}

		private void YearsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			RemoveButton.IsEnabled = YearsListBox.SelectedIndex >= 0;
		}

		private void AddYear_OnClick(object sender, RoutedEventArgs e)
		{
			try
			{
				var year = int.Parse(AddYearTextBox.Text);
				viewModel.AddYear(year);
				AddYearTextBox.Text = "";
				AddYearTextBox.Focus();
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message);
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
				DialogResult = true;
				Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void AddYearTextBox_OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Return)
			{
				AddYear_OnClick(this, e);
			}
		}
	}
}
