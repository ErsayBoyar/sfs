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
    
    public partial class Siparis
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Siparis()
        {
            this.SiparisDetay = new HashSet<SiparisDetay>();
        }
    
        public int Id { get; set; }
        public string SiparisNotu { get; set; }
        public Nullable<System.DateTime> Tarih { get; set; }
        public Nullable<int> OdemeId { get; set; }
        public Nullable<int> DurumId { get; set; }
        public Nullable<int> AdresId { get; set; }
        public Nullable<int> MusteriId { get; set; }
        public Nullable<int> EsnafId { get; set; }
        public Nullable<bool> Sil { get; set; }
        public Nullable<decimal> ToplamTutar { get; set; }
        public Nullable<bool> Onay { get; set; }
        public Nullable<decimal> IndirimTutari { get; set; }
        public Nullable<decimal> IndirimliTutar { get; set; }
    
        public virtual Musteri Musteri { get; set; }
        public virtual MusteriAdres MusteriAdres { get; set; }
        public virtual SiparisDurum SiparisDurum { get; set; }
        public virtual SiparisOdemeTur SiparisOdemeTur { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SiparisDetay> SiparisDetay { get; set; }
        public virtual Esnaf Esnaf { get; set; }
    }
}
