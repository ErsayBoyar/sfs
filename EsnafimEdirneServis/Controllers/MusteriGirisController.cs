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
    
    public class MusteriGirisController : ApiController
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
        string sayi;
        [CustomAuthorization]
        public async Task<IHttpActionResult> Get(string eposta, string sifre, Musteri esn)
        {
            MailGonder mail_send = new MailGonder();
            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            var list = db.Musteri.Where(x => x.EPosta == eposta && x.Sifre == sifre && x.Sil == false).SingleOrDefault();
            if (list != null)
            {
                if (list.EPostaOnay == false)
                {
                    Random x = new Random();
                    sayi = Convert.ToString(x.Next(100000, 1000000));
                    var id = db.Musteri.Where(a => a.EPosta == eposta).FirstOrDefault();
                    mail_send.SendEmail(id.EPosta.ToString(), id.Ad.ToString() + " " + id.Soyad, sayi, @"Yeni şifreniz başarıyla 
oluşturuldu.Edirne Belediyemizce geliştirilen, esnafımızı ve
halkımızı bir araya getiren Edirne Esnafım mobil uygulamasına
hoş geldiniz.", "", "", "");
                    var listt = db.Musteri.Where(d => d.Id == id.Id).SingleOrDefault();
                    listt.EPostaCheck = sayi;
                    db.SaveChanges();
                    myError = new Error()
                    {
                        Status = "500",
                        Message = "Eposta Adresi Onaylanmamış.",
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
            else
            {
                myError = new Error()
                {
                    Status = "404",
                    Message = "Kayıtlı Hesap Bulunamadı",
                    Data = new List<Musteri>()
                    {

                    }
                };
                return new ErrorResult(myError, Request);
            }

        }
    }
}
