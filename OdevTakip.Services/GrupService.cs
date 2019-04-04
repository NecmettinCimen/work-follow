using OdevTakip.Entities;
using System.Collections.Generic;

namespace OdevTakip.Services
{
    public interface IGrupService
    {
        bool Insert(Grup model);
        List<Grup> Select(Grup model);
        bool Delete(Grup model);
    }

    public class GrupService : IGrupService
    {
        private readonly IGenericRepository _genericRepository;

        public GrupService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public bool Delete(Grup model)
        {
            const string sql = @"update public.grup set sil=true where Id = @Id";
            return _genericRepository.Delete(sql, model);
        }

        public bool Insert(Grup model)
        {
            const string sql = @"INSERT INTO public.grup(aktif, sil, olusturmatarihi, olusturankisi, guncellemetarihi, guncelleyenkisi, ad, aciklama, yoneticiid)
                                VALUES (@aktif, @sil, @olusturmatarihi, @olusturankisi, @guncellemetarihi, @guncelleyenkisi, @ad, @aciklama, @yoneticiid);";
            return _genericRepository.Insert(sql, model);
        }

        public List<Grup> Select(Grup model)
        {
            const string sql = "select * from public.Grup where sil=@sil";
            return _genericRepository.Select<Grup>(sql, model);
        }

    }
}
