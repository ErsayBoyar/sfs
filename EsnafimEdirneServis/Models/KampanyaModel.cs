using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsnafimEdirneServis.Models
{
    public class KampanyaModel
    {
      
        public DateTime BaslamaZamani { get; set; }
        public DateTime BitisZamani { get; set; }
        public int KampanyaId { get; set; }
        public double? Oran { get; set; }
        public int? EsnafId { get; set; }

    }
}