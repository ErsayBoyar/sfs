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
    public class OneCikanlarListController : ApiController
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
            public string FotoYol { get; set; }
            public int? HizmetKategoriId { get; set; }
            public DateTime BaslamaZamani { get; set; }
            public DateTime BitisZamani { get; set; }
            public string HizmetKategori { get; set; }
            public int KampanyaId { get; set; }
            public string Lat { get; set; }
            public string Lon { get; set; }
            public string Telefon { get; set; }
            public double? Oran { get; set; }
            public int? TeslimatSuresi { get; set; }
            public bool? Online { get; set; }
        }
        EsnafimEdirneEntities db = new EsnafimEdirneEntities();


        [CustomAuthorization]
        public ProductApiCollection Get()
        {

            string sql = @"
select FORMAT( SYSDATETIME(),'dd.MM.yyyy HH:mm') ,FORMAT( GETDATE(),'dd.MM.yyyy HH:mm'),e.FotoYol,e.Online,  k.Oran,e.TeslimatSuresi,e.Telefon,e.Lat,e.Lon, e.Id as EsnafId,e.FirmaUnvan,e.KisaAd, k.BaslamaZamani,k.BitisZamani,hk.Id as HizmetKategoriId,hk.ad as HizmetKategori,k.Id as KampanyaId from Esnaf e 
left outer join Kampanya k on k.EsnafId=e.Id
left outer join HizmetKategori hk on hk.Id=e.HizmetKategoriID
where e.Id in (select EsnafId from Kampanya ) and isnull(k.Sil,'False')='False' and BaslamaZamani <= FORMAT( SYSDATETIME(),'yyyy-MM-dd HH:mm') and BitisZamani >= FORMAT( SYSDATETIME(),'yyyy-MM-dd HH:mm')
order by k.Id desc
";

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
