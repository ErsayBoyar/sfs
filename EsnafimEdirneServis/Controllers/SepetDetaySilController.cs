using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class SepetDetaySilController : ApiController
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
        public SepetDetaySilController()
        {
            _modelFactory = new ModelFactory3();
        }

        [CustomAuthorization]
        public ProductApiCollection Delete(int sepetDetayId)
        {
            var result = new ProductApiCollection();

            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Siparis db_siparis = new Siparis();
            SiparisDetay db_detay = new SiparisDetay();
            var varmi = db.SiparisDetay.Where(a => a.Id == sepetDetayId).SingleOrDefault();
            int siparis_id = Convert.ToInt32(varmi.SiparisId);
            if (varmi == null)
            {
                result.Status = "500";
                result.Data = null;
                result.Message = "Ürün Bulunamadı";
                return result;
            }
            else
            {

                db.SiparisDetay.Remove(varmi);
                db.SaveChanges();
            }

            var urun_sayisi= db.SiparisDetay.Where(a => a.SiparisId == siparis_id).Count();
            if (urun_sayisi==0)
            {
                var silinecek = new Siparis { Id = Convert.ToInt32(siparis_id) };
                db.Entry(silinecek).State = EntityState.Deleted;
                db.SaveChanges();
            }
            else
            {
                var siparis_detay = db.SiparisDetay.Where(x => x.SiparisId == siparis_id).Sum(x => x.Fiyat);
                var siparis_detay2 = db.SiparisDetay.Where(x => x.SiparisId == siparis_id).Sum(x => x.IndirimTutari);
                var siparis_detay3 = db.SiparisDetay.Where(x => x.SiparisId == siparis_id).Sum(x => x.IndirimliTutar);
                var siparis = db.Siparis.Where(x => x.Id == siparis_id).FirstOrDefault();
                siparis.ToplamTutar = siparis_detay;
                siparis.IndirimTutari = siparis_detay2;
                siparis.IndirimliTutar = siparis_detay3;
                db.SaveChanges();
            }

            var apiModels = db.Siparis.Where(x => x.Id == siparis_id && x.Sil!=true).ToList().Select(c => _modelFactory.Createe(c));
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
    }
}
