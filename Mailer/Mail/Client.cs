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

        private readonly NetworkCredential credentials = new NetworkCredential();
       
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

        public SmtpClient Mailer { get; private set; }
        

        public Client(string host, int port)
        {
			Mailer = new SmtpClient(host, port)
			{
				EnableSsl = true,
				Credentials = credentials
			};
        }

        public void Dispose()
        {
			Mailer.Dispose();
        }

        
    }
}
