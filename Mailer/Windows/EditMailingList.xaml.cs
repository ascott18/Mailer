using System;
using System.Linq;
using System.Windows;
using Mailer.ViewModels;

namespace Mailer.Windows
{
	/// <summary>
	/// Interaction logic for EditMailingList.xaml
	/// </summary>
	public partial class EditMailingList : Window
	{
		public EditMailingList(EditMailingViewModel editMailingViewModel)
		{
			InitializeComponent();
			DataContext = editMailingViewModel;
		}
	}
}
