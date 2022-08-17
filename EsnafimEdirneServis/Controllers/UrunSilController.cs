using EsnafimEdirneServis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EsnafimEdirneServis.Controllers
{
    public class UrunSilController : ApiController
    {
        string value;
        [CustomAuthorization]
        public string Put(int urunid,bool sil)
        {
            EsnafimEdirneEntities db = new EsnafimEdirneEntities();

            var list = db.Urun.Where(x => x.Id == urunid).SingleOrDefault();
            if (list != null)
            {
                value = list.Id.ToString();
                list.Sil = Convert.ToBoolean(sil);
                db.SaveChanges();
                value = "İşlem Başarılı";

            }
            else
            {
                value = "Kayıtlı Mail Adresi Bulunamadı";
            }
            return value;

        }
    }
}
