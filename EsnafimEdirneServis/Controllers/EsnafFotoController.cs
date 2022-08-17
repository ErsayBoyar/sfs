using EsnafimEdirneServis.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace EsnafimEdirneServis.Controllers
{
    public class EsnafFotoController : ApiController
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

        string ad;
        [CustomAuthorization]
        public async Task<IHttpActionResult> UploadFile(HttpRequestMessage request,int esnafId)
        {
            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Esnaf db_esnaf = new Esnaf();
            var ctx = HttpContext.Current;
            var root = ctx.Server.MapPath("~/EsnafFoto");
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
                var esnaf = db.Esnaf.Where(x => x.Id == esnafId).SingleOrDefault();
                esnaf.FotoYol = "http://5.180.107.19:81/EsnafFoto/" + DateTime.Now.ToString("yyyyMMddHHmmss") + ad.ToString();
                db.SaveChanges();
            }
            catch (Exception e)
            {
                myError = new Error()
                {
                    Status = "404",
                    Message = e.ToString(),
                    Data = new List<Esnaf>()
                    {

                    }
                };
                return new ErrorResult(myError, Request);
            }
            var list = db.Esnaf.Where(x => x.Id == esnafId).SingleOrDefault();
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
