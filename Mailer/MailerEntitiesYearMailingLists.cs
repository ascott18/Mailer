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
		public static ReadOnlyCollection<MailingList> GetYearMailingLists(this MailerEntities db)
		{
			var ymls = new Collection<MailingList>();

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

				ymls.Add(mailingList);
			}

			return new ReadOnlyCollection<MailingList>(ymls);
		}
	}
}
