using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsnafimEdirneServis.Models
{
    public class SepetModel
    {
        public int SiparisId { get; set; }
        public int? SepetCount { get; set; }
        public int? EsnafId { get; set; }
        public string EsnafAd { get; set; }
        public string EsnafTelefon { get; set; }
        public string EsnafFoto { get; set; }
        public decimal ToplamTutar { get; set; }
        public decimal IndirimTutari { get; set; }
        public decimal IndirimliTutar { get; set; }
        public decimal MinSiparis { get; set; }
        public DateTime Tarih { get; set; }
        public IEnumerable<SiparisDetayModel> siparisDetay { get; set; }
    }
}