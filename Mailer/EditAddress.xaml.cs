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
using System.Windows.Shapes;

namespace Mailer
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

		private void Okay_Click(object sender, RoutedEventArgs e)
		{
			viewModel.Save();
			Close();
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
