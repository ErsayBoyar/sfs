using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsnafimEdirneServis.Models
{
    public class SiparisDetayModel
    {
        public int SiparisDetayId { get; set; }
        public int SiparisId { get; set; }
        public int UrunId { get; set; }
        public int Adet { get; set; }
        public string UrunAd { get; set; }
        public string UrunFoto { get; set; }
        public decimal Fiyat { get; set; }
        public decimal IndirimTutari { get; set; }
        public decimal IndirimliTutar { get; set; }
    }
}