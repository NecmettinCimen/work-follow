using WorkFollow.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OdevTakip.Services
{
    public interface IEtkinlikService
    {
        List<Etkinlik> Select(Etkinlik model);

        Etkinlik Find(Etkinlik model);

        bool Insert(Etkinlik model);

        bool Update(Etkinlik model);

        bool Delete(Etkinlik model);

    }

    public class EtkinlikService : IEtkinlikService
    {
        private readonly IGenericRepository _genericRepository;
        public EtkinlikService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public List<Etkinlik> Select(Etkinlik model)
        {
            const string sql = @"SELECT id, sil, olusturmatarihi, olusturankisi, guncellemetarihi, guncelleyenkisi, baslik, aciklama, durumid, kategoriid, baslangictarihi, bitistarihi, atananid, projeid
	                                FROM public.etkinlik where sil = @sil and olusturankisi=@olusturankisi and projeid = CASE @projeid WHEN 0 THEN projeid ELSE @projeid END;";

            return _genericRepository.Select<Etkinlik>(sql, model);
        }

        public Etkinlik Find(Etkinlik model)
        {
            const string sql = @"SELECT id, sil, olusturmatarihi, olusturankisi, guncellemetarihi, guncelleyenkisi, baslik, aciklama, durumid, kategoriid, baslangictarihi, bitistarihi, atananid, projeid
	                                FROM public.etkinlik where sil = @sil and olusturankisi=@olusturankisi and id=@id;";

            return _genericRepository.First<Etkinlik>(sql, model);
        }

        public bool Insert(Etkinlik model)
        {
            const string sql = @"INSERT INTO public.etkinlik(
	                                sil, olusturmatarihi, olusturankisi, guncellemetarihi, guncelleyenkisi, baslik, aciklama, durumid, kategoriid, baslangictarihi, bitistarihi, atananid, projeid)
	                                VALUES (@sil, @olusturmatarihi, @olusturankisi, @guncellemetarihi, @guncelleyenkisi, @baslik, @aciklama, @durumid, @kategoriid, @baslangictarihi, @bitistarihi, @atananid, @projeid);";

            return _genericRepository.Insert(sql, model);
        }

        public bool Update(Etkinlik model)
        {
            const string sql = @"UPDATE public.etkinlik
	                                SET guncellemetarihi=@guncellemetarihi, guncelleyenkisi=@guncelleyenkisi, baslik=@baslik, aciklama=@aciklama, durumid=@durumid, kategoriid=@kategoriid, baslangictarihi=@baslangictarihi, bitistarihi=@bitistarihi, atananid=@atananid, projeid=@projeid
	                                WHERE id=@id";

            return _genericRepository.Update(sql, model);
        }

        public bool Delete(Etkinlik model)
        {
            const string sql = @"UPDATE public.etkinlik
	                                SET sil=true
	                                WHERE id=@id";

            return _genericRepository.Delete(sql, model);
        }

    }
}
