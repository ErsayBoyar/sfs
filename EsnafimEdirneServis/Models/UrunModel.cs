using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsnafimEdirneServis.Models
{
    public class UrunModel
    {
        public int UrunId { get; set; }
        public string  Ad { get; set; }
        public string Aciklama { get; set; }
        public string FotoYol { get; set; }
        public decimal? Fiyat { get; set; }
        public bool? Sil { get; set; }
        public bool? Mevcut { get; set; }
    }
}