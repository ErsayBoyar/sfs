using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsnafimEdirneServis.Models
{
    public class SiparisModel
    {
        public int SiparisId { get; set; }
        public string EsnafAd { get; set; }
        public string EsnafTelefon { get; set; }
        public string EsnafFoto { get; set; }
        public string SiparisNotu { get; set; }
        public decimal ToplamTutar { get; set; }
        public decimal IndirimTutari { get; set; }
        public decimal IndirimliTutar { get; set; }
        public decimal MinSiparis { get; set; }
        public DateTime Tarih { get; set; }
        public string OdemeDurum { get; set; }
        public int DurumId { get; set; }
        public string Durum { get; set; }
        public int AdresId { get; set; }
        public bool Onay { get; set; }
        public string AdresAd { get; set; }
        public string Adres { get; set; }
        public string AdresTarifi { get; set; }
        public string BinaNo { get; set; }
        public string Daire { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public string MusteriAd { get; set; }
        public string MusteriSoyad { get; set; }
        public string MusteriMail { get; set; }
        public string MusteriTelefon { get; set; }
        public IEnumerable<SiparisDetayModel> siparisDetay { get; set; }
    }
}