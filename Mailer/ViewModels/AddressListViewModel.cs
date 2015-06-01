using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mailer.ViewModels
{
	/// <summary>
	///     A ViewModel that represents a collection of Addresses and their associated
	///     AddressListItemViewModels.
	///     Provides behavior for creating new addresses.
	/// </summary>
	internal class AddressListViewModel : BaseViewModel
	{
		private string filter = "";

		public AddressListViewModel()
		{
			AddressListItemViewModels = new ObservableCollection<AddressListItemViewModel>();
			AddressListItemViewModelsUnfiltered = new ObservableCollection<AddressListItemViewModel>();
		}

		/// <summary>
		///     The string to filter the addresses by.
		/// </summary>
		public string Filter
		{
			get { return filter; }
			set
			{
				filter = value;
				UpdateFilter();
			}
		}

		/// <summary>
		///     The collection of AddressListItemViewModels that this AddressListViewModel represents, after
		///     filtering is applied
		/// </summary>
		public ObservableCollection<AddressListItemViewModel> AddressListItemViewModels { get; protected set; }

		/// <summary>
		///     The collection of AddressListItemViewModels that this AddressListViewModel represents, before
		///     filtering is applied
		/// </summary>
		private ObservableCollection<AddressListItemViewModel> AddressListItemViewModelsUnfiltered { get; set; }

		/// <summary>
		///     Add the specified AddressListItemViewModel to this AddressListViewModel.
		/// </summary>
		/// <param name="alivm">The AddressListItemViewModel to add.</param>
		public void AddAddressListItemViewModel(AddressListItemViewModel alivm)
		{
			AddressListItemViewModelsUnfiltered.Add(alivm);
			UpdateFilter();
		}

		/// <summary>
		///     Remove the specified AddressListItemViewModel from this AddressListViewModel.
		/// </summary>
		/// <param name="alivm">The AddressListItemViewModel to remove.</param>
		public void RemoveAddressListItemViewModels(AddressListItemViewModel alivm)
		{
			AddressListItemViewModelsUnfiltered.Remove(alivm);
			UpdateFilter();
		}

		/// <summary>
		///     Add a new Address to the database, and opens a dialog to edit that new address.
		/// </summary>
		public void Add()
		{
			using (var db = new MailerEntities())
			{
				var address = new Address
				{
					FirstName = "",
					LastName = "",
					Email = "",
				};
				db.Addresses.Add(address);
				db.SaveChanges();


				var vm = new AddressListItemViewModel(address);
				if (vm.Edit() == true)
				{
					AddAddressListItemViewModel(vm);
				}
				else
				{
					db.Addresses.Remove(address);
					db.SaveChanges();
				}
			}
		}


		/// <summary>
		///     Command the ViewModel to listen to appropriate events in order to keep itself updated from
		///     MailerEntities.
		/// </summary>
		public void StartAutoUpdating()
		{
			MessagePump.OnMessage -= MessagePump_OnMessage;
			MessagePump.OnMessage += MessagePump_OnMessage;

			using (var db = new MailerEntities())
			{
				foreach (var address in db.Addresses)
				{
					AddAddressListItemViewModel(new AddressListItemViewModel(address));
				}
			}
		}

		/// <summary>
		///     Handle messages from the message pump and trigger updates when appropriate
		/// </summary>
		private void MessagePump_OnMessage(object sender, string msg)
		{
			if (msg == "AddressDeleted")
			{
				var alivm = sender as AddressListItemViewModel;
				if (alivm == null)
					throw new NullReferenceException("Expected AddressListItemViewModel from message AddressDeleted");

				RemoveAddressListItemViewModels(alivm);
			}
		}

		private void UpdateFilter()
		{
			var newCollection = new ObservableCollection<AddressListItemViewModel>();

			var filterLower = Filter.ToLower().Trim();

			foreach (var unfiltered in AddressListItemViewModelsUnfiltered)
			{
				var addr = unfiltered.Address;

				if (addr.FirstName.ToLower().Contains(filterLower) ||
				    addr.LastName.ToLower().Contains(filterLower) ||
				    (addr.FirstName + " " + addr.LastName).ToLower().Contains(filterLower))
				{
					newCollection.Add(unfiltered);
				}
			}

			AddressListItemViewModels =
				new ObservableCollection<AddressListItemViewModel>(
					newCollection.OrderBy(alivm => alivm.Address.LastName.ToLower()));
			OnPropertyChanged("AddressListItemViewModels");
		}
	}
}
