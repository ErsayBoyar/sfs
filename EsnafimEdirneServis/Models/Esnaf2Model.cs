using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsnafimEdirneServis.Models
{
    public class Esnaf2Model
    {
        
        public int EsnafId { get; set; }
        public string FirmaUnvan { get; set; }
        public string KisaAd { get; set; }
        public string FotoYol { get; set; }
        public string Telefon { get; set; }
        public string Adres { get; set; }
        public decimal? MinSiparis { get; set; }
        public int? TeslimatSuresi { get; set; }
        public int? HizmetKategoriID { get; set; }
        public bool? Online { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public IEnumerable<UrunKategoriModel> urunKategori { get; set; }
    }
}