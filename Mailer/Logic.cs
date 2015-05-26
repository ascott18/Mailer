using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer
{
    public class Logic
    {
   

        public void Clone(int id)
        {
            var db = new MailerEntities();
           // using (var db = new MailerEntities();)
          //  {
                var oldList = db.MailingLists.Single(ml => ml.ListID == id);

                var newList = new MailingList
                {
                    Name = oldList.Name,
                    MailingListLines = new HashSet<MailingListLine>(oldList.MailingListLines.Select(mll => new MailingListLine{Address = mll.Address}))
                };

                db.MailingLists.Add(newList);
                db.SaveChanges();
           // }

            
        }
    }
}
