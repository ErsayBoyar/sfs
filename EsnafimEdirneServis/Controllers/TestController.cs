using EsnafimEdirneServis.Models;
using Newtonsoft.Json;
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
    public class TestController : ApiController
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

        public IHttpActionResult Put(int musteriId)
        {

            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Musteri db_esnaf = new Musteri();
            var list = db.Musteri.Where(x => x.Id == musteriId).SingleOrDefault();

            Error myError = new Error()
            {
                Status = "200",
                Message = "Başarılı",
                Data = new List<Musteri>()
                {
                  new Musteri {
                      Id =list.Id,
                      Ad =list.Ad,
                      Soyad=list.Soyad,
                      EPosta= list.EPosta
                  }
                }
            };
           
            return new ErrorResult(myError, Request);



        }
    }
}
