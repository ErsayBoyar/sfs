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
using System.Web.Http.Description;

namespace EsnafimEdirneServis.Controllers
{
    public class EsnafSifreSifirController : ApiController
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
        public async Task<IHttpActionResult> Put(string eposta, Esnaf esn)
        {
            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            MailGonder mail_send = new MailGonder();
            var list = db.Esnaf.Where(x => x.EPosta == eposta && x.Aktif == true && x.EpostaOnay==true).SingleOrDefault();
            if (list != null)
            {
                try
                {
                    Random x = new Random();
                    string sayi = Convert.ToString(x.Next(100000, 1000000));
                    list.Sifre = sayi;
                    db.SaveChanges();
                    mail_send.SendEmail(list.EPosta.ToString(), list.FirmaUnvan, sayi, @"Yeni şifreniz başarıyla 
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
                        Data = new List<Esnaf>()
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
                    Message = "Kayıtlı Esnaf Bulunamadı",
                    Data = new List<Esnaf>()
                    {

                    }
                };
                return new ErrorResult(myError, Request);
            }
            myError = new Error()
            {
                Status = "200",
                Message = "Başarılı",
                Data = new List<Esnaf>()
                {
                  
                }
            };
            return new ErrorResult(myError, Request);


        }
    }
}
