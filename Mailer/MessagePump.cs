using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer
{
	/// <summary>
	///     A very rudimentary message pump for sending messages throughout the application.
	/// </summary>
	public static class MessagePump
	{
		public delegate void Message(object sender, string msg);

		/// <summary>
		///     Dispatch a message to interested listner.
		/// </summary>
		/// <param name="sender">The sender of the message.</param>
		/// <param name="message">The message to be sent.</param>
		public static void Dispatch(object sender, string message)
		{
			if (OnMessage != null)
				OnMessage(sender, message);
		}

		/// <summary>
		///     Occurs when a message is dispatched through the MessagePump.
		/// </summary>
		public static event Message OnMessage;
	}
}
