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
	/// Interaction logic for MailingListControl.xaml
	/// </summary>
	public partial class MailingListControl : UserControl
	{
		public MailingListControl()
		{
			InitializeComponent();
		}

        private void Edit_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = (MailingListItemViewModel)DataContext;
            vm.Edit();
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = (MailingListItemViewModel)DataContext;

            var res = MessageBox.Show(
                "Are you sure you wish to delete the mailing list " + vm.Name + "?",
                "Are you sure?",
                MessageBoxButton.YesNo);

            if (res != MessageBoxResult.Yes)
                return;

            vm.Delete();
        }
	}
}
