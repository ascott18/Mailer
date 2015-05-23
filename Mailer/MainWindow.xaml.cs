using Mailer.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Mailer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            using (var db = new MailerEntities())
            {
                db.Addresses.Add(new Address()
                {
                    FirstName = "Andrew",
                    LastName = "Scott",
                    Email = "test@google.com",
                    ReceivedMails = new List<ReceivedMail>()
					{
						new ReceivedMail(){Year = 1}
					}
                });
                db.SaveChanges();
            }
            
            
            Client client = new Client("smtp.gmail.com", 587);
            client.UserName = "youremail@gmail.com";
            client.Password = "yourpassword";

            Mail.Message message = new Mail.Message(client);
            message.Subject = "Test message";
            message.Body = "Here is the body of my test email";
            Address address = new Address();
            address.Email = "recipient@example.com";
            message.AddSingleRecipient(address);
            message.Send();
            




           
        }

    }
}
