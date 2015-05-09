//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Mailer
{
    using System;
    using System.Collections.Generic;
    
    ///<summary>Represents an Address belonging to a MailingList.</summary>
    public partial class MailingListLine
    {
        public long ListID { get; set; }
        public long AddressID { get; set; }
    
        ///<summary>The MailingList entity that owns this MailingListLine.</summary>
        public virtual MailingList MailingList { get; set; }
        ///<summary>The Address entity represented by this MailingListLine.</summary>
        public virtual Address Address { get; set; }
    }
}
