using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsnafimEdirneServis.Controllers
{
    public class EsnafRepository
    {
        public IQueryable<Esnaf> GetAllEsnaf()
        {
            EsnafimEdirneEntities ctx = new EsnafimEdirneEntities();
            return ctx.Esnaf.Where(x=>x.Aktif==true);
        }
        public IQueryable<Esnaf> GetAllEsnaf(int id)
        {
            EsnafimEdirneEntities ctx = new EsnafimEdirneEntities();
            return ctx.Esnaf.Where(c=>c.Id==id && c.Aktif == true).Select(e=>e);
        }
    }
}