using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class MusteriSepetListController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public IEnumerable<SepetModel> Data { get; set; }

        }
        ModelFactory3 _modelFactory;
        public MusteriSepetListController()
        {
            _modelFactory = new ModelFactory3();
        }
        [CustomAuthorization]
        public ProductApiCollection Get(int musteriId)
        {
            var result = new ProductApiCollection();
            EsnafimEdirneEntities cr = new EsnafimEdirneEntities();
            var apiModels = cr.Siparis.Where(x => x.MusteriId == musteriId && x.Onay == false).ToList().Select(c => _modelFactory.Createe(c));
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";

            return result;
        }
       
    }
}
