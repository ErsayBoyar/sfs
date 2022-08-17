using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class YeniEpostaGonderController : ApiController
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
        public async Task<IHttpActionResult> Put(string eposta, Musteri esn)
        {
            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            MailGonder mail_send = new MailGonder();
            var list = db.Musteri.Where(x => x.EPosta == eposta && x.Sil == false).SingleOrDefault();
            if (list != null)
            {
                try
                {
                    Random x = new Random();
                    string sayi = Convert.ToString(x.Next(100000, 1000000));
                    list.EPostaCheck = sayi;
                    db.SaveChanges();
                    mail_send.SendEmail(list.EPosta.ToString(), list.Ad + ' ' + list.Soyad, sayi, @"Yeni şifreniz başarıyla 
oluşturuldu.Edirne Belediyemizce geliştirilen, esnafımızı ve
halkımızı bir araya getiren Edirne Esnafım mobil uygulamasına
hoş geldiniz.", "", "", "");
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

            myError = new Error()
            {
                Status = "200",
                Message = "Başarılı",
                Data = new List<Musteri>()
                {

                }
            };
            return new ErrorResult(myError, Request);

        }
    }
}
