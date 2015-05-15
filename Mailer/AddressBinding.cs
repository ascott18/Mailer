using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer
{
	class AddressBinding
	{
		public Address BaseAddress { get; private set; }

		public bool Editing { get; set; }

		public bool NotEditing { get { return !Editing; } }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Email { get; set; }

		public long AddressID { get; set; }
	}
}
