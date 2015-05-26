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

		public ObservableCollection<AddressListItemViewModel> AddressViewModels { get; private set; }

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

	internal class AddressListItemViewModel : BaseViewModel
	{
		public AddressListItemViewModel(Address address)
		{
			Address = address;
			FirstName = address.FirstName;
			LastName = address.LastName;
			Email = address.Email;
		}

		public Address Address { get; private set; }
		
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }

		public void Edit()
		{
			var addrWind = new EditAddress(new EditAddressViewModel(Address));
			addrWind.ShowDialog();

			using (var db = new MailerEntities())
			{
				Address = db.Addresses.Find(Address.AddressID);
			}

			FirstName = Address.FirstName;
			OnPropertyChanged("FirstName");

			LastName = Address.LastName;
			OnPropertyChanged("LastName");

			Email = Address.Email;
			OnPropertyChanged("Email");

		}

		public void Delete()
		{
			using (var db = new MailerEntities())
			{
				db.Addresses.Attach(Address);
				db.MailingListLines.RemoveRange(Address.MailingListLines);
				db.ReceivedMails.RemoveRange(Address.ReceivedMails);
				db.Addresses.Remove(Address);
				db.SaveChanges();
			}

			OnDeleted();
		}

		/// <summary>
		/// Fired when the address represented by the ViewModel is deleted by the ViewModel.
		/// </summary>
		public event EventHandler Deleted;

		protected virtual void OnDeleted()
		{
			EventHandler handler = Deleted;
			if (handler != null) handler(this, EventArgs.Empty);
		}
	}
}
