using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class EsnafSiparisSayisiController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public Data[] Data { get; set; }
        }

        public class Data
        {
            public int DurumId { get; set; }
            public int? Sayi { get; set; }
            public string Durum { get; set; }
        }
        EsnafimEdirneEntities db = new EsnafimEdirneEntities();
        // GET /api/values
        [CustomAuthorization]
        public ProductApiCollection Get(int esnafId)
        {
            var result = new ProductApiCollection();
            string sql = @"select count(*) Sayi,s.DurumId,sd.Ad as Durum from Siparis s
left outer join SiparisDurum sd on sd.Id=s.DurumId
where isnull(s.Onay,'False')='True' and s.EsnafId='"+esnafId+@"'
group by s.DurumId,sd.Ad
order by COUNT(*) desc";
            var liste = db.Database.SqlQuery<Data>(sql).ToArray();
            //var apiModels = db.HizmetKategori.Select(x => new Data { Id = x.Id, Ad=x.Ad,FotoYol=x.FotoYol }).ToArray();
            result.Data = liste;

            //var status = db.Status.Any() ? 1 : 0;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
    }
}
