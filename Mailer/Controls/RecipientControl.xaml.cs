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
using Mailer.ViewModels;

namespace Mailer.Controls
{
	/// <summary>
	///     Interaction logic for RecipientControl.xaml
	///     DataContext should be a subclass of RecipientViewModel
	/// </summary>
	public partial class RecipientControl : UserControl
	{
		public RecipientControl()
		{
			InitializeComponent();
		}

		private void Delete_OnClick(object sender, RoutedEventArgs e)
		{
			var vm = DataContext as RecipientViewModel;

			if (vm != null)
			{
				vm.Remove();
			}
		}
	}
}
