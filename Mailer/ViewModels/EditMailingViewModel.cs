using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
			}
		}

		public MailingList MailingList { get; private set; }

		public ObservableCollection<ReceivedMail> ReceivedMails { get; private set; }


        public void Clone(int id)
        {
            using (var db = new MailerEntities())
            {
                var oldList = db.MailingLists.Single(ml => ml.ListID == id);

                var newList = new MailingList
                {
                    Name = oldList.Name,
                    MailingListLines =
                        new HashSet<MailingListLine>(
                            oldList.MailingListLines.Select(mll => new MailingListLine { Address = mll.Address }))
                };

                db.MailingLists.Add(newList);
                db.SaveChanges();
            }
        }
    }
}
