using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace EsnafimEdirneServis.Controllers
{
    public class UrunKategoriListController : ApiController
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
        [CustomAuthorization]
        public ProductApiCollection Get(int esnafId)
        {
            var result = new ProductApiCollection();
            UrunKategori db_esnaf = new UrunKategori();
            var apiModels = db.UrunKategori.Select(x => new Data
            {
                KategoriId = x.Id,
                EsnafId = x.EsnafId,
                Ad = x.Ad,
                Sil = x.Sil,
            }).Where(x => x.EsnafId == esnafId && x.Sil!=true).ToArray();
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
    }
}
