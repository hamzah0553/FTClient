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
    
    public partial class Municipality
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Municipality()
        {
            this.Mayor = new ObservableCollection<Mayor>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public int regionId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ObservableCollection<Mayor> Mayor { get; set; }
        public virtual Region Region { get; set; }
    }
}
