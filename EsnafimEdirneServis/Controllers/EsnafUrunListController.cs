using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class EsnafUrunListController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public IEnumerable<Esnaf2Model> Data { get; set; }

        }
        ModelFactory2 _modelFactory;
        public EsnafUrunListController()
        {
            _modelFactory = new ModelFactory2();
        }
        
        [CustomAuthorization]
        public ProductApiCollection Get()
        {
            var result = new ProductApiCollection();
            EsnafRepository cr = new EsnafRepository();
            var apiModels = cr.GetAllEsnaf().Where(x=>x.Aktif==true).ToList().Select(c => _modelFactory.Create(c));
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
        [CustomAuthorization]
        public ProductApiCollection Get(int esnafId)
        {
            var result = new ProductApiCollection();
            EsnafRepository cr = new EsnafRepository();
            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            var apiModels = db.Esnaf.Where(x=>x.Id==esnafId).ToList().Select(c => _modelFactory.Create(c));
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
    }
}
