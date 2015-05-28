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
			    MailingListLines = new ObservableCollection<MailingListLine>(mlist.MailingListLines);
                AvailAddresses = new ObservableCollection<Address>(db.Addresses.Where(addr => MailingList.MailingListLines.All(mll => mll.AddressID != addr.AddressID)));
			}
		}

	    protected EditMailingViewModel()
	    {
	        MailingListLines = new ObservableCollection<MailingListLine>();
            AvailAddresses = new ObservableCollection<Address>();
	    }



		public MailingList MailingList { get; protected set; }

		public ObservableCollection<MailingListLine> MailingListLines { get; protected set; }

        public ObservableCollection<Address> AvailAddresses { get; protected set; } 


        public void AddAddressId(int aid)
        {
            if (MailingListLines.Any(rm => rm.AddressID == aid))
                throw new InvalidOperationException("Address ID already exists!");

            using (var db = new MailerEntities())
            {
                db.MailingLists.Attach(MailingList);
                var newRm = new MailingListLine
                {
                    AddressID = aid
                };
                MailingList.MailingListLines.Add(newRm);
                MailingListLines.Add(newRm);
                db.SaveChangesAsync();
            }
        }


        public void RemoveAddressId(int aid)
        {
            using (var db = new MailerEntities())
            {
                db.MailingLists.Attach(MailingList);

                var rmToRemove = MailingListLines.First(rm => rm.AddressID == aid);
                MailingList.MailingListLines.Remove(rmToRemove);
                MailingListLines.Remove(rmToRemove);
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
