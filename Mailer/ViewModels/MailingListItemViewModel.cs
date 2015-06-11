using System;
using Mailer.Windows;

namespace Mailer.ViewModels
{
	public class MailingListItemViewModel : RecipientSourceViewModel
	{
		private int countAddresses;

		/// <summary>
		///     Create a new MailingListItemViewModel
		/// </summary>
		/// <param name="mlist">The MailingList that the MailingListItemViewModel represents</param>
		/// <param name="canChange">Whether or not the mailing list can be edited or deleted.</param>
		public MailingListItemViewModel(MailingList mlist, bool canChange)
		{
			MailingList = mlist;
			countAddresses = MailingList.MailingListLines.Count;
			CanChange = canChange;
		}

		/// <summary>
		///     Get the MailingList that this MailingListItemViewModel represents.
		/// </summary>
		public MailingList MailingList { get; private set; }

		public override object Recipient
		{
			get { return MailingList; }
		}

		/// <summary>
		///     Get whether or not the mailing list is able to be edited or deleted from the database.
		/// </summary>
		public bool CanChange { get; private set; }

		/// <summary>
		///     Get the name of the MailingListItemViewModel's MailingList.
		/// </summary>
		public string Name
		{
			get { return MailingList.Name; }
		}

		/// <summary>
		///     The subtext to be displayed underneath the name of the mailinglist.
		/// </summary>
		public string Subtext
		{
			get
			{
				var count = countAddresses;
				var fmt = count == 1 ? "{0} Address" : "{0} Addresses";

				return String.Format(fmt, count);
			}
		}


		/// <summary>
		///     Open a new EditMailingList window to edit the current mailing list.
		/// </summary>
		public void Edit()
		{
			if (!CanChange)
				throw new InvalidOperationException("Cannot edit this mailing list");

			var mailWind = new EditMailingList(new EditMailingViewModel(MailingList));
			mailWind.ShowDialog();

			using (var db = new MailerEntities())
			{
				MailingList = db.MailingLists.Find(MailingList.ListID);
				countAddresses = MailingList.MailingListLines.Count;
			}

			OnPropertyChanged("Name");
			OnPropertyChanged("Subtext");

			MessagePump.Dispatch(this, "MailingListChanged");
		}

		/// <summary>
		///     Delete the underlying MailingList from the database.
		/// </summary>
		public void Delete()
		{
			if (!CanChange)
				throw new InvalidOperationException("Cannot delete this mailing list");

			using (var db = new MailerEntities())
			{
				db.MailingLists.Attach(MailingList);
				db.MailingListLines.RemoveRange(MailingList.MailingListLines);
				db.MailingLists.Remove(MailingList);
				db.SaveChanges();
			}

			MessagePump.Dispatch(this, "MailingListDeleted");
		}
	}
}
