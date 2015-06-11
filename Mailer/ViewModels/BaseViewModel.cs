using System;
using System.ComponentModel;
using System.Linq;

namespace Mailer.ViewModels
{
	/// <summary>
	///     The base class for all ViewModels for the Mailer project. Implements INotifyPropertyChanged.
	/// </summary>
	public class BaseViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName = null)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
