using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EsnafimEdirneServis.Models
{
    public class ModelFactory3
    {
        public MusteriModel Create(Musteri esnaf)
        {
            return new MusteriModel()
            {
                MusteriId = esnaf.Id,
                Ad = esnaf.Ad,
                Soyad = esnaf.Soyad,
                Telefon = esnaf.Telefon,
                Eposta = esnaf.EPosta,
                Lat = esnaf.Lat,
                Lon = esnaf.Lon,
                siparis = esnaf.Siparis.Select(a => Create(a))
            };
        }

        public SiparisModel Create(Siparis kampanya)
        {
            return new SiparisModel()
            {
                SiparisId = kampanya.Id,
                EsnafAd=kampanya.Esnaf.KisaAd,
                EsnafTelefon=kampanya.Esnaf.Telefon,
                EsnafFoto=kampanya.Esnaf.FotoYol,
                MusteriAd=kampanya.Musteri.Ad,
                MusteriSoyad=kampanya.Musteri.Soyad,
                MusteriMail=kampanya.Musteri.EPosta,
                MusteriTelefon=kampanya.Musteri.Telefon,
                ToplamTutar=Convert.ToDecimal(kampanya.ToplamTutar),
                IndirimTutari=Convert.ToDecimal(kampanya.IndirimTutari),
                IndirimliTutar=Convert.ToDecimal(kampanya.IndirimliTutar),
                MinSiparis = Convert.ToDecimal(kampanya.Esnaf.MinSiparis),
                SiparisNotu = kampanya.SiparisNotu,
                AdresId=Convert.ToInt32(kampanya.AdresId),
                Durum=kampanya.SiparisDurum.Ad,
                DurumId=Convert.ToInt32( kampanya.DurumId),
                OdemeDurum=kampanya.SiparisOdemeTur.Ad,
                Tarih=Convert.ToDateTime(kampanya.Tarih),
                Onay=Convert.ToBoolean(kampanya.Onay),
                AdresAd=kampanya.MusteriAdres.AdresAd,
                Adres=kampanya.MusteriAdres.Adres,
                AdresTarifi=kampanya.MusteriAdres.AdresTarifi,
                BinaNo=kampanya.MusteriAdres.BinaNo,
                Daire=kampanya.MusteriAdres.Daire,
                Lat=kampanya.MusteriAdres.Lat,
                Lon=kampanya.MusteriAdres.Lon,
                siparisDetay = kampanya.SiparisDetay.Where(x=>x.Sil!=true).Select(b => Create(b)),
                
            };
        }
        EsnafimEdirneEntities db = new EsnafimEdirneEntities();
        public SepetModel Createe(Siparis kampanya)
        {
            int esnaf_id = Convert.ToInt32( kampanya.EsnafId);
            int siparis_id = Convert.ToInt32(kampanya.Id);
            var esnaf= db.Esnaf.Where(x => x.Id == esnaf_id).SingleOrDefault();
            int adet = db.SiparisDetay.Where(x => x.SiparisId == siparis_id).Count();
            return new SepetModel()
            {
                SiparisId = kampanya.Id,
                EsnafId = kampanya.EsnafId,
                SepetCount = adet,
                EsnafAd=esnaf.KisaAd,
                EsnafFoto=esnaf.FotoYol,
                EsnafTelefon= esnaf.Telefon,
                ToplamTutar = Convert.ToDecimal(kampanya.ToplamTutar),
                IndirimTutari = Convert.ToDecimal(kampanya.IndirimTutari),
                IndirimliTutar = Convert.ToDecimal(kampanya.IndirimliTutar),
                Tarih = Convert.ToDateTime(kampanya.Tarih),
                MinSiparis = Convert.ToDecimal(esnaf.MinSiparis),
                siparisDetay = kampanya.SiparisDetay.Where(x => x.Sil != true).Select(b => Create(b)),

            };
        }
        public SiparisDetayModel Create(SiparisDetay kampanya2)
        {
            return new SiparisDetayModel()
            {
                SiparisDetayId= Convert.ToInt32(kampanya2.Id),
                SiparisId = Convert.ToInt32( kampanya2.SiparisId),
                UrunId =Convert.ToInt32( kampanya2.UrunId),
                UrunAd=kampanya2.Urun.Ad,
                UrunFoto = kampanya2.Urun.FotoYol,
                Adet =Convert.ToInt32( kampanya2.Adet),
                Fiyat=Convert.ToDecimal(kampanya2.Fiyat),
                IndirimTutari = Convert.ToDecimal(kampanya2.IndirimTutari),
                IndirimliTutar = Convert.ToDecimal(kampanya2.IndirimliTutar),
            };
        }
        public AdresDetayModel Create(MusteriAdres adres)
        {
            return new AdresDetayModel()
            {
                Adres = adres.Adres,
                AdresAd = adres.AdresAd,
                AdresTarifi = adres.AdresTarifi,
                BinaNo = adres.BinaNo,
                Daire = adres.Daire,
                Kat=adres.Kat,
                Lat=adres.Lat,
                Lon=adres.Lon
                
            };
        }
    }
}