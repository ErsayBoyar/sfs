using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class EsnafCagirController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public Data[] Data { get; set; }
        }

        public class Data
        {
            public int Id { get; set; }
            public string FirmaUnvan { get; set; }
            public string KisaAd { get; set; }
            public string FotoYol { get; set; }
            public string Telefon { get; set; }
            public string Adres { get; set; }
            public decimal? MinSiparis { get; set; }
            public bool? Hizmet1 { get; set; }
            public bool? Hizmet2 { get; set; }
            public bool? Hizmet3 { get; set; }
            public bool? Hizmet4 { get; set; }
            public bool? Hizmet5 { get; set; }
            public bool? Hizmet6 { get; set; }
            public bool? Hizmet7 { get; set; }
            public string HizmetBaslama1 { get; set; }
            public string HizmetBaslama2 { get; set; }
            public string HizmetBaslama3 { get; set; }
            public string HizmetBaslama4 { get; set; }
            public string HizmetBaslama5 { get; set; }
            public string HizmetBaslama6 { get; set; }
            public string HizmetBaslama7 { get; set; }
            public string HizmetBitis1 { get; set; }
            public string HizmetBitis2 { get; set; }
            public string HizmetBitis3 { get; set; }
            public string HizmetBitis4 { get; set; }
            public string HizmetBitis5 { get; set; }
            public string HizmetBitis6 { get; set; }
            public string HizmetBitis7 { get; set; }
            public bool? KapidaNakit { get; set; }
            public bool? KapidaKredi { get; set; }
            public bool? Online { get; set; }
            public string Lat { get; set; }
            public string Lon { get; set; }
            public int? TeslimatSuresi { get; set; }
            public int? HizmetKategoriId { get; set; }
        }
       
            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
        // GET /api/values
        [CustomAuthorization]
        public ProductApiCollection Get()
        {
            var result = new ProductApiCollection();
            var apiModels = db.Esnaf.Select(x => new Data {
                Id = x.Id,
                FirmaUnvan = x.FirmaUnvan,
                FotoYol = x.FotoYol,
                Telefon=x.Telefon,
                Adres=x.Adres,
                MinSiparis=x.MinSiparis,
                Hizmet1=x.Hizmet1,
                Hizmet2=x.Hizmet2,
                Hizmet3=x.Hizmet3,
                Hizmet4=x.Hizmet4,
                Hizmet5=x.Hizmet5,
                Hizmet6=x.Hizmet6,
                Hizmet7=x.Hizmet7,
                HizmetBaslama1=x.HizmetBaslama1.ToString(),
                HizmetBaslama2 = x.HizmetBaslama2.ToString(),
                HizmetBaslama3 = x.HizmetBaslama3.ToString(),
                HizmetBaslama4 = x.HizmetBaslama4.ToString(),
                HizmetBaslama5 = x.HizmetBaslama5.ToString(),
                HizmetBaslama6 = x.HizmetBaslama6.ToString(),
                HizmetBaslama7 = x.HizmetBaslama7.ToString(),
                HizmetBitis1 = x.HizmetBitis1.ToString(),
                HizmetBitis2 = x.HizmetBitis2.ToString(),
                HizmetBitis3 = x.HizmetBitis3.ToString(),
                HizmetBitis4 = x.HizmetBitis4.ToString(),
                HizmetBitis5 = x.HizmetBitis5.ToString(),
                HizmetBitis6 = x.HizmetBitis6.ToString(),
                HizmetBitis7 = x.HizmetBitis7.ToString(),
                HizmetKategoriId=x.HizmetKategoriID,
                KapidaKredi=x.KapidaKredi,
                 KapidaNakit=x.KapidaNakit,
                 KisaAd=x.KisaAd,
                 Lat=x.Lat,
                 Lon=x.Lon,
                 TeslimatSuresi=x.TeslimatSuresi,
                 Online=x.Online
                 
            }).ToArray();
            result.Data = apiModels;

            //var status = db.Status.Any() ? 1 : 0;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
        [CustomAuthorization]
        public ProductApiCollection Get(int id)
        {
            var result = new ProductApiCollection();
            var apiModels = db.Esnaf.Where(x=>x.Id==id).Select(x => new Data
            {
                Id = x.Id,
                FirmaUnvan = x.FirmaUnvan,
                FotoYol = x.FotoYol,
                Telefon = x.Telefon,
                Adres = x.Adres,
                MinSiparis = x.MinSiparis,
                Hizmet1 = x.Hizmet1,
                Hizmet2 = x.Hizmet2,
                Hizmet3 = x.Hizmet3,
                Hizmet4 = x.Hizmet4,
                Hizmet5 = x.Hizmet5,
                Hizmet6 = x.Hizmet6,
                Hizmet7 = x.Hizmet7,
                HizmetBaslama1 = x.HizmetBaslama1.ToString(),
                HizmetBaslama2 = x.HizmetBaslama2.ToString(),
                HizmetBaslama3 = x.HizmetBaslama3.ToString(),
                HizmetBaslama4 = x.HizmetBaslama4.ToString(),
                HizmetBaslama5 = x.HizmetBaslama5.ToString(),
                HizmetBaslama6 = x.HizmetBaslama6.ToString(),
                HizmetBaslama7 = x.HizmetBaslama7.ToString(),
                HizmetBitis1 = x.HizmetBitis1.ToString(),
                HizmetBitis2 = x.HizmetBitis2.ToString(),
                HizmetBitis3 = x.HizmetBitis3.ToString(),
                HizmetBitis4 = x.HizmetBitis4.ToString(),
                HizmetBitis5 = x.HizmetBitis5.ToString(),
                HizmetBitis6 = x.HizmetBitis6.ToString(),
                HizmetBitis7 = x.HizmetBitis7.ToString(),
                HizmetKategoriId = x.HizmetKategoriID,
                KapidaKredi = x.KapidaKredi,
                KapidaNakit = x.KapidaNakit,
                KisaAd = x.KisaAd,
                Lat = x.Lat,
                Lon = x.Lon,
                TeslimatSuresi = x.TeslimatSuresi,
                Online = x.Online
            }).ToArray();
            result.Data = apiModels;

            //var status = db.Status.Any() ? 1 : 0;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
    }
}
