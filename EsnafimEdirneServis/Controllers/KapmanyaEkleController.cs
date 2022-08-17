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
    public class KapmanyaEkleController : ApiController
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
        public class Varmi
        {
            public int varmi { get; set; }
        }
        string value;
        [ResponseType(typeof(Kampanya))]
        [CustomAuthorization]
        public ProductApiCollection Post(Kampanya esn)
        {
            var result = new ProductApiCollection();
            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Kampanya db_esnaf = new Kampanya();
            if (esn.BaslamaZamani.ToString() == "" || esn.BitisZamani.ToString() == "")
            {
                result.Status = "500";
                result.Message = "Tarihler Boş Olamaz";
                result.Data = null;
                return result;
                
            }
            int id = Convert.ToInt32(esn.Id);
            string baslangic = Convert.ToDateTime( esn.BaslamaZamani).ToString("yyyy-MM-dd HH:mm");
            string bitis = Convert.ToDateTime(esn.BitisZamani).ToString("yyyy-MM-dd HH:mm");

            string sql = @"select count(*) as Varmi from Kampanya where
                (CAST(BaslamaZamani AS Datetime) <= '" + baslangic + @"' and CAST(BitisZamani AS Datetime)>= '" + baslangic + @"')  and EsnafId = '" + esn.EsnafId + "' and isnull(Sil,'False')='False'";
            var degisken = db.Database.SqlQuery<Varmi>(sql).FirstOrDefault();
            int varmi = degisken.varmi;
            if (varmi != 0)
            {

               
                result.Status = "404";
                result.Message = "Seçili tarihler arasında başka bir kampanya var. Lütfen yeni tarihler belirleyin.";

                return result;
            }

            sql = @"select count(*) as Varmi from Kampanya where
                    (CAST(BaslamaZamani AS Datetime) <= '" + bitis + @"' and CAST(BitisZamani AS Datetime) >= '" + bitis + @"') and
                     EsnafId = '" + esn.EsnafId + "' and isnull(Sil,'False')='False'";
            degisken = db.Database.SqlQuery<Varmi>(sql).FirstOrDefault();
            varmi = degisken.varmi;
            if (varmi != 0)
            {
                
                result.Status = "404";
                result.Message = "Seçili tarihler arasında başka bir kampanya var. Lütfen yeni tarihler belirleyin.";

                return result;
            }
            sql = @"select count(*) as Varmi from Kampanya where
                        (CAST(BaslamaZamani AS Datetime) >= '" + baslangic + @"' and CAST(BitisZamani AS Datetime) <= '" + bitis + @"') and
                         EsnafId = '" + esn.EsnafId + "' and isnull(Sil,'False')='False'";
            degisken = db.Database.SqlQuery<Varmi>(sql).FirstOrDefault();
            varmi = degisken.varmi;

            if (varmi != 0)
            {
                
                result.Status = "404";
                result.Message = "Seçili tarihler arasında başka bir kampanya var. Lütfen yeni tarihler belirleyin.";

                return result;
            }

            try
            {
                db_esnaf.EsnafId = Convert.ToInt32(esn.EsnafId);
                db_esnaf.Sil = false;
                db_esnaf.BaslamaZamani = esn.BaslamaZamani;
                db_esnaf.BitisZamani = esn.BitisZamani;
                db_esnaf.Oran = esn.Oran;
                db.Kampanya.Add(db_esnaf);
                db.SaveChanges();
               

            }
            catch (Exception ex)
            {

                
                result.Status = "500";
                result.Message = ex.ToString();

                return result;
            }

            var apiModels = db.Kampanya.Select(x => new Data
            {
                KampanyaId = x.Id,
                EsnafId = x.EsnafId,
                BaslamaZamani = x.BaslamaZamani,
                BitisZamani =  x.BitisZamani,
                Oran = x.Oran,
                Sil = x.Sil,
             }).Where(x=>x.EsnafId==esn.EsnafId).OrderByDescending(x=>x.KampanyaId).ToArray();
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
    }
}
