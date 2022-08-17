using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsnafimEdirneServis.Models
{
    public class UrunKategoriModel2
    {
        public int UrunKategoriId { get; set; }
        public int EsnafId { get; set; }
        public string Ad { get; set; }
        public bool? Sil { get; set; }
        public int? urunAdet { get; set; }
    }
}