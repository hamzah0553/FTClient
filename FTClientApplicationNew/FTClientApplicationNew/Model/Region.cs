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
    
    public partial class Region
    {
        public int id { get; set; }
        public string name { get; set; }
        public int municipalityId { get; set; }
    
        public virtual Municipality Municipality { get; set; }
    }
}