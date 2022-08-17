using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class OdemeDurumController : ApiController
    {
        EsnafimEdirneEntities db = new EsnafimEdirneEntities();
        // GET /api/values
        public dynamic Get()
        {
            var allCustomers = db.OdemeDurum.Select(i =>
            new {
                i.Id,
                i.Ad,
            });
            return allCustomers;

        }
    }
}
