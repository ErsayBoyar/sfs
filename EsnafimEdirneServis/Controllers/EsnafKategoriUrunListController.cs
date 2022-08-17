using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class EsnafKategoriUrunListController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public IEnumerable<UrunKategoriModel2> Data { get; set; }

        }
        ModelFactory4 _modelFactory;
        public EsnafKategoriUrunListController()
        {
            _modelFactory = new ModelFactory4();
        }
        [CustomAuthorization]
        public ProductApiCollection Get(int esnafId)
        {
            var result = new ProductApiCollection();
            EsnafimEdirneEntities db = new EsnafimEdirneEntities();
            var apiModels = db.UrunKategori.Where(x => x.EsnafId == esnafId && x.Sil==false).ToList().Select(c => _modelFactory.Create(c));
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";
            return result;
        }
    }
}
