using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class UrunListController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public Data[] Data { get; set; }
        }
        public class Data
        {
            public int? UrunId { get; set; }
            public string Ad { get; set; }
            public int? EsnafId { get; set; }
            public decimal? Fiyat { get; set; }
            public bool? Mevcut { get; set; }
            public bool? Sil { get; set; }
            public string Aciklama { get; set; }
            public int? KategoriId { get; set; }
        }
        EsnafimEdirneEntities db = new EsnafimEdirneEntities();
        [CustomAuthorization]
        public ProductApiCollection Get()
        {
            var result = new ProductApiCollection();
            Urun db_esnaf = new Urun();
            var apiModels = db.Urun.Select(x => new Data
            {
                UrunId = x.Id,
                EsnafId = x.EsnafId,
                Ad = x.Ad,
                Aciklama = x.Aciklama,
                Fiyat = x.Fiyat,
                KategoriId = x.KategoriId,
                Mevcut = x.Mevcut,
                Sil = x.Sil,
            }).Where(x=>x.Sil!=true).ToArray();
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;

        }
        [CustomAuthorization]
        public ProductApiCollection Get(int urunId)
        {
            var result = new ProductApiCollection();
            Urun db_esnaf = new Urun();
            var apiModels = db.Urun.Select(x => new Data
            {
                UrunId = x.Id,
                EsnafId = x.EsnafId,
                Ad = x.Ad,
                Aciklama = x.Aciklama,
                Fiyat = x.Fiyat,
                KategoriId = x.KategoriId,
                Mevcut = x.Mevcut,
                Sil = x.Sil,
            }).Where(x => x.UrunId == urunId ).ToArray();
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;

        }
    }
}
