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

namespace Mailer
{
	/// <summary>
	/// Interaction logic for AttachmentControl.xaml
	/// </summary>
	public partial class AttachmentControl : UserControl
	{
		public AttachmentControl()
		{
			InitializeComponent();
		}

		private void Delete_OnClick(object sender, RoutedEventArgs e)
		{
			var vm = DataContext as AttachmentViewModel;
			if (vm != null)
			{
				vm.Delete();
			}
		}
	}
}
