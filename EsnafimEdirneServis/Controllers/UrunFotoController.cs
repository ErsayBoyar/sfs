using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class UrunFotoController : ApiController
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
            public List<Urun> Data { get; set; }
        }
        Error myError;

        string ad;
        [CustomAuthorization]
        public async Task<IHttpActionResult> UploadFile(HttpRequestMessage request, int urunId)
        {
            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Urun db_urun = new Urun();
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/UrunFoto");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content
                    .ReadAsMultipartAsync(provider);

                foreach (var file in provider.FileData)
                {
                    var name = file.Headers
                        .ContentDisposition
                        .FileName;

                    name = name.Trim('"');
                    ad = name.ToString();
                    var localFileName = file.LocalFileName;
                    var filePath = Path.Combine(root, DateTime.Now.ToString("yyyyMMddHHmmss") + name);

                    File.Move(localFileName, filePath);

                }
                var urun = db.Urun.Where(x => x.Id == urunId).SingleOrDefault();
                urun.FotoYol = "http://5.180.107.19:81/UrunFoto/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ad.ToString();
                db.SaveChanges();
            }
            catch (Exception e)
            {
                myError = new Error()
                {
                    Status = "404",
                    Message = e.ToString(),
                    Data = new List<Urun>()
                    {

                    }
                };
                return new ErrorResult(myError, Request);
            }
            var list = db.Urun.Where(x => x.Id == urunId).SingleOrDefault();
            myError = new Error()
            {
                Status = "200",
                Message = "Başarılı",
                Data = new List<Urun>()
                {
                  new Urun {
                      Id=list.Id,
                      Ad=list.Ad,
                      EsnafId=list.EsnafId,
                      Aciklama=list.Aciklama,
                      Mevcut=list.Mevcut,
                      Fiyat=list.Fiyat,
                      Sil=list.Sil,
                      FotoYol=list.FotoYol
                  }
                }
            };
            return new ErrorResult(myError, Request);
        }
    }
}
