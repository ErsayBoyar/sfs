using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsnafimEdirneServis.Models
{
    public class ModelFactory2
    {
        public Esnaf2Model Create(Esnaf esnaf)
        {
            return new Esnaf2Model()
            {
                EsnafId = esnaf.Id,
                KisaAd = esnaf.KisaAd,
                FirmaUnvan=esnaf.FirmaUnvan,
                FotoYol = esnaf.FotoYol,
                Telefon = esnaf.Telefon,
                Adres = esnaf.Adres,
                MinSiparis = esnaf.MinSiparis,
                TeslimatSuresi = esnaf.TeslimatSuresi,
                HizmetKategoriID = esnaf.HizmetKategoriID,
                Online = esnaf.Online,
                Lat = esnaf.Lat,
                Lon = esnaf.Lon,
                urunKategori = esnaf.UrunKategori.Where(x=>x.Sil!=true).Select(h => Create(h))
            };
        }

        public UrunKategoriModel Create(UrunKategori kampanya)
        {
            return new UrunKategoriModel()
            {
                UrunKategoriId = kampanya.Id,
                EsnafId = Convert.ToInt32( kampanya.EsnafId),
                Ad = kampanya.Ad,
                Sil = Convert.ToBoolean(kampanya.Sil),
                urun= kampanya.Urun.Where(x => x.Sil != true).Select(h => Create(h))

            };
        }
        public UrunModel Create(Urun kampanya)
        {
            return new UrunModel()
            {
                UrunId = kampanya.Id,
                Aciklama = kampanya.Aciklama,
                Ad = kampanya.Ad,
                Fiyat=Convert.ToDecimal(kampanya.Fiyat),
                Sil = Convert.ToBoolean(kampanya.Sil),
                Mevcut = Convert.ToBoolean(kampanya.Mevcut),
                FotoYol=kampanya.FotoYol

            };
        }
    }
}