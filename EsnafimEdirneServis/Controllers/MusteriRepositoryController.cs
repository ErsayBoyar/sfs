using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class MusteriRepositoryController : ApiController
    {
        public IQueryable<SiparisDetay> GetAllEsnaf()
        {
            EsnafimEdirneEntities ctx = new EsnafimEdirneEntities();
            return ctx.SiparisDetay;
        }
        public IQueryable<SiparisDetay> GetAllEsnaf(int id)
        {
            EsnafimEdirneEntities ctx = new EsnafimEdirneEntities();
            return ctx.SiparisDetay.Where(c => c.Id == id).Select(e => e);
        }
    }
}
