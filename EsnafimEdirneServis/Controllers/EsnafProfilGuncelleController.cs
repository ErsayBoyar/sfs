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
    public class EsnafProfilGuncelleController : ApiController
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
        public async Task<IHttpActionResult> Put(Esnaf esn)
        {

            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            Esnaf db_esnaf = new Esnaf();
            var varmi = db.Esnaf.Where(a => a.Id == esn.Id).SingleOrDefault();
            if (varmi != null)
            {
                try
                {
                    if (esn.KisaAd!=null)
                    {
                        varmi.KisaAd = esn.KisaAd;
                    }
                    if (esn.Telefon!=null)
                    {
                        varmi.Telefon = esn.Telefon.ToString();
                    }
                    if (esn.HizmetKategoriID!=null)
                    {
                        varmi.HizmetKategoriID = Convert.ToInt32(esn.HizmetKategoriID);
                    }
                    if (esn.KapidaKredi!=null)
                    {
                        varmi.KapidaKredi = Convert.ToBoolean(esn.KapidaKredi);
                    }
                    if (esn.KapidaNakit!=null)
                    {
                        varmi.KapidaNakit = Convert.ToBoolean(esn.KapidaNakit);
                    }
                    if (esn.MinSiparis!=null)
                    {
                        varmi.MinSiparis = Convert.ToDecimal(esn.MinSiparis);
                    }
                    if (esn.Adres!=null)
                    {
                        varmi.Adres = esn.Adres;
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
                    if (esn.Hizmet1!=null)
                    {
                        varmi.Hizmet1 = Convert.ToBoolean(esn.Hizmet1);
                    }
                    if (esn.Hizmet2 != null)
                    {
                        varmi.Hizmet2 = Convert.ToBoolean(esn.Hizmet2);
                    }
                    if (esn.Hizmet3 != null)
                    {
                        varmi.Hizmet3 = Convert.ToBoolean(esn.Hizmet3);
                    }
                    if (esn.Hizmet4 != null)
                    {
                        varmi.Hizmet4 = Convert.ToBoolean(esn.Hizmet4);
                    }
                    if (esn.Hizmet5 != null)
                    {
                        varmi.Hizmet5 = Convert.ToBoolean(esn.Hizmet5);
                    }
                    if (esn.Hizmet6 != null)
                    {
                        varmi.Hizmet6 = Convert.ToBoolean(esn.Hizmet6);
                    }
                    if (esn.Hizmet7 != null)
                    {
                        varmi.Hizmet7 = Convert.ToBoolean(esn.Hizmet7);
                    }
                    if (esn.HizmetBaslama1 != null)
                    {
                        varmi.HizmetBaslama1 = esn.HizmetBaslama1;
                    }
                    if (esn.HizmetBaslama2 != null)
                    {
                        varmi.HizmetBaslama2 = esn.HizmetBaslama2;
                    }
                    if (esn.HizmetBaslama3 != null)
                    {
                        varmi.HizmetBaslama3 = esn.HizmetBaslama3;
                    }
                    if (esn.HizmetBaslama4 != null)
                    {
                        varmi.HizmetBaslama4 = esn.HizmetBaslama4;
                    }
                    if (esn.HizmetBaslama5 != null)
                    {
                        varmi.HizmetBaslama5 = esn.HizmetBaslama5;
                    }
                    if (esn.HizmetBaslama6 != null)
                    {
                        varmi.HizmetBaslama6 = esn.HizmetBaslama6;
                    }
                    if (esn.HizmetBaslama7 != null)
                    {
                        varmi.HizmetBaslama7 = esn.HizmetBaslama7;
                    }
                    if (esn.HizmetBitis1 != null)
                    {
                        varmi.HizmetBitis1 = esn.HizmetBitis1;
                    }
                    if (esn.HizmetBitis2 != null)
                    {
                        varmi.HizmetBitis2 = esn.HizmetBitis2;
                    }
                    if (esn.HizmetBitis3 != null)
                    {
                        varmi.HizmetBitis3 = esn.HizmetBitis3;
                    }
                    if (esn.HizmetBitis4 != null)
                    {
                        varmi.HizmetBitis4 = esn.HizmetBitis4;
                    }
                    if (esn.HizmetBitis5 != null)
                    {
                        varmi.HizmetBitis5 = esn.HizmetBitis5;
                    }
                    if (esn.HizmetBitis6 != null)
                    {
                        varmi.HizmetBitis6 = esn.HizmetBitis6;
                    }
                    if (esn.HizmetBitis7 != null)
                    {
                        varmi.HizmetBitis7 = esn.HizmetBitis7;
                    }
                    if (esn.TeslimatSuresi != null)
                    {
                        varmi.TeslimatSuresi = esn.TeslimatSuresi;
                    }
                    if (esn.Online != null)
                    {
                        varmi.Online = esn.Online;
                    }

                    db.SaveChanges();


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



            var list = db.Esnaf.Where(x => x.Id == esn.Id).SingleOrDefault();
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
