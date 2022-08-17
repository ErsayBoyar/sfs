using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class MusteriAdresListController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public Data[] Data { get; set; }
        }
        public class Data
        {
            public int? Id { get; set; }
            public string AdresAd { get; set; }
            public string Adres { get; set; }
            public string AdresTarifi { get; set; }
            public string BinaNo { get; set; }
            public string Daire { get; set; }
            public string Kat { get; set; }
            public string Lat { get; set; }
            public string Lon { get; set; }
            public int? MusteriId { get; set; }
            public bool? Sil { get; set; }
        }
        EsnafimEdirneEntities db = new EsnafimEdirneEntities();
        // GET /api/values
        [CustomAuthorization]
        public ProductApiCollection Get(int musteriId)
        {

            var result = new ProductApiCollection();
            var apiModels = db.MusteriAdres.Select(x => new Data
            {
                Id= x.Id,
                AdresAd =x.AdresAd,
                Adres=x.Adres,
                AdresTarifi= x.AdresTarifi,
                BinaNo=x.BinaNo,
                Daire=x.Daire,
                Kat= x.Kat,
                Lat= x.Lat,
                Lon= x.Lon,
                MusteriId=x.MusteriId,
                Sil= x.Sil
            }).Where(x => x.MusteriId == musteriId && x.Sil!=true).ToArray();
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;

        }
    }
}
