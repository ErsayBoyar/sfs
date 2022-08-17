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
    public class MusteriAdresSilController : ApiController
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
            public List<MusteriAdres> Data { get; set; }
        }
        Error myError;
        [CustomAuthorization]
        public async Task<IHttpActionResult> Put(int adresId, int musteriId)
        {

            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            MusteriAdres db_esnaf = new MusteriAdres();
            var varmi = db.MusteriAdres.Where(a => a.Id == adresId).SingleOrDefault();
            if (varmi != null)
            {
                try
                {

                    varmi.Sil = Convert.ToBoolean(true);
                    db.SaveChanges();


                }
                catch (Exception ex)
                {

                    myError = new Error()
                    {
                        Status = "404",
                        Message = ex.ToString(),
                        Data = new List<MusteriAdres>()
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
                    Message = "Adres Bulunamadı",
                    Data = new List<MusteriAdres>()
                    {

                    }
                };
                return new ErrorResult(myError, Request);
            }



            var list = db.MusteriAdres.Where(x => x.MusteriId == musteriId && x.Sil == false).OrderByDescending(u => u.Id).FirstOrDefault();
            if (list != null)
            {
                myError = new Error()
                {
                    Status = "200",
                    Message = "Başarılı",
                    Data = new List<MusteriAdres>()
                {
                  new MusteriAdres {
                      Id=list.Id,
                      AdresAd = list.AdresAd,
                      MusteriId = Convert.ToInt32(list.MusteriId),
                      Adres = list.Adres,
                      AdresTarifi = list.AdresTarifi,
                      BinaNo = list.BinaNo,
                      Daire = list.Daire,
                      Kat = list.Kat,
                      Lat = list.Lat,
                      Lon = list.Lon,
                      Sil = list.Sil
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
                    Data = new List<MusteriAdres>()
                {
                  
                }
                };
                return new ErrorResult(myError, Request);
            }

        }
    }
}
