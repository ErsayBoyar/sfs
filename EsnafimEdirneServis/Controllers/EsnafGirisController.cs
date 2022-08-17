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
    public class EsnafGirisController : ApiController
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
            public List<Esnaf> Data { get; set; }
        }


        string value;
        string status;
        string mesaj;
        string sayi;
        [CustomAuthorization]
        public IHttpActionResult Get(string eposta, string sifre, Esnaf esn)
        {
            MailGonder mail_send = new MailGonder();

            Error myError;
            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Esnaf db_esnaf = new Esnaf();
            var list = db.Esnaf.Where(x => x.EPosta == eposta && x.Sifre == sifre ).SingleOrDefault();
            if (list == null)
            {
                myError = new Error()
                {
                    Status = "404",
                    Message = "Hatalı Kullanıcı Mail veya Şifre ",
                    Data = new List<Esnaf>()
                    {

                    }
                };
                return new ErrorResult(myError, Request);
            }
            else
            {
                if (list.EpostaOnay == false)
                {
                    Random x = new Random();
                    sayi = Convert.ToString(x.Next(100000, 1000000));
                   
                    mail_send.SendEmail(list.EPosta.ToString(), list.FirmaUnvan.ToString(), sayi, @"Yeni şifreniz başarıyla 
oluşturuldu.Edirne Belediyemizce geliştirilen, esnafımızı ve
halkımızı bir araya getiren Edirne Esnafım mobil uygulamasına
hoş geldiniz.", "", "", "");
                    var listt = db.Esnaf.Where(d => d.Id == list.Id).SingleOrDefault();
                    listt.EPostaCheck = sayi;
                    db.SaveChanges();
                    myError = new Error()
                    {
                        Status = "500",
                        Message = "Eposta Adresi Onaylanmamış.",
                        Data = new List<Esnaf>()
                {
                  new Esnaf {
                      Id=listt.Id,
                      FirmaUnvan=listt.FirmaUnvan,
                      VergiNo=listt.VergiNo,
                      KisaAd=listt.KisaAd,
                      Telefon=listt.Telefon,
                      EPosta=listt.EPosta,
                      EPostaCheck=listt.EPostaCheck,
                      HizmetKategoriID=listt.HizmetKategoriID,
                      KapidaKredi=listt.KapidaKredi,
                      KapidaNakit=listt.KapidaNakit,
                      MinSiparis=listt.MinSiparis,
                      Aktif=listt.Aktif,
                      Adres=listt.Adres,
                      Sifre=listt.Sifre,
                      Lat=listt.Lat,
                      Lon=listt.Lon,
                      Online=listt.Online,
                      FotoYol=listt.FotoYol,
                      EpostaOnay=listt.EpostaOnay,
                      Hizmet1=listt.Hizmet1,
                      Hizmet2=listt.Hizmet2,
                      Hizmet3=listt.Hizmet3,
                      Hizmet4=listt.Hizmet4,
                      Hizmet5=listt.Hizmet5,
                      Hizmet6=listt.Hizmet6,
                      Hizmet7=listt.Hizmet7,
                      HizmetBaslama1=listt.HizmetBaslama1,
                      HizmetBaslama2=listt.HizmetBaslama2,
                      HizmetBaslama3=listt.HizmetBaslama3,
                      HizmetBaslama4=listt.HizmetBaslama4,
                      HizmetBaslama5=listt.HizmetBaslama5,
                      HizmetBaslama6=listt.HizmetBaslama6,
                      HizmetBaslama7=listt.HizmetBaslama7,
                      HizmetBitis1=listt.HizmetBitis1,
                      HizmetBitis2=listt.HizmetBitis2,
                      HizmetBitis3=listt.HizmetBitis3,
                      HizmetBitis4=listt.HizmetBitis4,
                      HizmetBitis5=listt.HizmetBitis5,
                      HizmetBitis6=listt.HizmetBitis6,
                      HizmetBitis7=listt.HizmetBitis7,
                      TeslimatSuresi=listt.TeslimatSuresi
                  }
                }
                    };
                    return new ErrorResult(myError, Request);
                }
                else
                {
                    if (list.Aktif==false)
                    {
                        myError = new Error()
                        {
                            Status = "404",
                            Message = "Hesabının Belediye Tarafından Henüz Onaylanmamıştır. ",
                            Data = new List<Esnaf>()
                            {

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
                            Data = new List<Esnaf>()
                {
                  new Esnaf {
                      Id=list.Id,
                      FirmaUnvan=list.FirmaUnvan,
                      VergiNo=list.VergiNo,
                      KisaAd=list.KisaAd,
                      Telefon=list.Telefon,
                      EPosta=list.EPosta,
                      EPostaCheck=list.EPostaCheck,
                      HizmetKategoriID=list.HizmetKategoriID,
                      KapidaKredi=list.KapidaKredi,
                      KapidaNakit=list.KapidaNakit,
                      MinSiparis=list.MinSiparis,
                      Aktif=list.Aktif,
                      Adres=list.Adres,
                      Sifre=list.Sifre,
                      Lat=list.Lat,
                      Lon=list.Lon,
                      Online=list.Online,
                      FotoYol=list.FotoYol,
                      EpostaOnay=list.EpostaOnay,
                      Hizmet1=list.Hizmet1,
                      Hizmet2=list.Hizmet2,
                      Hizmet3=list.Hizmet3,
                      Hizmet4=list.Hizmet4,
                      Hizmet5=list.Hizmet5,
                      Hizmet6=list.Hizmet6,
                      Hizmet7=list.Hizmet7,
                      HizmetBaslama1=list.HizmetBaslama1,
                      HizmetBaslama2=list.HizmetBaslama2,
                      HizmetBaslama3=list.HizmetBaslama3,
                      HizmetBaslama4=list.HizmetBaslama4,
                      HizmetBaslama5=list.HizmetBaslama5,
                      HizmetBaslama6=list.HizmetBaslama6,
                      HizmetBaslama7=list.HizmetBaslama7,
                      HizmetBitis1=list.HizmetBitis1,
                      HizmetBitis2=list.HizmetBitis2,
                      HizmetBitis3=list.HizmetBitis3,
                      HizmetBitis4=list.HizmetBitis4,
                      HizmetBitis5=list.HizmetBitis5,
                      HizmetBitis6=list.HizmetBitis6,
                      HizmetBitis7=list.HizmetBitis7,
                      TeslimatSuresi=list.TeslimatSuresi
                  }
                }
                        };
                        return new ErrorResult(myError, Request);
                    }
                    
                }
            }



           

        }
    }
}
