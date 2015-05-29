using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailer.ViewModels;

namespace Mailer.DesignData
{
    internal class MockMailingListViewModel : MailingListViewModel
    {
        public MockMailingListViewModel()
        {
            MailingListViewModels = new ObservableCollection<MailingListItemViewModel>
			{
				new MockMailingListItemViewModel(),
				new MockMailingListItemViewModel(),
				new MockMailingListItemViewModel()
			};
        }
    }
}
