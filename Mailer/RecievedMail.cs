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
    
    public partial class RecievedMail
    {
        public long AddressID { get; set; }
        public long Year { get; set; }
    
        ///<summary>The Address entity that sent the email.</summary>
        public virtual Address SenderAddress { get; set; }
    }
}
