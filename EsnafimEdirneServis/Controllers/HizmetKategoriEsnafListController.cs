using EsnafimEdirneServis.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class HizmetKategoriEsnafListController : ApiController
    {
        public class ProductApiCollection
        {

            public string Status { get; set; }
            public string Message { get; set; }
            public IEnumerable<EsnafModel> Data { get; set; }

        }
        ModelFactory _modelFactory;
        public HizmetKategoriEsnafListController()
        {
            _modelFactory = new ModelFactory();
        }
        [CustomAuthorization]
        public ProductApiCollection Get(int hizmetKategoriId, [FromUri]PagingParameterModel pagingparametermodel)
        {
            var result = new ProductApiCollection();
            EsnafRepository cr = new EsnafRepository();
            var apiModels = cr.GetAllEsnaf().Where(x=>x.HizmetKategoriID==hizmetKategoriId).OrderBy(x=>x.Id).Skip((pagingparametermodel.pageNumber-1)*pagingparametermodel.pageSize).Take(pagingparametermodel.pageSize).ToList().Select(c => _modelFactory.Create(c));
            result.Data = apiModels;
            result.Status = "200";
            result.Message = "İşlem Başarılı";
            var apiModels2 = cr.GetAllEsnaf().Where(x => x.HizmetKategoriID == hizmetKategoriId).OrderBy(x => x.Id).ToList().Select(c => _modelFactory.Create(c));
            int count = apiModels2.Count();
            int CurrentPage = pagingparametermodel.pageNumber;
            int PageSize = pagingparametermodel.pageSize;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = apiModels.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? true : false;
            var nextPage = CurrentPage < TotalPages ? true : false;
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };
            
            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));

            return result;
        }
        
    }
}
