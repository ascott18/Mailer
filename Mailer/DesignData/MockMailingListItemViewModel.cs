using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailer.ViewModels;

namespace Mailer.DesignData
{
    class MockMailingListItemViewModel : MailingListItemViewModel
    {
        public MockMailingListItemViewModel()
            : base(new MailingList()
            {
                Name = "Test MailingList"
            }, true)
        {

        }
    }
}
