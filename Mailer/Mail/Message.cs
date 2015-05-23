using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

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
        


        //not functional
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

           //TODO: Add all of the addresses from the mailing lists to the message.To list
            //add all individually added addresses if exitst to the message.To

            client.Mailer.Send(message);
        }




      
    }
}
