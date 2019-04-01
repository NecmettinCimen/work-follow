using OdevTakip.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdevTakip.Services
{
    public class KullaniciService
    {
        public bool Insert(Kullanici model)
        {
            return GenericRepository.Insert(@"INSERT INTO public.kullanici
(aktif, sil, olusturmatarihi, olusturankisi, guncellemetarihi, guncelleyenkisi, ad, soyad, cinsiyet, telefon, eposta, sifre, unvani, egitim, sirket)
VALUES(@aktif, @sil, @olusturmatarihi, @olusturankisi, @guncellemetarihi, @guncelleyenkisi, @ad, @soyad, @cinsiyet, @telefon, @eposta, @sifre, @unvani, @egitim, @sirket)", model);
        }
    }
}
