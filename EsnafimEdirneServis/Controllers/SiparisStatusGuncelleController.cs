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
    public class SiparisStatusGuncelleController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public IEnumerable<SiparisModel> Data { get; set; }
        }
        public class Data
        {
            public int SiparisId { get; set; }
            public int SepetDetayId { get; set; }
            public int MusteriId { get; set; }
            public int EsnafId { get; set; }
            public int UrunId { get; set; }
            public int Adet { get; set; }
            public int DurumId { get; set; }
            public bool Onay { get; set; }
            public decimal Fiyat { get; set; }
            public string KategoriAd { get; set; }
            public string FotoYol { get; set; }
            public string Kdv { get; set; }
            public bool Sil { get; set; }

            public virtual List<SiparisDetay> SiparisDetays { get; set; }
        }
       
        ModelFactory3 _modelFactory;
        public SiparisStatusGuncelleController()
        {
            _modelFactory = new ModelFactory3();
        }

        [CustomAuthorization]
        public ProductApiCollection Put(Data esn)
        {
            var result = new ProductApiCollection();

            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Siparis db_siparis = new Siparis();


            var sepet = db.Siparis.Where(x => x.Id == esn.SiparisId && x.Onay == true).SingleOrDefault();
            if (sepet==null)
            {
                result.Status = "500";
                result.Data = null;
                result.Message = "Sipariş Bulunamadı";
                return result;
            }
            else
            {
                if (esn.DurumId!=null)
                {
                    sepet.DurumId = esn.DurumId;
                    db.SaveChanges();
                }
            }

          
            
            
            

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add("Authorization:key=AAAAOFW548c:APA91bHqbTPfPyfSFG3dNxEubrT4wg4Xs-yjva24NUVeCCVYL0DCElz4cxED2eD5COQRWoYX1oDeH5soqjwHGk6jYA6kea2l0fnEWA4oGuIffhZ-qpNjfwbc6m3qrbXd_HmSls4EHwi2");
            httpWebRequest.Method = "POST";
            using (StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string mesaj="";
                string uid = "";
                if (esn.DurumId == 1)
                {
                    mesaj = "Siparişiniz Hazırlanıyor.";
                    uid = sepet.Musteri.Token;

                }
                else if (esn.DurumId == 2)
                {
                    mesaj = "Siparişiniz Yola Çıktı.";
                    uid = sepet.Musteri.Token;
                }
                else if (esn.DurumId == 3)
                {
                    mesaj = "Siparişiniz Teslim Edildi.";
                    uid = sepet.Musteri.Token;
                }
                else if (esn.DurumId == 4)
                {
                    mesaj = "Sipariş İptal Edildi.";
                    uid = sepet.Esnaf.Token;
                }

                string json = string.Concat(new object[] { "{\"notification\": {\"title\":\"Esnafım Edirne\",\"body\": \"", mesaj, "\",\"badge\": 0,\"sound\": \"default\",\"click_action\": \"FCM_PLUGIN_ACTIVITY\"},\"to\" : \"", uid, "\",\"content_available\": true}" });
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            using (StreamReader streamReader = new StreamReader(((HttpWebResponse)httpWebRequest.GetResponse()).GetResponseStream()))
            {
                streamReader.ReadToEnd();
            }

            var apiModels = db.Siparis.Where(x => x.Id == esn.SiparisId && x.Sil != true).ToList().Select(c => _modelFactory.Create(c));
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
    }
}
