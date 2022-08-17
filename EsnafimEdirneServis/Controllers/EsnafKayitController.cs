using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace EsnafimEdirneServis.Controllers
{
    public class EsnafKayitController : ApiController
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
        Error myError;
        [CustomAuthorization]
        public async Task<IHttpActionResult> Post(Esnaf esn)
        {
            string sayi, value;
            string k1, k2, k3, k4, k5, k6;
            string email, kisi_no, TCC;
            MailGonder mail_send = new MailGonder();
            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Esnaf db_esnaf = new Esnaf();
            var varmi = db.Esnaf.Where(a => a.VergiNo == esn.VergiNo || a.EPosta == esn.EPosta).SingleOrDefault();
            if (varmi == null)
            {
                try
                {
                    Random x = new Random();
                    sayi = Convert.ToString(x.Next(100000, 1000000));
                    db_esnaf.FirmaUnvan = esn.FirmaUnvan.ToString().ToUpper();
                    db_esnaf.VergiNo = esn.VergiNo;
                    db_esnaf.KisaAd = esn.KisaAd;
                    db_esnaf.EPosta = esn.EPosta;
                    db_esnaf.Telefon = esn.Telefon.ToString();
                    db_esnaf.HizmetKategoriID = Convert.ToInt32(esn.HizmetKategoriID);
                    db_esnaf.KapidaKredi = Convert.ToBoolean(esn.KapidaKredi);
                    db_esnaf.KapidaNakit = Convert.ToBoolean(esn.KapidaNakit);
                    db_esnaf.MinSiparis = Convert.ToDecimal(esn.MinSiparis);
                    db_esnaf.Aktif = false;
                    db_esnaf.Adres = esn.Adres;
                    db_esnaf.Sifre = esn.Sifre;
                    db_esnaf.Token = esn.Token;
                    db_esnaf.Online = false;
                    db_esnaf.Lat = esn.Lat;
                    db_esnaf.Lon = esn.Lon;
                    db_esnaf.EPostaCheck = sayi;
                    db_esnaf.EpostaOnay = false;
                    db_esnaf.Hizmet1 = Convert.ToBoolean(esn.Hizmet1);
                    db_esnaf.Hizmet2 = Convert.ToBoolean(esn.Hizmet2);
                    db_esnaf.Hizmet3 = Convert.ToBoolean(esn.Hizmet3);
                    db_esnaf.Hizmet4 = Convert.ToBoolean(esn.Hizmet4);
                    db_esnaf.Hizmet5 = Convert.ToBoolean(esn.Hizmet5);
                    db_esnaf.Hizmet6 = Convert.ToBoolean(esn.Hizmet6);
                    db_esnaf.Hizmet7 = Convert.ToBoolean(esn.Hizmet7);
                    db_esnaf.HizmetBaslama1 = esn.HizmetBaslama1;
                    db_esnaf.HizmetBaslama2 = esn.HizmetBaslama2;
                    db_esnaf.HizmetBaslama3 = esn.HizmetBaslama3;
                    db_esnaf.HizmetBaslama4 = esn.HizmetBaslama4;
                    db_esnaf.HizmetBaslama5 = esn.HizmetBaslama5;
                    db_esnaf.HizmetBaslama6 = esn.HizmetBaslama6;
                    db_esnaf.HizmetBaslama7 = esn.HizmetBaslama7;
                    db_esnaf.HizmetBitis1 = esn.HizmetBitis1;
                    db_esnaf.HizmetBitis2 = esn.HizmetBitis2;
                    db_esnaf.HizmetBitis3 = esn.HizmetBitis3;
                    db_esnaf.HizmetBitis4 = esn.HizmetBitis4;
                    db_esnaf.HizmetBitis5 = esn.HizmetBitis5;
                    db_esnaf.HizmetBitis6 = esn.HizmetBitis6;
                    db_esnaf.HizmetBitis7 = esn.HizmetBitis7;
                    db_esnaf.TeslimatSuresi = esn.TeslimatSuresi;
                    db.Esnaf.Add(db_esnaf);
                    db.SaveChanges();
                    var id = db.Esnaf.Where(a => a.VergiNo == esn.VergiNo).FirstOrDefault();
                    mail_send.SendEmail(db_esnaf.EPosta.ToString(), db_esnaf.FirmaUnvan.ToString(), sayi, @"Hesabınız başarıyla oluşturuldu. Edirne Belediyemizce 
geliştirilen, esnafımızı ve halkımızı bir araya getiren Edirne 
Esnafım mobil uygulamasına hoş geldiniz.", "", "", "");


                }
                catch (Exception ex)
                {

                    myError = new Error()
                    {
                        Status = "404",
                        Message = ex.ToString(),
                        Data = new List<Esnaf>()
                        {

                        }
                    };
                    return new ErrorResult(myError, Request);
                }

            }
            else
            {
                if (varmi.EpostaOnay == false)
                {
                    Random x = new Random();
                    sayi = Convert.ToString(x.Next(100000, 1000000));
                    var id = db.Esnaf.Where(a => a.VergiNo == esn.VergiNo).FirstOrDefault();
//                    mail_send.SendEmail(id.EPosta.ToString(), id.FirmaUnvan.ToString(), sayi, @"Yeni şifreniz başarıyla 
//oluşturuldu.Edirne Belediyemizce geliştirilen, esnafımızı ve
//halkımızı bir araya getiren Edirne Esnafım mobil uygulamasına
//hoş geldiniz.", "", "", "");
                    var listt = db.Esnaf.Where(d => d.Id == id.Id).SingleOrDefault();
                    myError = new Error()
                    {
                        Status = "500",
                        Message = "Bu e-posta adresi veya vergi numarası ile daha önce hesap oluşturulmuş, lütfen giriş yapınız.",
                        Data = new List<Esnaf>()
                {
                  new Esnaf {
                      Id=listt.Id,
                      FirmaUnvan=listt.FirmaUnvan,
                      VergiNo=listt.VergiNo,
                      KisaAd=listt.KisaAd,
                      Telefon=listt.Telefon,
                      EPosta=listt.EPosta,
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
                    myError = new Error()
                    {
                        Status = "404",
                        Message = "Vergi Numarası veya Eposta Daha Önce Kaydedilmiş ",
                        Data = new List<Esnaf>()
                        {

                        }
                    };
                    return new ErrorResult(myError, Request);
                }

            }

            var list = db.Esnaf.Where(x => x.VergiNo == esn.VergiNo).SingleOrDefault();
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
