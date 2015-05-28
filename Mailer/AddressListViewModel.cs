using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer
{
	class AddressListViewModel : BaseViewModel
	{
		public AddressListViewModel()
		{
			AddressViewModels = new ObservableCollection<AddressListItemViewModel>();
		}

		public AddressListItemViewModel AddAddressListItemViewModel(AddressListItemViewModel vm)
		{
			vm.Deleted += vm_Deleted;
			AddressViewModels.Add(vm);
			return vm;
		}

		void vm_Deleted(object sender, EventArgs e)
		{
			var alivm = sender as AddressListItemViewModel;

			AddressViewModels.Remove(alivm);
		}

		public ObservableCollection<AddressListItemViewModel> AddressViewModels { get; protected set; }

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


				var vm = AddAddressListItemViewModel(new AddressListItemViewModel(address));
				vm.Edit();
			}
		}
	}

	
}
