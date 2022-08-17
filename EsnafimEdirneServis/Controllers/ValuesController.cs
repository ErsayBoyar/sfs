using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class ValuesController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public IEnumerable<EsnafModel> Data  { get; set; }

        }
        ModelFactory _modelFactory;
        public ValuesController()
        {
            _modelFactory = new ModelFactory();
        }
        [CustomAuthorization]
        public ProductApiCollection Get()
        {
            var result = new ProductApiCollection();
            EsnafRepository cr = new EsnafRepository();
            var apiModels= cr.GetAllEsnaf().ToList().Select(c => _modelFactory.Create(c));
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
       
    }
}
