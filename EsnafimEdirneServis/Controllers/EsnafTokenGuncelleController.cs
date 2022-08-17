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
    public class EsnafTokenGuncelleController : ApiController
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
        public async Task<IHttpActionResult> Put(Int32 esnafId, string token, Esnaf esn)
        {
            EsnafimEdirneEntities db = new EsnafimEdirneEntities();

            var list = db.Esnaf.Where(x => x.Id == esnafId && x.Aktif == true).SingleOrDefault();
            if (list != null)
            {
                
                list.Token = token;
                db.SaveChanges();
                

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
