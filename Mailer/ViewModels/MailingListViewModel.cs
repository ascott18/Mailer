﻿using System;
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

            using (var db = new MailerEntities())
            {
                foreach (var mlist in db.MailingLists)
                {
                    AddMailingListItemViewModel(mlist);
                }
            }
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
            var alivm = sender as MailingListItemViewModel;

            MailingViewModels.Remove(alivm);
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
    }


   
    internal class MailingListItemViewModel : BaseViewModel
    {
        public MailingListItemViewModel(MailingList mlist)
        {
            MailingList = mlist;
            Name = mlist.Name;
        }

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
