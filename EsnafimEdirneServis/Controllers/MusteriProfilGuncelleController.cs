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
    public class MusteriProfilGuncelleController : ApiController
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
        public async Task<IHttpActionResult> Put(Musteri esn)
        {

            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Musteri db_esnaf = new Musteri();
            var varmi = db.Musteri.Where(a => a.Id == esn.Id).SingleOrDefault();
            if (varmi != null)
            {
                try
                {
                    if (esn.Ad!=null)
                    {
                        varmi.Ad = esn.Ad;

                    }
                    if (esn.Soyad!=null)
                    {
                        varmi.Soyad = esn.Soyad;
                    }
                    if (esn.Telefon!=null)
                    {
                        varmi.Telefon = esn.Telefon;
                    }
                    if (esn.Sifre!=null)
                    {
                        varmi.Sifre = esn.Sifre;
                    }
                    if (esn.Lat!=null)
                    {
                        varmi.Lat = esn.Lat;
                    }
                    if (esn.Lon!=null)
                    {
                        varmi.Lon = esn.Lon;
                    }
                    
                    db.SaveChanges();


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



            var list = db.Musteri.Where(a => a.Id == esn.Id).FirstOrDefault();
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
