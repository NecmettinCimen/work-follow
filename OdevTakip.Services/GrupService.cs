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

        public bool Delete(Grup model) => _genericRepository.Delete(@"update public.grup set sil=1 where Id = @Id", model);

        public bool Insert(Grup model) => _genericRepository.Insert(@"INSERT INTO public.grup(aktif, sil, olusturmatarihi, olusturankisi, guncellemetarihi, guncelleyenkisi, ad, aciklama, yoneticiid)
	                                                                                        VALUES (@aktif, @sil, @olusturmatarihi, @olusturankisi, @guncellemetarihi, @guncelleyenkisi, @ad, @aciklama, @yoneticiid);", model);

        public List<Grup> Select(Grup model) => _genericRepository.Select<Grup>("select * from public.Grup where sil=@sil", model);

    }
}
