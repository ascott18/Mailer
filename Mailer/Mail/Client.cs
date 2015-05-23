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

        public SmtpClient Mailer { get; set; }
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

        

        public Client(string host, int port)
        {
            Mailer = new SmtpClient(host, port);
            credentials = new NetworkCredential();
            Mailer.EnableSsl = true;

            Mailer.Credentials = credentials;
        }

        public void Dispose()
        {
            Mailer.Dispose();
        }

        
    }
}
