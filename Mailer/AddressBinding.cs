using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer
{
	internal class AddressBinding
	{
		public Address BaseAddress { get; set; }

		public bool Editing { get; set; }

		public bool NotEditing
		{
			get { return !Editing; }
		}
	}
}
