using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class KampanyaListController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public Data[] Data { get; set; }
        }
        public class Data
        {
            public int? KampanyaId { get; set; }
            public int? EsnafId { get; set; }
            public double? Oran { get; set; }
            public DateTime? BaslamaZamani { get; set; }
            public DateTime? BitisZamani { get; set; }
            public bool? Sil { get; set; }

        }
        EsnafimEdirneEntities db = new EsnafimEdirneEntities();
        // GET /api/values
        [CustomAuthorization]
        public ProductApiCollection Get(int esnafid)
        {
            var result = new ProductApiCollection();
            var varmi = db.Kampanya.Where(x => x.EsnafId == esnafid).ToList();
            if (varmi!=null)
            {
                var apiModels = db.Kampanya.Select(x => new Data
                {
                    KampanyaId = x.Id,
                    EsnafId = x.EsnafId,
                    BaslamaZamani = x.BaslamaZamani,
                    BitisZamani = x.BitisZamani,
                    Oran = x.Oran,
                    Sil = x.Sil,
                }).Where(x => x.EsnafId == esnafid && x.Sil!=true).OrderByDescending(x => x.KampanyaId).ToArray();
                result.Data = apiModels;
                result.Status = "200";
                result.Message = "İşlem Başarılı";

                return result;
            }
            else
            {
                
                result.Data = null;
                result.Status = "500";
                result.Message = "Kayıtlı kampanya bulunamadı";

                return result;
            }

        }
    }
}
