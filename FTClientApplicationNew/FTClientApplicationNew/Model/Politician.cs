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
    
    public partial class Politician
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Politician()
        {
            this.ContactInfo = new ObservableCollection<ContactInfo>();
            this.ParliamentMember = new ObservableCollection<ParliamentMember>();
            this.Mayor = new ObservableCollection<Mayor>();
            this.Minister = new ObservableCollection<Minister>();
        }
    
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int partyId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<ContactInfo> ContactInfo { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<ParliamentMember> ParliamentMember { get; set; }
        public virtual Party Party { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Mayor> Mayor { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Minister> Minister { get; set; }
    }
}
