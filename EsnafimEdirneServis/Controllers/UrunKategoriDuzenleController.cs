using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class UrunKategoriDuzenleController : ApiController
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
        public ProductApiCollection Put(Urun esn)
        {
            var result = new ProductApiCollection();
            UrunKategori db_esnaf = new UrunKategori();
            
            var varmi = db.UrunKategori.Where(a => a.Id == esn.Id).SingleOrDefault();
            if (varmi == null)
            {

                result.Status = "500";
                result.Message = "Ürün Bulunamadı";

                return result;
            }
            else
            {
                try
                {
                    var varmii = db.UrunKategori.Where(a => a.EsnafId == varmi.EsnafId && a.Ad == esn.Ad.ToString().ToUpper() && a.Sil == false && a.Id != esn.Id).Count();
                    if (varmii > 0)
                    {
                        result.Status = "404";
                        result.Message = "Bu isimli bir kategori mevcut.";

                        return result;
                    }
                    else
                    {

                        if (esn.Sil != null)
                        {
                            varmi.Sil = Convert.ToBoolean(esn.Sil);

                        }
                        if (esn.Ad != null)
                        {
                            varmi.Ad = esn.Ad.ToString().ToUpper();
                        }
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {

                    result.Status = "500";
                    result.Message = ex.ToString();

                    return result;
                }

            }
            var apiModels = db.UrunKategori.Select(x => new Data
            {
                KategoriId = x.Id,
                EsnafId = x.EsnafId,
                Ad = x.Ad,
                Sil = x.Sil,
            }).Where(x => x.KategoriId == esn.Id).ToArray();
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }

    }
}
