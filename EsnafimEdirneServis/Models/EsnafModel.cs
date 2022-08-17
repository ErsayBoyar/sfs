using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsnafimEdirneServis.Models
{
    public class EsnafModel
    {
        public int Id { get; set; }
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
        public bool? isNext { get; set; }
        public bool? isPrevious { get; set; }
        public IEnumerable<KampanyaModel> Kampanya { get; set; }

    }
}