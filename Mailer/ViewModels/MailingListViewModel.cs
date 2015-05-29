using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailer.ViewModels;

namespace Mailer.ViewModels
{
	public class MailingListViewModel : BaseViewModel
    {

        
        public ObservableCollection<MailingListItemViewModel> MailingListViewModels { get; protected set; }


        public MailingListViewModel()
        {
           MailingListViewModels = new ObservableCollection<MailingListItemViewModel>();

        }


	    public void AddMailingListItemViewModel(MailingListItemViewModel vm)
        {
            vm.Deleted += vm_Deleted;
            MailingListViewModels.Add(vm);
        }

        void vm_Deleted(object sender, EventArgs e)
        {
            var alivm = sender as MailingListItemViewModel;

            MailingListViewModels.Remove(alivm);
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


                var vm = new MailingListItemViewModel(mlist);
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
    }
}
