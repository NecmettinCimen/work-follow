using OdevTakip.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdevTakip.Services
{
    public interface IGrupService
    {
        bool Insert(Grup model);
        List<Grup> Select(Grup model);
    }

    public class GrupService : IGrupService
    {
        private readonly IGenericRepository _genericRepository;

        public GrupService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public bool Insert(Grup model) => _genericRepository.Insert(@"INSERT INTO public.grup(aktif, sil, olusturmatarihi, olusturankisi, guncellemetarihi, guncelleyenkisi, ad, aciklama, yoneticiid)
	                                                                                        VALUES (@aktif, @sil, @olusturmatarihi, @olusturankisi, @guncellemetarihi, @guncelleyenkisi, @ad, @aciklama, @yoneticiid);", model);

        public List<Grup> Select(Grup model) => _genericRepository.Select<Grup>("select * from public.Grup", model);

    }
}
