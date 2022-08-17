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
    public class SiparisOlusturController : ApiController
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
        public SiparisOlusturController()
        {
            _modelFactory = new ModelFactory3();
        }
        int max_siparis_id;
        [CustomAuthorization]
        public ProductApiCollection Post(Data esn)
        {
            var result = new ProductApiCollection();

            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Siparis db_siparis = new Siparis();
            SiparisDetay db_detay = new SiparisDetay();
            var varmi = db.Siparis.Where(a => a.MusteriId == esn.MusteriId && a.Onay == false).SingleOrDefault();
            if (varmi == null)
            {
                db_siparis.MusteriId = Convert.ToInt32(esn.MusteriId);
                db_siparis.Onay = Convert.ToBoolean(false);
                db_siparis.EsnafId = Convert.ToInt32(esn.EsnafId);
                db_siparis.Sil = Convert.ToBoolean(false);
               
                db.Siparis.Add(db_siparis);
                db.SaveChanges();

                var max = db.Siparis.Where(x => x.MusteriId == esn.MusteriId).OrderByDescending(u => u.Id).FirstOrDefault();
                max_siparis_id = max.Id;
                string sql = @"select k.Id as Varmi,k.Oran from Esnaf e 
left outer join Kampanya k on k.EsnafId=e.Id
left outer join HizmetKategori hk on hk.Id=e.HizmetKategoriID
where k.EsnafId ='" + esn.EsnafId + @"' and isnull(k.Sil,'False')='False' and BaslamaZamani <= FORMAT( SYSDATETIME(),'yyyy-MM-dd HH:mm') and BitisZamani >= FORMAT( SYSDATETIME(),'yyyy-MM-dd HH:mm')
order by k.Id desc";
                var degisken = db.Database.SqlQuery<Varmi>(sql).FirstOrDefault();
                if (degisken==null)
                {
                    int urun_id = Convert.ToInt32(esn.UrunId);
                    var urun = db.Urun.Where(x => x.Id == urun_id).SingleOrDefault();
                    db_detay.SiparisId = max_siparis_id;
                    db_detay.UrunId = Convert.ToInt32(esn.UrunId);
                    db_detay.Adet = Convert.ToInt32(esn.Adet);
                    db_detay.Sil = Convert.ToBoolean(false);
                    db_detay.Fiyat = urun.Fiyat * Convert.ToInt32(esn.Adet);
                    db_detay.IndirimliTutar = urun.Fiyat * Convert.ToInt32(esn.Adet);
                    db_detay.IndirimTutari = 0;
                    db.SiparisDetay.Add(db_detay);
                    db.SaveChanges();
                }
                else
                {
                    int urun_id = Convert.ToInt32(esn.UrunId);
                    var urun = db.Urun.Where(x => x.Id == urun_id).SingleOrDefault();
                    db_detay.SiparisId = max_siparis_id;
                    db_detay.UrunId = Convert.ToInt32(esn.UrunId);
                    db_detay.Adet = Convert.ToInt32(esn.Adet);
                    db_detay.Sil = Convert.ToBoolean(false);
                    db_detay.Fiyat = urun.Fiyat * Convert.ToInt32(esn.Adet);
                    db_detay.IndirimliTutar = (Convert.ToDecimal( urun.Fiyat) - (urun.Fiyat * Convert.ToDecimal( degisken.Oran))) * Convert.ToInt32(esn.Adet);
                    db_detay.IndirimTutari = (urun.Fiyat* Convert.ToDecimal( degisken.Oran)) * Convert.ToInt32(esn.Adet);
                    db.SiparisDetay.Add(db_detay);
                    db.SaveChanges();
                }
                

                var siparis_detay = db.SiparisDetay.Where(x => x.SiparisId == max_siparis_id ).Sum(x => x.Fiyat);
                var siparis_detay2 = db.SiparisDetay.Where(x => x.SiparisId == max_siparis_id ).Sum(x => x.IndirimTutari);
                var siparis_detay3 = db.SiparisDetay.Where(x => x.SiparisId == max_siparis_id).Sum(x => x.IndirimliTutar);
                var siparis= db.Siparis.Where(x => x.Id==max_siparis_id).FirstOrDefault();
                siparis.ToplamTutar = siparis_detay;
                siparis.IndirimTutari = siparis_detay2;
                siparis.IndirimliTutar = siparis_detay3;
                db.SaveChanges();
            }
            else
            {
                if (varmi.EsnafId!=esn.EsnafId)
                {
                    result.Status = "404";
                    result.Message = "Sepetinizde Başka Mağazadan Ürün Bulunmakta";

                    return result;
                }
                int urun_id = Convert.ToInt32(esn.UrunId);
                var urun = db.Urun.Where(x => x.Id == urun_id).SingleOrDefault();
                max_siparis_id = varmi.Id;
                string sql = @"select k.Id as Varmi,k.Oran from Esnaf e 
left outer join Kampanya k on k.EsnafId=e.Id
left outer join HizmetKategori hk on hk.Id=e.HizmetKategoriID
where k.EsnafId ='" + esn.EsnafId + @"' and isnull(k.Sil,'False')='False' and BaslamaZamani <= FORMAT( SYSDATETIME(),'yyyy-MM-dd HH:mm') and BitisZamani >= FORMAT( SYSDATETIME(),'yyyy-MM-dd HH:mm')
order by k.Id desc";
                var degisken = db.Database.SqlQuery<Varmi>(sql).FirstOrDefault();
                var sepette_varmi = db.SiparisDetay.Where(x =>x.SiparisId==varmi.Id && x.UrunId == esn.UrunId && x.Sil == false).SingleOrDefault();
                if (sepette_varmi ==null)
                {
                    if (degisken == null)
                    {
                        db_detay.SiparisId = max_siparis_id;
                        db_detay.UrunId = Convert.ToInt32(esn.UrunId);
                        db_detay.Adet = Convert.ToInt32(esn.Adet);
                        db_detay.Sil = Convert.ToBoolean(false);
                        db_detay.Fiyat = urun.Fiyat * Convert.ToInt32(esn.Adet);
                        db_detay.IndirimliTutar = urun.Fiyat * Convert.ToInt32(esn.Adet);
                        db_detay.IndirimTutari = 0;
                        db.SiparisDetay.Add(db_detay);
                        db.SaveChanges();
                    }
                    else
                    {
                        db_detay.SiparisId = max_siparis_id;
                        db_detay.UrunId = Convert.ToInt32(esn.UrunId);
                        db_detay.Adet = Convert.ToInt32(esn.Adet);
                        db_detay.Sil = Convert.ToBoolean(false);
                        db_detay.Fiyat = urun.Fiyat * Convert.ToInt32(esn.Adet);
                        db_detay.IndirimliTutar = (Convert.ToDecimal(urun.Fiyat) - (urun.Fiyat * Convert.ToDecimal(degisken.Oran))) * Convert.ToInt32(esn.Adet);
                        db_detay.IndirimTutari = (urun.Fiyat * Convert.ToDecimal(degisken.Oran)) * Convert.ToInt32(esn.Adet);
                        db.SiparisDetay.Add(db_detay);
                        db.SaveChanges();
                    }
                }
                else
                {
                    if (degisken == null)
                    {
                        
                        
                        sepette_varmi.Adet = sepette_varmi.Adet + Convert.ToInt32(esn.Adet);
                        sepette_varmi.Fiyat = urun.Fiyat * Convert.ToInt32(sepette_varmi.Adet);
                        sepette_varmi.IndirimliTutar = urun.Fiyat * Convert.ToInt32(sepette_varmi.Adet);
                        sepette_varmi.IndirimTutari = 0;
                        db.SaveChanges();
                    }
                    else
                    {

                        sepette_varmi.Adet = sepette_varmi.Adet + Convert.ToInt32(esn.Adet);
                        sepette_varmi.Fiyat = urun.Fiyat * (sepette_varmi.Adet);
                        sepette_varmi.IndirimliTutar = (Convert.ToDecimal(urun.Fiyat) - (urun.Fiyat * Convert.ToDecimal(degisken.Oran))) * sepette_varmi.Adet;
                        sepette_varmi.IndirimTutari = (urun.Fiyat * Convert.ToDecimal(degisken.Oran)) * Convert.ToInt32(sepette_varmi.Adet);
                        db.SaveChanges();
                    }
                }

                

                var siparis_detay = db.SiparisDetay.Where(x => x.SiparisId == max_siparis_id ).Sum(x => x.Fiyat);
                var siparis_detay2 = db.SiparisDetay.Where(x => x.SiparisId == max_siparis_id ).Sum(x => x.IndirimTutari);
                var siparis_detay3 = db.SiparisDetay.Where(x => x.SiparisId == max_siparis_id ).Sum(x => x.IndirimliTutar);
                var siparis = db.Siparis.Where(x => x.Id == max_siparis_id).FirstOrDefault();
                siparis.ToplamTutar = siparis_detay;
                siparis.IndirimTutari = siparis_detay2;
                siparis.IndirimliTutar = siparis_detay3;
                db.SaveChanges();
            }



            var apiModels = db.Siparis.Where(x => x.MusteriId == esn.MusteriId && x.Id == max_siparis_id && x.Sil!=true).ToList().Select(c => _modelFactory.Createe(c));
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
    }
}
