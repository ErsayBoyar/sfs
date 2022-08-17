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
    public class MusteriMailOnayController : ApiController
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
        public async Task<IHttpActionResult> Put(int musteriId, Musteri esn)
        {

            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Esnaf db_esnaf = new Esnaf();
            var varmi = db.Musteri.Where(a => a.Id == musteriId).SingleOrDefault();
            if (varmi != null)
            {
                try
                {
                    if (varmi.EPostaCheck == esn.EPostaCheck)
                    {
                        varmi.EPostaOnay = true;
                        db.SaveChanges();
                    }
                    else
                    {
                        myError = new Error()
                        {
                            Status = "404",
                            Message = "Hatalı Onay Kodu",
                            Data = new List<Musteri>()
                            {

                            }
                        };
                        return new ErrorResult(myError, Request);
                    }
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
            var list = db.Musteri.Where(a => a.Id==musteriId).FirstOrDefault();
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
