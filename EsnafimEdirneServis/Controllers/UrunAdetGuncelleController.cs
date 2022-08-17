using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class UrunAdetGuncelleController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public IEnumerable<SepetModel> Data { get; set; }
        }
        public class Data
        {
            public int SepetId { get; set; }
            public int SepetDetayId { get; set; }
            public int MusteriId { get; set; }
            public int EsnafId { get; set; }
            public int UrunId { get; set; }
            public int Adet { get; set; }
            public bool Onay { get; set; }
            public decimal Fiyat { get; set; }
            public string KategoriAd { get; set; }
            public string FotoYol { get; set; }
            public string Kdv { get; set; }
            public bool Sil { get; set; }

            public virtual List<SiparisDetay> SiparisDetays { get; set; }
        }
        public class Varmi
        {
            public int? varmi { get; set; }
            public double? Oran { get; set; }
        }
        ModelFactory3 _modelFactory;
        public UrunAdetGuncelleController()
        {
            _modelFactory = new ModelFactory3();
        }

        [CustomAuthorization]
        public ProductApiCollection Put(Data esn)
        {
            var result = new ProductApiCollection();

            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Siparis db_siparis = new Siparis();
            

            var sepet = db.Siparis.Where(x => x.MusteriId == esn.MusteriId && x.Onay!=true).SingleOrDefault();
            var sepet_detay = db.SiparisDetay.Where(x => x.Id == esn.SepetDetayId).SingleOrDefault();
            int urun_id = Convert.ToInt32(sepet_detay.UrunId);
            var urun = db.Urun.Where(x => x.Id == urun_id).SingleOrDefault();
            
            string sql = @"select k.Id as Varmi,k.Oran from Esnaf e 
left outer join Kampanya k on k.EsnafId=e.Id
left outer join HizmetKategori hk on hk.Id=e.HizmetKategoriID
where k.EsnafId ='" + sepet.EsnafId+@"' and isnull(k.Sil,'False')='False' and BaslamaZamani <= FORMAT( SYSDATETIME(),'yyyy-MM-dd HH:mm') and BitisZamani >= FORMAT( SYSDATETIME(),'yyyy-MM-dd HH:mm')
order by k.Id desc";
            var degisken = db.Database.SqlQuery<Varmi>(sql).FirstOrDefault();
            
            if (degisken == null)
            {
                
                sepet_detay.Adet = Convert.ToInt32(esn.Adet);
                sepet_detay.Fiyat = urun.Fiyat * Convert.ToInt32(esn.Adet);
                sepet_detay.IndirimliTutar = urun.Fiyat * Convert.ToInt32(esn.Adet);
                sepet_detay.IndirimTutari = 0;
                db.SaveChanges();
            }
            else
            {
                
                sepet_detay.Adet = Convert.ToInt32(esn.Adet);
                sepet_detay.Fiyat = urun.Fiyat * Convert.ToInt32(esn.Adet);
                sepet_detay.IndirimliTutar = (Convert.ToDecimal(urun.Fiyat) - (urun.Fiyat * Convert.ToDecimal(degisken.Oran))) * Convert.ToInt32(esn.Adet);
                sepet_detay.IndirimTutari = (urun.Fiyat * Convert.ToDecimal(degisken.Oran)) * Convert.ToInt32(esn.Adet);
                
                db.SaveChanges();
            }

            var siparis_detay = db.SiparisDetay.Where(x => x.SiparisId == sepet.Id ).Sum(x => x.Fiyat);
            var siparis_detay2 = db.SiparisDetay.Where(x => x.SiparisId == sepet.Id ).Sum(x => x.IndirimTutari);
            var siparis_detay3 = db.SiparisDetay.Where(x => x.SiparisId == sepet.Id ).Sum(x => x.IndirimliTutar);
            var siparis = db.Siparis.Where(x => x.Id == sepet.Id).FirstOrDefault();
            siparis.ToplamTutar = siparis_detay;
            siparis.IndirimTutari = siparis_detay2;
            siparis.IndirimliTutar = siparis_detay3;
            db.SaveChanges();

            var apiModels = db.Siparis.Where(x => x.Id == sepet.Id && x.Sil != true).ToList().Select(c => _modelFactory.Createe(c));
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
    }
}
