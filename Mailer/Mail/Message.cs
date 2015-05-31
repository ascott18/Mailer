using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;

namespace Mailer.Mail
{

    /*
     * The begging of the message class.  
     * Still needs to be able to allow user to add an attatchment 
     */
    class Message
    {
        private Client client;
        private MailMessage message;
       
        //the addresses in the mailing lists are not currently being added to the message.To
        private readonly List<Address> recipients = new List<Address>();
		
        public string Body
        {
            get { return message.Body; }
            set
            {
                message.Body = value;
            }
        }

		public string Subject
		{
			get { return message.Subject; }
			set
			{
				message.Subject = value;
			}
		}

		public MailAddress From
		{
			get { return message.From; }
			set
			{
				message.From = value;
			}
		}

        public Message(Client client)
        {
           
            //save reference to the sender of the message 
            this.client = client;

            //set the from of the message to the client's username
            message = new MailMessage();

        }

        public void AddRecipient(Address address) 
        {
			recipients.Add(address);
        }

		public void AddRecipient(DynamicMailingList list) 
        {
		    foreach (var line in list.MailingListLines)
			{
				recipients.Add(line.Address);
		    }
        }

        public void AddRecipient(MailingList list) 
        {
	        using (var db = new MailerEntities())
	        {
		        list = db.MailingLists.Find(list.ListID);

		        foreach (var line in list.MailingListLines)
				{
					recipients.Add(line.Address);
		        }
	        }
        }


        public void Send()
        {
			foreach (var address in recipients)
            {
                message.To.Add(new MailAddress(address.Email, address.FirstName + " " + address.LastName));
            }

            client.Mailer.Send(message);
        }

        public void AddAttachment(string attachmentFilename)
        {
            var attachment = new Attachment(attachmentFilename);
            message.Attachments.Add(attachment);
        }
      
    }
}
