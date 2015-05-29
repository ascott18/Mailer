using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.ViewModels
{
	public class EditMailingViewModel
    {
        public EditMailingViewModel(MailingList mlist)
		{
			using (var db = new MailerEntities())
			{
				MailingList = db.MailingLists.Find(mlist.ListID);

			    var allAddresses = db.Addresses.ToList();

                CurrAddresses = new ObservableCollection<Address>();

			    foreach (var mailingListLine in MailingList.MailingListLines)
			    {
			        CurrAddresses.Add(mailingListLine.Address);
			        allAddresses.Remove(mailingListLine.Address);
			    }

                AvailAddresses = new ObservableCollection<Address>(allAddresses);
			}
		}

	    protected EditMailingViewModel()
	    {
            CurrAddresses = new ObservableCollection<Address>();
            AvailAddresses = new ObservableCollection<Address>();
	    }



		public MailingList MailingList { get; protected set; }

        public ObservableCollection<Address> AvailAddresses { get; protected set; }

        public ObservableCollection<Address> CurrAddresses { get; protected set; } 


        public void AddAddressId(long aid)
        {
            if (CurrAddresses.Any(addr => addr.AddressID == aid))
                throw new InvalidOperationException("Address ID already exists!");

            using (var db = new MailerEntities())
            {
                db.MailingLists.Attach(MailingList);
                var newRm = new MailingListLine
                {
                    AddressID = aid
                };
                MailingList.MailingListLines.Add(newRm);
                var address = db.Addresses.Find(aid);
                CurrAddresses.Add(address);
                AvailAddresses.Remove(AvailAddresses.Single(addr => addr.AddressID == aid));
                db.SaveChangesAsync();
            }
        }


        public void RemoveAddressId(long aid)
        {
            using (var db = new MailerEntities())
            {
                db.MailingLists.Attach(MailingList);

                var mllToRemove = MailingList.MailingListLines.First(mll => mll.AddressID == aid);
                MailingList.MailingListLines.Remove(mllToRemove);
                AvailAddresses.Add(mllToRemove.Address);
                CurrAddresses.Remove(CurrAddresses.Single(addr => addr.AddressID == aid));
                db.SaveChangesAsync();
            }
        }

        public void Save()
        {
            using (var db = new MailerEntities())
            {
                db.MailingLists.Attach(MailingList);
                db.Entry(MailingList).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

       
    }
}
