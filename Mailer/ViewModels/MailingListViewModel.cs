using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailer.DesignData;
using Mailer.ViewModels;

namespace Mailer.ViewModels
{
	public class MailingListViewModel : BaseViewModel
    {

        public ObservableCollection<MailingListItemViewModel> MailingListItemViewModels { get; protected set; }


        public MailingListViewModel()
        {
           MailingListItemViewModels = new ObservableCollection<MailingListItemViewModel>();
        }


	    public void AddMailingListItemViewModel(MailingListItemViewModel vm)
        {
            MailingListItemViewModels.Add(vm);
        }


        public void Add()
        {
            using (var db = new MailerEntities())
            {
                var mlist = new MailingList
                {
                    Name = ""
                };
                db.MailingLists.Add(mlist);
                db.SaveChangesAsync();


                var vm = new MailingListItemViewModel(mlist, true);
                AddMailingListItemViewModel(vm);
                vm.Edit();
            }
        }

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




		/// <summary>
		/// Command the MailingListViewModel to listen to appropriate events in order to keep itself updated from MailerEntities.
		/// </summary>
		public void StartAutoUpdating()
		{
			MessagePump.OnMessage -= MessagePump_OnMessage;
			MessagePump.OnMessage += MessagePump_OnMessage;

			UpdateMailingLists();
		}

		/// <summary>
		/// Handle messages from the message pump and trigger updates when appropriate
		/// </summary>
		private void MessagePump_OnMessage(object sender, string msg)
		{
			if (msg.StartsWith("Address") || msg.StartsWith("MailingList"))
			{
				UpdateMailingLists();
			}
		}

		/// <summary>
		/// Update the ViewModel with data from MailerEntities.
		/// </summary>
		private void UpdateMailingLists()
		{
			using (var db = new MailerEntities())
			{
				MailingListItemViewModels.Clear();

				// Add regular mailing lists.
				foreach (var mlist in db.MailingLists)
				{
					AddMailingListItemViewModel(new MailingListItemViewModel(mlist, true));
				}

				// Add dynamic mailing lists
				var yearMailingLists = db.GetYearMailingLists();
				foreach (var mlist in yearMailingLists)
				{
					AddMailingListItemViewModel(new MailingListItemViewModel(mlist, false));
				}
			}
		}
    }
}
