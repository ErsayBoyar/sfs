using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class UrunDuzenlemeController : ApiController
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
        public class Varmi
        {
            public int varmi { get; set; }
        }
        [CustomAuthorization]
        public ProductApiCollection Put(int urunId,Urun esn)
        {
            var result = new ProductApiCollection();
            Urun db_esnaf = new Urun();


            var list = db.Urun.Where(x => x.Id == urunId).SingleOrDefault();
            if (list != null)
            {

                if (esn.Fiyat!=null)
                {
                    list.Fiyat = Convert.ToDecimal(esn.Fiyat);
                }
                if (esn.Ad!=null)
                {
                    list.Ad = esn.Ad.ToString().ToUpper();
                }
                if (esn.Aciklama!=null)
                {
                    list.Aciklama = esn.Aciklama.ToString();
                }
                if (esn.KategoriId!=null)
                {
                    list.KategoriId = Convert.ToInt32(esn.KategoriId);
                }
                if (esn.Mevcut!=null)
                {
                    list.Mevcut = Convert.ToBoolean(esn.Mevcut);
                }
                if (esn.Sil!=null)
                {
                    list.Sil = Convert.ToBoolean(esn.Sil);
                }
                db.SaveChanges();

            }
            else
            {
                result.Status = "500";
                result.Message = "Ürün Bulunamadı";

                return result;
            }
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
            }).Where(x => x.UrunId == urunId).ToArray();
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;

        }
    }
}
