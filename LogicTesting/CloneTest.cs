using System;
using Mailer.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mailer;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicTesting
{
    [TestClass]
    public class CloneTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            int id = 1;

            MailingListViewModel testClone = new MailingListViewModel();

            testClone.Clone(id);

            using (var db = new MailerEntities())
            {
                var name = db.MailingLists.Single(ml => ml.ListID == id).Name;

                var ids = db.MailingLists.Where(ml => ml.Name.Equals(name));

            }

        }
    }
}
