//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EsnafimEdirneServis.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class MusteriAdres
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MusteriAdres()
        {
            this.Siparis = new HashSet<Siparis>();
        }
    
        public int Id { get; set; }
        public Nullable<int> MusteriId { get; set; }
        public string AdresAd { get; set; }
        public string Adres { get; set; }
        public string BinaNo { get; set; }
        public string Daire { get; set; }
        public string Kat { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public Nullable<bool> Sil { get; set; }
        public string AdresTarifi { get; set; }
    
        public virtual Musteri Musteri { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Siparis> Siparis { get; set; }
    }
}
