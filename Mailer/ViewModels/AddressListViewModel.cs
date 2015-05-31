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
		public AddressListViewModel()
		{
			AddressListItemViewModels = new ObservableCollection<AddressListItemViewModel>();
		}

		/// <summary>
		///     The collection of AddressListItemViewModels that this AddressListViewModel represents.
		/// </summary>
		public ObservableCollection<AddressListItemViewModel> AddressListItemViewModels { get; protected set; }

		/// <summary>
		///     Add the specified AddressListItemViewModel to this AddressListViewModel.
		/// </summary>
		/// <param name="vm">The AddressListItemViewModel to add.</param>
		public void AddAddressListItemViewModel(AddressListItemViewModel vm)
		{
			AddressListItemViewModels.Add(vm);
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
				db.SaveChangesAsync();


				var vm = new AddressListItemViewModel(address);
				AddAddressListItemViewModel(vm);
				vm.Edit();
			}
		}


		/// <summary>
		/// Command the ViewModel to listen to appropriate events in order to keep itself updated from MailerEntities.
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
		/// Handle messages from the message pump and trigger updates when appropriate
		/// </summary>
		private void MessagePump_OnMessage(object sender, string msg)
		{
			if (msg == "AddressDeleted")
			{
				var alivm = sender as AddressListItemViewModel;
				if (alivm == null)
					throw new NullReferenceException("Expected AddressListItemViewModel from message AddressDeleted");

				AddressListItemViewModels.Remove(alivm);
			}
		}
	}
}
