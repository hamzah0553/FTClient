//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FTClientApplication.Model
{
    using System;
    using System.Collections.ObjectModel;
    
    public partial class Selection_member
    {
        public int id { get; set; }
        public int parliamentMemberId { get; set; }
        public int selectionId { get; set; }
    
        public virtual ParliamentMember ParliamentMember { get; set; }
        public virtual Selection Selection { get; set; }
    }
}
