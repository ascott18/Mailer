using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mailer.DesignData
{
    class MockAddress : Address
    {
        
        public MockAddress()
        {
            FirstName = "Frank";
            LastName = "Sinatra";
            Email = "franksinatra@dawg.net";

        }
    }
}
