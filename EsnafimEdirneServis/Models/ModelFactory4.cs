using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsnafimEdirneServis.Models
{
    public class ModelFactory4
    {
        EsnafimEdirneEntities db = new EsnafimEdirneEntities();
        public UrunKategoriModel2 Create(UrunKategori kampanya)
        {
            return new UrunKategoriModel2()
            {
                
                UrunKategoriId = kampanya.Id,
                EsnafId = Convert.ToInt32(kampanya.EsnafId),
                Ad = kampanya.Ad,
                Sil = Convert.ToBoolean(kampanya.Sil),
                urunAdet = db.Urun.Where(x => x.EsnafId == kampanya.EsnafId && x.KategoriId==kampanya.Id && x.Sil==false).Count()

            };
        }
        //public UrunModel Create(Urun kampanya)
        //{
        //    return new UrunModel()
        //    {
        //        UrunId = kampanya.Id,
        //        Aciklama = kampanya.Aciklama,
        //        Ad = kampanya.Ad,
        //        Fiyat = Convert.ToDecimal(kampanya.Fiyat),
        //        Sil = Convert.ToBoolean(kampanya.Sil),
        //        Mevcut = Convert.ToBoolean(kampanya.Mevcut),
        //        FotoYol = kampanya.FotoYol

        //    };
        //}
    }
}