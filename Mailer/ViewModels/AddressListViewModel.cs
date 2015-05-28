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
			vm.Deleted += vm_Deleted;
			AddressListItemViewModels.Add(vm);
		}

		private void vm_Deleted(object sender, EventArgs e)
		{
			var alivm = sender as AddressListItemViewModel;

			AddressListItemViewModels.Remove(alivm);
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
	}
}
