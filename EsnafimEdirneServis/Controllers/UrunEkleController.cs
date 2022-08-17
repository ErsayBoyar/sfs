using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class UrunEkleController : ApiController
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
        public ProductApiCollection Post(Urun esn)
        {
            var result = new ProductApiCollection();
            Urun db_esnaf = new Urun();

            var varmi = db.Urun.Where(a => a.EsnafId == esn.EsnafId && a.Ad == esn.Ad && esn.Sil==false).SingleOrDefault();
            if (varmi != null)
            {

                result.Status = "500";
                result.Message = "Ürün İsmi Daha Önce Zaten Tanımlanmış";

                return result;
            }
           else
            {


                try
                {
                    db_esnaf.Ad = esn.Ad.ToString().ToUpper();
                    db_esnaf.EsnafId = esn.EsnafId;
                    db_esnaf.Sil = false;
                    db_esnaf.Aciklama = esn.Aciklama;
                    db_esnaf.Fiyat = Convert.ToDecimal(esn.Fiyat);
                    db_esnaf.KategoriId = Convert.ToInt32(esn.KategoriId);
                    db_esnaf.Mevcut = Convert.ToBoolean(esn.Mevcut);
                    db.Urun.Add(db_esnaf);
                    db.SaveChanges();


                }
                catch (Exception ex)
                {

                    result.Status = "500";
                    result.Message = ex.ToString();

                    return result;
                }

            }
            var max_id= db.Urun.Where(a => a.EsnafId == esn.EsnafId).OrderByDescending(u => u.Id).FirstOrDefault();
            var apiModels = db.Urun.Select(x => new Data
            {
                UrunId = x.Id,
                EsnafId = x.EsnafId,
                Ad = x.Ad,
                Aciklama = x.Aciklama,
                Fiyat = x.Fiyat,
                KategoriId=x.KategoriId,
                Mevcut=x.Mevcut,
                Sil = x.Sil,
            }).Where(x => x.UrunId == max_id.Id).ToArray();
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
    }
}
