using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.ViewModels
{
    class AddressListItemViewModel : BaseViewModel
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
