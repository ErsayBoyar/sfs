using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsnafimEdirneServis.Models
{
    public class MusteriModel
    {
        public int MusteriId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Eposta { get; set; }
        public string Telefon { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }
        public IEnumerable<SiparisModel> siparis { get; set; }
    }
}