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
     *Client class that validates the users user name and password and 
     * connects user as an SMTP client with thier host and given port number.
     */

    class Client
    {

        private SmtpClient mailer;
        private NetworkCredential credentials;
       
        public string UserName
        {
            get { return credentials.UserName; }
            set { credentials.UserName = value; }
        }

        public string Password
        {
            get { return credentials.Password;}
            set { credentials.Password = value; }
        }

        public SmtpClient Mailer { get; set; }
        

        public Client(string host, int port)
        {
            mailer = new SmtpClient(host, port);
            mailer.EnableSsl = true;
        }

        public void Dispose()
        {
            mailer.Dispose();
        }

        
    }
}
