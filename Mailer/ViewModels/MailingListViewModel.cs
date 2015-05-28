using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailer.ViewModels;

namespace Mailer.ViewModels
{
    class MailingListViewModel : BaseViewModel
    { 
        public ObservableCollection<MailingListItemViewModel> MailingViewModels { get; private set; }


        public MailingListViewModel()
        {
           MailingViewModels = new ObservableCollection<MailingListItemViewModel>();
        }

        private MailingListItemViewModel AddMailingListItemViewModel(MailingList mlist)
        {
            var vm = new MailingListItemViewModel(mlist);
            vm.Deleted += vm_Deleted;
            MailingViewModels.Add(vm);
            return vm;
        }

        void vm_Deleted(object sender, EventArgs e)
        {
            var mlivm = sender as MailingListItemViewModel;

            MailingViewModels.Remove(mlivm);
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


                var vm = AddMailingListItemViewModel(mlist);
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
