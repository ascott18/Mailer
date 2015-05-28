using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.ViewModels
{
    class MailingListItemViewModel : BaseViewModel
    {
        public MailingListItemViewModel(MailingList mlist)
        {
            MailingList = mlist;
            Name = mlist.Name;
        }

        protected MailingListItemViewModel(){}

        public MailingList MailingList { get; private set; }

        public string Name { get; set; }


        public void Edit()
        {
            var mailWind = new EditMailingList(new EditMailingViewModel(MailingList));
            mailWind.ShowDialog();

            using (var db = new MailerEntities())
            {
                MailingList = db.MailingLists.Find(MailingList.ListID);
            }

            Name = MailingList.Name;
            OnPropertyChanged("Name");

        }

        public void Delete()
        {
            using (var db = new MailerEntities())
            {
                db.MailingLists.Attach(MailingList);
                db.MailingListLines.RemoveRange(MailingList.MailingListLines);
                db.MailingLists.Remove(MailingList);
                db.SaveChanges();
            }

            OnDeleted();
        }

        /// <summary>
        /// Fired when the mailinglist represented by the ViewModel is deleted by the ViewModel.
        /// </summary>
        public event EventHandler Deleted;

        protected virtual void OnDeleted()
        {
            EventHandler handler = Deleted;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
