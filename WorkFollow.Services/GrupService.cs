using WorkFollow.Entities;
using System.Collections.Generic;

namespace OdevTakip.Services
{
    public interface IGrupService
    {
        bool Insert(Grup model);
        List<Grup> Select(Grup model);
        bool Delete(Grup model);
        Grup First(Grup model);
        bool Update(Grup model);
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

        public Grup First(Grup model)
        {
            const string sql = @"select * from public.grup  where Id = @Id";
            return _genericRepository.First<Grup>(sql, model);
        }

        public bool Insert(Grup model)
        {
            const string sql = @"INSERT INTO public.grup(aktif, sil, olusturmatarihi, olusturankisi, guncellemetarihi, guncelleyenkisi, ad, aciklama, yoneticiid)
                                VALUES (@aktif, @sil, @olusturmatarihi, @olusturankisi, @guncellemetarihi, @guncelleyenkisi, @ad, @aciklama, @yoneticiid);";
            return _genericRepository.Insert(sql, model);
        }

        public List<Grup> Select(Grup model)
        {
            const string sql = "select * from public.Grup where sil=@sil and olusturankisi = @olusturankisi";
            return _genericRepository.Select<Grup>(sql, model);
        }

        public bool Update(Grup model)
        {
            const string sql = @"update public.grup set guncelleyenkisi = @guncelleyenkisi, ad = @ad, aciklama = @aciklama where id = @id";
            return _genericRepository.Update(sql, model);
        }
    }
}
