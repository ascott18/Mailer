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

        public MailMessage MailMessage { get; set; }

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

        public Message(Client client)
        {
           
            //save reference to the sender of the message 
            this.client = client;

            //set the from of the message to the client's username
            message = new MailMessage();
            message.From = new MailAddress(client.UserName);

        }

        public void AddRecipient(Address address) 
        {
			recipients.Add(address);
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
                message.To.Add(address.Email);
            }

            client.Mailer.Send(message);
        }

        private void AddAttachment(string attachmentFilename)
        {
            var attachment = new Attachment(attachmentFilename);
            message.Attachments.Add(attachment);
        }
      
    }
}
