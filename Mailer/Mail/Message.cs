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
        private List<MailingList> mailingLists;
        private List<Address> addresses;

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

            addresses = new List<Address>();

            //set the from of the message to the client's username
            message = new MailMessage();
            message.From = new MailAddress(client.UserName);

        }

        public void AddMailingList(MailingList list)
        {
            mailingLists.Add(list);
        }

        public void AddSingleRecipient(Address address) 
        {
            addresses.Add(address);
        }


        public void Send()
        {

            foreach (Address address in addresses)
            {
                message.To.Add(address.Email);
            }
            if (client.Mailer != null)
            {
                client.Mailer.Send(message);
            }
            
        }

        private void AddAttachment(string attachmentFilename, string mediaTypeName)
        {
            Attachment attachment = new Attachment(attachmentFilename, mediaTypeName);
            message.Attachments.Add(attachment);
        }
      
    }
}
