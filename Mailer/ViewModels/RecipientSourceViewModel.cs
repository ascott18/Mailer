using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.ViewModels
{
	public abstract class RecipientSourceViewModel : BaseViewModel
	{
		public virtual object Recipient
		{
			get { throw new NotImplementedException(); }
		}
	}
}
