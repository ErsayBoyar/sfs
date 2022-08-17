using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class SepetOnayController : ApiController
    {

        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public IEnumerable<SiparisModel> Data { get; set; }
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
            public string SiparisNotu { get; set; }
            public bool Sil { get; set; }
            public int? AdresId { get; set; }
            public int? OdemeId { get; set; }
            public virtual List<SiparisDetay> SiparisDetays { get; set; }
        }
        public class Varmi
        {
            public int? varmi { get; set; }
            public double? Oran { get; set; }
        }
        ModelFactory3 _modelFactory;
        public SepetOnayController()
        {
            _modelFactory = new ModelFactory3();
        }

        [CustomAuthorization]
        public ProductApiCollection Post(Data esn)
        {
            var result = new ProductApiCollection();

            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Siparis db_siparis = new Siparis();
            SiparisDetay db_detay = new SiparisDetay();
            var varmi = db.Siparis.Where(a => a.Onay == false && a.Id == esn.Id).SingleOrDefault();
            if (varmi == null)
            {
                result.Status = "500";
                result.Data = null;
                result.Message = "Sipariş Bulunamadı veya Daha Önce Onaylanmış";
                return result;
            }
            else
            {
                if (varmi.Esnaf.Online==false)
                {
                    result.Status = "500";
                    result.Data = null;
                    result.Message = "Esnaf şu an hizmet dışı olduğu için servis verememektedir";
                    return result;

                }
                else
                {
                    string sql = @"select count(*) varmi from SiparisDetay s
left outer join Urun u on u.Id=s.UrunId
where isnull(u.Mevcut,'False')='False' and s.SiparisId='"+varmi.Id+"'";
                    var degisken = db.Database.SqlQuery<Varmi>(sql).FirstOrDefault();
                    if (degisken.varmi!=0)
                    {
                        result.Status = "500";
                        result.Data = null;
                        result.Message = "Sepetinizde şu an mevcut olmayan ürünler var";
                        return result;
                    }
                    else
                    {
                        varmi.AdresId = esn.AdresId;
                        varmi.OdemeId = esn.OdemeId;
                        varmi.DurumId = 1;
                        varmi.Onay = true;
                        varmi.SiparisNotu = esn.SiparisNotu;
                        varmi.Tarih = DateTime.Now;
                        db.SaveChanges();

                    }
                    
                }
                
            }
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization:key=AAAAOFW548c:APA91bHqbTPfPyfSFG3dNxEubrT4wg4Xs-yjva24NUVeCCVYL0DCElz4cxED2eD5COQRWoYX1oDeH5soqjwHGk6jYA6kea2l0fnEWA4oGuIffhZ-qpNjfwbc6m3qrbXd_HmSls4EHwi2");
            httpWebRequest.Method = "POST";
            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string mesaj = "";
                string uid = "";
                
                    mesaj = "Yeni Bir Siparişiniz Var.";
                    uid = varmi.Esnaf.Token;
                

                string json = string.Concat(new object[] { "{\"notification\": {\"title\":\"Esnafım Edirne\",\"body\": \"", mesaj, "\",\"badge\": 0,\"sound\": \"default\",\"click_action\": \"FCM_PLUGIN_ACTIVITY\"},\"to\" : \"", uid, "\",\"content_available\": true}" });
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            using (StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream()))
            {
                streamReader.ReadToEnd();
            }



            var apiModels = db.Siparis.Where(x => x.Id == esn.Id).ToList().Select(c => _modelFactory.Create(c));
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
    }
}
