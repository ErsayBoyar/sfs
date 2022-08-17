using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EsnafimEdirneServis.Models
{
    public class ModelFactory
    {
        EsnafimEdirneEntities db = new EsnafimEdirneEntities();
        string sql = @"
select FORMAT( SYSDATETIME(),'dd.MM.yyyy HH:mm') ,FORMAT( GETDATE(),'dd.MM.yyyy HH:mm'),  k.Oran,e.TeslimatSuresi,e.Telefon,e.Lat,e.Lon, e.Id as EsnafId,e.FirmaUnvan,e.KisaAd, k.BaslamaZamani,k.BitisZamani,hk.Id as HizmetKategoriId,hk.ad as HizmetKategori,k.Id as KampanyaId from Esnaf e 
left outer join Kampanya k on k.EsnafId=e.Id
left outer join HizmetKategori hk on hk.Id=e.HizmetKategoriID
where e.Id in (select EsnafId from Kampanya ) and isnull(k.Sil,'False')='False' and BaslamaZamani <= FORMAT( SYSDATETIME(),'yyyy-MM-dd HH:mm') and BitisZamani >= FORMAT( SYSDATETIME(),'yyyy-MM-dd HH:mm')
order by k.Id desc
";
        public EsnafModel Create(Esnaf esnaf )
        {
            return new EsnafModel()
            {
                EsnafId = esnaf.Id,
                KisaAd = esnaf.KisaAd,
                FotoYol=esnaf.FotoYol,
                Telefon=esnaf.Telefon,
                Adres=esnaf.Adres,
                MinSiparis=esnaf.MinSiparis,
                TeslimatSuresi=esnaf.TeslimatSuresi,
                HizmetKategoriID=esnaf.HizmetKategoriID,
                Online=esnaf.Online,
                Lat=esnaf.Lat,
                Lon=esnaf.Lon,
                Kampanya= db.Database.SqlQuery<KampanyaModel>(sql).Where(x=>x.EsnafId==esnaf.Id).ToList()
                //Kampanya=esnaf.Kampanya.Select(h => Create(h))
            };
        }
        
        public KampanyaModel Create(Kampanya kampanya)
        {
            return new KampanyaModel()
            {
                KampanyaId = kampanya.Id,
                BaslamaZamani = Convert.ToDateTime( kampanya.BaslamaZamani),
                BitisZamani = Convert.ToDateTime(kampanya.BitisZamani),
                Oran=kampanya.Oran
            };
        }
        
    }
}