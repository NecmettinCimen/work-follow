using OdevTakip.Entities;

namespace OdevTakip.Services
{
    public interface IKullaniciService
    {
        Kullanici Insert(Kullanici model);

        Kullanici LoginKontrol(Kullanici model);
    }

    public class KullaniciService : IKullaniciService
    {
        private readonly IGenericRepository _genericRepository;

        public KullaniciService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public Kullanici Insert(Kullanici model)
        {
            const string sql = @"INSERT INTO public.kullanici
                                            (aktif, sil, olusturmatarihi, olusturankisi, guncellemetarihi, guncelleyenkisi, ad, soyad, cinsiyet, telefon, eposta, sifre, unvani, egitim, sirket)
                                            VALUES(@aktif, @sil, @olusturmatarihi, @olusturankisi, @guncellemetarihi, @guncelleyenkisi, @ad, @soyad, @cinsiyet, @telefon, @eposta, @sifre, @unvani, @egitim, @sirket);";
            bool result =  _genericRepository.Insert(sql, model);

            if (result)
            {
                return this.LoginKontrol(model);
            }
            else
            {
                return null;
            }

        }

        public Kullanici LoginKontrol(Kullanici model)
        {
            const string sql = @"select Id, ad, soyad from public.kullanici where eposta = @eposta and sifre = @sifre";
            return _genericRepository.First<Kullanici>(sql, model);
        }
    }
}
