using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using EsnafimEdirneServis.Models;
namespace EsnafimEdirneServis.Controllers
{
    public class MusteriKayitController : ApiController
    {
        public class ErrorResult : IHttpActionResult
        {
            Error _error;
            HttpRequestMessage _request;

            public ErrorResult(Error error, HttpRequestMessage request)
            {
                _error = error;
                _request = request;
            }
            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = new HttpResponseMessage()
                {
                    Content = new ObjectContent<Error>(_error, new JsonMediaTypeFormatter()),
                    RequestMessage = _request
                };
                return Task.FromResult(response);
            }
        }

        public class Error
        {
            public string Status { get; set; }
            public string Message { get; set; }
            public List<Musteri> Data { get; set; }
        }
        Error myError;
        [CustomAuthorization]
        public async Task<IHttpActionResult> Post(Musteri esn)
        {
            string sayi, value;
            string k1, k2, k3, k4, k5, k6;
            string email, kisi_no, TCC;
            MailGonder mail_send = new MailGonder();
            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Musteri db_esnaf = new Musteri();

            var varmi = db.Musteri.Where(a => a.EPosta == esn.EPosta).SingleOrDefault();
            if (varmi == null)
            {


                try
                {
                    Random x = new Random();
                    sayi = Convert.ToString(x.Next(100000, 1000000));
                    db_esnaf.Ad = esn.Ad.ToString().ToUpper();
                    db_esnaf.Soyad = esn.Soyad.ToString().ToUpper();
                    db_esnaf.EPosta = esn.EPosta;
                    db_esnaf.Telefon = esn.Telefon.ToString();
                    db_esnaf.Sil = false;
                    db_esnaf.Sifre = esn.Sifre;
                    db_esnaf.Token = esn.Token;
                    db_esnaf.Lat = esn.Lat;
                    db_esnaf.Lon = esn.Lon;
                    db_esnaf.EPostaCheck = sayi;
                    db_esnaf.EPostaOnay = false;
                    db.Musteri.Add(db_esnaf);
                    db.SaveChanges();

                    mail_send.SendEmail(db_esnaf.EPosta.ToString(), db_esnaf.Ad.ToString() + " " + db_esnaf.Soyad, sayi, @"Hesabınız başarıyla oluşturuldu. Edirne Belediyemizce 
geliştirilen, esnafımızı ve halkımızı bir araya getiren Edirne 
Esnafım mobil uygulamasına hoş geldiniz.", "", "", "");


                }
                catch (Exception ex)
                {

                    myError = new Error()
                    {
                        Status = "404",
                        Message = ex.ToString(),
                        Data = new List<Musteri>()
                        {

                        }
                    };
                    return new ErrorResult(myError, Request);
                }

            }
            else
            {
                if (varmi.EPostaOnay == false)
                {
                    Random x = new Random();
                    sayi = Convert.ToString(x.Next(100000, 1000000));
                    var id = db.Musteri.Where(a => a.EPosta == esn.EPosta).FirstOrDefault();
//                    mail_send.SendEmail(id.EPosta.ToString(), id.Ad.ToString() + " " + id.Soyad, sayi, @"Yeni şifreniz başarıyla 
//oluşturuldu.Edirne Belediyemizce geliştirilen, esnafımızı ve
//halkımızı bir araya getiren Edirne Esnafım mobil uygulamasına
//hoş geldiniz.", "", "", "");
                    var listt = db.Musteri.Where(d => d.Id == id.Id).SingleOrDefault();
                    myError = new Error()
                    {
                        Status = "500",
                        Message = "Bu e-posta adresiyle daha önce hesap oluşturulmuş, lütfen giriş yapınız.",
                        Data = new List<Musteri>()
                        {
                          new Musteri {
                              Id=listt.Id,
                              Ad = listt.Ad,
                              Soyad = listt.Soyad,
                              EPosta = listt.EPosta,
                              Telefon = listt.Telefon,
                              Sil = listt.Sil,
                              Sifre = listt.Sifre,
                              Token = listt.Token,
                              Lat = listt.Lat,
                              Lon = listt.Lon,
                              EPostaCheck = listt.EPostaCheck,
                              EPostaOnay = listt.EPostaOnay,
                          }
                        }
                    };
                    return new ErrorResult(myError, Request);
                }
                else
                {
                    myError = new Error()
                    {
                        Status = "404",
                        Message = "Eposta Daha Önce Kayıt Edilmiş",
                        Data = new List<Musteri>()
                        {

                        }
                    };
                    return new ErrorResult(myError, Request);
                }



            }


            var list = db.Musteri.Where(a => a.EPosta == esn.EPosta).FirstOrDefault();
            myError = new Error()
            {
                Status = "200",
                Message = "Başarılı",
                Data = new List<Musteri>()
                {
                  new Musteri {
                      Id=list.Id,
                      Ad = list.Ad,
                      Soyad = list.Soyad,
                      EPosta = list.EPosta,
                      Telefon = list.Telefon,
                      Sil = list.Sil,
                      Sifre = list.Sifre,
                      Token = list.Token,
                      Lat = list.Lat,
                      Lon = list.Lon,
                      EPostaCheck = list.EPostaCheck,
                      EPostaOnay = list.EPostaOnay,
                  }
                }
            };
            return new ErrorResult(myError, Request);
        }
    }
}
