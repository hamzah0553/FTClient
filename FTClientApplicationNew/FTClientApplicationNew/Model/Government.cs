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
    
    public partial class Government
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Government()
        {
            this.MinisterialPost = new ObservableCollection<MinisterialPost>();
        }
    
        public int id { get; set; }
        public System.DateTime startDate { get; set; }
        public Nullable<System.DateTime> endDate { get; set; }
        public string partiesInThisGov { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<MinisterialPost> MinisterialPost { get; set; }
    }
}
