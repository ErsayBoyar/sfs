using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class EsnafSiparisListController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public IEnumerable<SiparisModel> Data { get; set; }

        }
        ModelFactory3 _modelFactory;
        public EsnafSiparisListController()
        {
            _modelFactory = new ModelFactory3();
        }
        [CustomAuthorization]
        public ProductApiCollection Get(int esnafId)
        {
            var result = new ProductApiCollection();
            EsnafimEdirneEntities cr = new EsnafimEdirneEntities();
            var apiModels = cr.Siparis.Where(x => x.EsnafId == esnafId && x.Onay == true).ToList().Select(c => _modelFactory.Create(c)).OrderByDescending(x => x.SiparisId);
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
        [CustomAuthorization]
        public ProductApiCollection Get(int esnafId,  int durumId)
        {
            var result = new ProductApiCollection();
            EsnafimEdirneEntities cr = new EsnafimEdirneEntities();
            var apiModels = cr.Siparis.Where(x => x.EsnafId == esnafId && x.Onay == true && x.DurumId==durumId).ToList().Select(c => _modelFactory.Create(c)).OrderByDescending(x => x.SiparisId);
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
        [CustomAuthorization]
        public ProductApiCollection Gett(int esnafId, int siparisSayisi)
        {
            var result = new ProductApiCollection();
            EsnafimEdirneEntities cr = new EsnafimEdirneEntities();
            var apiModels = cr.Siparis.Where(x => x.EsnafId == esnafId && x.Onay == true).Take(siparisSayisi).ToList().Select(c => _modelFactory.Create(c)).OrderByDescending(x => x.SiparisId);
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
    }
}
