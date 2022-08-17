using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsnafimEdirneServis.Models
{
    public class AdresDetayModel
    {
        
            public int AdresId { get; set; }
            public string AdresAd { get; set; }
            public string Adres { get; set; }
            public string BinaNo { get; set; }
            public string Daire { get; set; }
            public string Kat { get; set; }
            public string Lat { get; set; }
            public string Lon { get; set; }
            public string AdresTarifi { get; set; }

    }
}