using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class HizmetKategoriController : ApiController
    {
        

        public class ProductApiCollection
        {
            
            public string Status { get; set; }
            public string Message { get; set; }
            public Data[] Data { get; set; }
        }

        public class Data
        {
            public int Id { get; set; }
            public int? Sayi { get; set; }
            public string Ad { get; set; }
            public string FotoYol { get; set; }
        }
        EsnafimEdirneEntities db = new EsnafimEdirneEntities();
        // GET /api/values
        [CustomAuthorization]
        public ProductApiCollection Get()
        {
            var result = new ProductApiCollection();
            //            string sql = @"select count(*) Sayi,h.Id,h.Ad,h.FotoYol from HizmetKategori h
            //left outer join Esnaf e on e.HizmetKategoriID = h.Id
            //where e.HizmetKategoriID in (select HizmetKategoriID from Esnaf) and isnull(e.Aktif,'False')= 'True'
            //group by h.Id,h.Ad,h.FotoYol
            //order by COUNT(*) desc";
            string sql = @"select h.Id,h.Ad,h.FotoYol from HizmetKategori h
where isnull(h.Sil,'False')= 'False'
order by h.Ad";
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
