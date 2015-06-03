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
	/// <summary>
	///     A ViewModel that represents a collection of MailingLists and their associated
	///     MailingListItemViewModels.
	///     Provides behavior for creating new mailing lists.
	/// </summary>
	public class MailingListViewModel : BaseViewModel
    {
		/// <summary>
		/// The collection of MailingListItemViewModels that this MailingListViewModel represents
		/// </summary>
        public ObservableCollection<MailingListItemViewModel> MailingListItemViewModels { get; protected set; }


        public MailingListViewModel()
        {
            MessagePump.OnMessage += MessagePump_OnMessage;

            MailingListItemViewModels = new ObservableCollection<MailingListItemViewModel>();
        }

		/// <summary>
		///     Add the specified MailingListItemViewModel to this MailingListViewModel.
		/// </summary>
		/// <param name="vm">The MailingListItemViewModel to add.</param>
	    public void AddMailingListItemViewModel(MailingListItemViewModel vm)
        {
            MailingListItemViewModels.Add(vm);
        }

		/// <summary>
		///     Add a new MailingList to the database, and opens a dialog to edit it.
		/// </summary>
        public void Add()
        {
            using (var db = new MailerEntities())
            {
                var mlist = new MailingList
                {
                    Name = "New Mailing List"
                };
                db.MailingLists.Add(mlist);
                db.SaveChangesAsync();


                var vm = new MailingListItemViewModel(mlist, true);
                AddMailingListItemViewModel(vm);
                vm.Edit();
            }
        }



		/// <summary>
		/// Handle messages from the message pump and trigger updates when appropriate
		/// </summary>
		private void MessagePump_OnMessage(object sender, string msg)
		{
			if (msg.StartsWith("Address") || msg.StartsWith("MailingList"))
			{
				LoadFromDatabase();
			}
		}

		/// <summary>
		/// Update the ViewModel with data from MailerEntities.
		/// </summary>
		public void LoadFromDatabase()
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
