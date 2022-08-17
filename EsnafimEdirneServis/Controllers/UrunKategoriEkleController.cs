using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class UrunKategoriEkleController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public Data[] Data { get; set; }
        }
        public class Data
        {
            public int? KategoriId { get; set; }
            public string Ad { get; set; }
            public int? EsnafId { get; set; }
            public bool? Sil { get; set; }
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
            UrunKategori db_esnaf = new UrunKategori();
            string ad = esn.Ad.ToString().ToUpper();
            var varmi = db.UrunKategori.Where(a => a.EsnafId == esn.EsnafId && a.Ad ==ad  && a.Sil == false).Count();
            if (varmi > 0)
            {

                result.Status = "404";
                result.Message = "Bu isimli bir kategori mevcut.";

                return result;
            }
            else
            {


                try
                {
                    db_esnaf.Ad = esn.Ad.ToString().ToUpper();
                    db_esnaf.EsnafId = esn.EsnafId;
                    db_esnaf.Sil = false;
                    db.UrunKategori.Add(db_esnaf);
                    db.SaveChanges();


                }
                catch (Exception ex)
                {

                    result.Status = "500";
                    result.Message = ex.ToString();

                    return result;
                }

            }
            var max_id = db.UrunKategori.Where(a => a.EsnafId == esn.EsnafId).OrderByDescending(u => u.Id).FirstOrDefault();
            var apiModels = db.UrunKategori.Select(x => new Data
            {
                KategoriId=x.Id,
                EsnafId = x.EsnafId,
                Ad = x.Ad,
                Sil = x.Sil,
            }).Where(x => x.KategoriId == max_id.Id && x.Sil!=true).ToArray();
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
    }
}
