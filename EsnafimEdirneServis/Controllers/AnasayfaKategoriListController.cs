using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class AnasayfaKategoriListController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public Data[] Data { get; set; }
        }
        public class Data
        {
            public int HizmetKategoriID { get; set; }
            public int? Sayi { get; set; }
            public string Ad { get; set; }
        }
        EsnafimEdirneEntities db = new EsnafimEdirneEntities();
        // GET /api/values
        [CustomAuthorization]
        public ProductApiCollection Get()
        {

            string sql = @"select count(*) Sayi, e.HizmetKategoriID,h.Ad from HizmetKategori h
left outer join Esnaf e on e.HizmetKategoriID = h.Id
where e.HizmetKategoriID in (select HizmetKategoriID from Esnaf) and isnull(e.Aktif,'False')= 'True'
group by e.HizmetKategoriID,h.Ad
order by COUNT(*) desc";
            var liste = db.Database.SqlQuery<Data>(sql).ToArray();

            
            var result = new ProductApiCollection();
            result.Data = liste;

            //var status = db.Status.Any() ? 1 : 0;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;

        }
    }
}
