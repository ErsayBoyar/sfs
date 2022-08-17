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
    public class SearchListController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public Data[] Data { get; set; }
        }
        public class Data
        {
            public int EsnafId { get; set; }
            public string FirmaUnvan { get; set; }
            public string KisaAd { get; set; }
            public int UrunId { get; set; }
            public string Urun { get; set; }
            public int? TeslimatSuresi { get; set; }
            public bool? Online { get; set; }
        }
        EsnafimEdirneEntities db = new EsnafimEdirneEntities();
        [CustomAuthorization]
        public ProductApiCollection Get(string keyword)
        {

            string sql = @"
select e.Online, e.Id as EsnafId,u.Id as UrunId,e.KisaAd,e.FirmaUnvan,Ad as Urun,e.TeslimatSuresi from Urun u
left outer join Esnaf e on u.EsnafId=e.Id
where u.Sil='False' and e.Aktif='True'  and  (e.FirmaUnvan like '%" + keyword + "%' or e.KisaAd like '%" + keyword + "%' or u.Ad like '%" + keyword + "%') ";

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
