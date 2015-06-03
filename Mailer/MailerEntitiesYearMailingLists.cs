using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mailer.Mail;

namespace Mailer
{
	public static class MailerEntitiesYearMailingLists
	{
		public static IEnumerable<DynamicMailingList> GetYearMailingLists(this MailerEntities db)
		{
			foreach (var receivedMailsInYear in db.ReceivedMails.GroupBy(rm => rm.Year))
			{
				var mailingList = new DynamicMailingList
				{
					ListID = -1,
					Name = "Recieved in " + receivedMailsInYear.Key,
				};

				mailingList.MailingListLines = new ReadOnlyCollection<MailingListLine>(
					receivedMailsInYear.Select(receivedMail => new MailingListLine
					{
						MailingList = mailingList,
						ListID = -1,
						Address = receivedMail.Address,
						AddressID = receivedMail.AddressID
					}).ToArray());

				yield return mailingList;
			}
		}
	}
}
