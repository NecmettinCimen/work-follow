using OdevTakip.Entities;
using System;
using System.Collections.Generic;

namespace OdevTakip.Services
{
    public interface IKullaniciDokumanService
    {
        bool Insert(Dokuman model);
        bool Delete(int id);
        List<Dokuman> Select(KullaniciDokuman model);
    }
    public class KullaniciDokumanService : IKullaniciDokumanService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly IDokumanService _dokumanService;
        public KullaniciDokumanService(
            IGenericRepository genericRepository,
            IDokumanService dokumanService)
        {
            _genericRepository = genericRepository;
            _dokumanService = dokumanService;
        }
        public bool Delete(int id)
        {
            const string Sql = "delete from public.kullanicidokuman where id = @id";
            return _genericRepository.Delete(Sql, new { id });
        }

        public bool Insert(Dokuman model)
        {
            KullaniciDokuman kullaniciDokuman = new KullaniciDokuman
            {
                DokumanId = _dokumanService.Insert(model),
                KullaniciId = model.Olusturankisi,
                Etkinlikid = model.Guncelleyenkisi
            };

            const string Sql = "insert into public.kullanicidokuman (kullaniciid, dokumanid, etkinlikid) values(@kullaniciid, @dokumanid, @etkinlikid)";
            return _genericRepository.Insert(Sql, kullaniciDokuman);
        }

        public List<Dokuman> Select(KullaniciDokuman model)
        {
            const string Sql = "select d.* from public.dokuman d, public.kullanicidokuman kd where d.id = kd.dokumanid and kd.etkinlikid = @etkinlikid";
            return _genericRepository.Select<Dokuman>(Sql, model);
        }
    }
}
