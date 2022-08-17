using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class SepetSilController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public IEnumerable<SepetModel> Data { get; set; }
        }
        public class Data
        {
            public int Id { get; set; }
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
        public SepetSilController()
        {
            _modelFactory = new ModelFactory3();
        }

        [CustomAuthorization]
        public ProductApiCollection Delete(int musteriId)
        {
            var result = new ProductApiCollection();

            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Siparis db_siparis = new Siparis();
            SiparisDetay db_detay = new SiparisDetay();
            var varmi = db.Siparis.Where(a =>  a.Onay == false && a.MusteriId==musteriId).SingleOrDefault();
            int sepet_id=varmi.Id;
            if (varmi == null)
            {
                result.Status = "500";
                result.Data = null;
                result.Message = "Sepet Bulunamadı";
                return result;
            }
            else
            {
                db.Database.ExecuteSqlCommand("delete from SiparisDetay where SiparisId='"+sepet_id+"'");
                db.SaveChanges();

                db.Siparis.Remove(varmi);
                db.SaveChanges();

            }



            var apiModels = db.Siparis.Where(x =>  x.Id == sepet_id && x.Sil!=true).ToList().Select(c => _modelFactory.Createe(c));
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
    }
}
