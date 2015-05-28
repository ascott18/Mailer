using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.ViewModels
{


	class AddressListViewModel : BaseViewModel
	{

        public ObservableCollection<AddressListItemViewModel> AddressViewModels { get; private set; }


		public AddressListViewModel()
		{
			AddressViewModels = new ObservableCollection<AddressListItemViewModel>();

			using (var db = new MailerEntities())
			{
				foreach (var address in db.Addresses)
				{
					AddAddressListItemViewModel(address);
				}
			}
		}

		private AddressListItemViewModel AddAddressListItemViewModel(Address address)
		{
			var vm = new AddressListItemViewModel(address);
			vm.Deleted += vm_Deleted;
			AddressViewModels.Add(vm);
			return vm;
		}

		void vm_Deleted(object sender, EventArgs e)
		{
			var alivm = sender as AddressListItemViewModel;

			AddressViewModels.Remove(alivm);
		}

		

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


				var vm = AddAddressListItemViewModel(address);
				vm.Edit();
			}
		}
	}

	
}
