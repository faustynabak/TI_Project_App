//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ti.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Salas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Salas()
        {
            this.SalaWynajmujacies = new HashSet<SalaWynajmujacies>();
        }
    
        public int SalaId { get; set; }
        public Nullable<int> BudynekId { get; set; }
        public string Numer { get; set; }
        public int IloscMiejsca { get; set; }
        public int TypSali { get; set; }
    
        public virtual Budyneks Budyneks { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaWynajmujacies> SalaWynajmujacies { get; set; }
    }
}
