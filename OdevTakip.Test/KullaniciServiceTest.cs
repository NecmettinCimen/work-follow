using OdevTakip.Entities;
using OdevTakip.Services;
using System;
using Xunit;

namespace OdevTakip.Test
{
    public class KullaniciServiceTest
    {
        readonly KullaniciService _kullaniciService;

        public KullaniciServiceTest()
        {
            _kullaniciService = new KullaniciService(new GenericRepository());
        }

        [Fact]
        public void InsertLoginKontrolTest()
        {
            Kullanici kullanici =_kullaniciService.Insert(new Kullanici
            {
                Ad="Test",
                Cinsiyet="Erkek",
                Eposta="test@test.com",
                Sifre="***"
            });

            Assert.NotNull(kullanici);

            Kullanici kullanici2 = _kullaniciService.LoginKontrol(new Kullanici
            {
                Eposta = "test@test.com",
                Sifre = "***"
            });

            Assert.NotNull(kullanici2);
        }
    }
}
