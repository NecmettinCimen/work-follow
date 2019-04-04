using OdevTakip.Entities;
using System.Collections.Generic;

namespace OdevTakip.Services
{
    public interface IProjeService
    {
        bool Insert(Proje model);
        List<Proje> Select(Proje model);
    }
    public class ProjeService : IProjeService
    {
        private readonly IGenericRepository _genericRepository;
        public ProjeService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public bool Insert(Proje model)
        {
            const string sql = @"INSERT INTO public.proje(
	        sil, olusturmatarihi, olusturankisi, guncellemetarihi, guncelleyenkisi, ad, aciklama, durumid, kategoriid, baslangictarihi, bitistarihi, yoneticiid, grupid)
	        VALUES (@sil, @olusturmatarihi, @olusturankisi, @guncellemetarihi, @guncelleyenkisi, @ad, @aciklama, @durumid, @kategoriid, @baslangictarihi, @bitistarihi, @yoneticiid, @grupid);";

            return _genericRepository.Insert(sql, model);
        }

        public List<Proje> Select(Proje model)
        {
            const string sql = "select * from public.proje where sil=@sil";

            return _genericRepository.Select<Proje>(sql, model);
        }
    }
}
