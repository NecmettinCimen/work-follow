using WorkFollow.Entities;
using WorkFollow.Services;
using Xunit;

namespace WorkFollow.Test;

public class KullaniciServiceTest
{
    private readonly KullaniciService _kullaniciService;

    public KullaniciServiceTest()
    {
        _kullaniciService = new KullaniciService(new GenericRepository());
    }

    [Fact]
    public void InsertLoginKontrolTest()
    {
        Kullanici kullanici = _kullaniciService.Insert(new Kullanici
        {
            Ad = "Test",
            Cinsiyet = "Erkek",
            Eposta = "test@test.com",
            Sifre = "***"
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