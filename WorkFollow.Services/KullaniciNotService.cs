using WorkFollow.Entities;
using System.Collections.Generic;

namespace OdevTakip.Services
{
    public interface IKullaniciNotService
    {
        List<TblNot> Select(KullaniciNot model);
        bool Insert(int etkinlikid, TblNot tblNot);
        bool Update(TblNot model);
        bool Delete(TblNot model);
    }
    public class KullaniciNotService : IKullaniciNotService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ITblNotService _tblNotService;
        public KullaniciNotService(IGenericRepository genericRepository, ITblNotService tblNotService)
        {
            _genericRepository = genericRepository;
            _tblNotService = tblNotService;
        }

        public bool Delete(TblNot model)
        {
            const string Sql = "delete from public.kullanicinot where id = @id";
            _genericRepository.Delete(Sql, model);

            const string DeleteSqlTblNot = "delete from public.tblnot where id = @notid";
            return _genericRepository.Delete(DeleteSqlTblNot, model);
        }

        public bool Insert(int etkinlikid, TblNot tblNot)
        {
            int tblnotid = _tblNotService.Insert(tblNot);

            KullaniciNot kullaniciNot = new KullaniciNot
            {
                KullaniciId = tblNot.Olusturankisi,
                NotId = tblnotid,
                Etkinlikid = etkinlikid
            };

            const string Sql = "insert into public.kullanicinot (kullaniciid, notid, etkinlikid) values (@kullaniciid, @notid, @etkinlikid)";
            return _genericRepository.Insert(Sql, kullaniciNot);

        }

        public List<TblNot> Select(KullaniciNot model)
        {
            const string Sql = @"select t.id, t.konu, t.aciklama from public.tblnot t, public.kullanicinot kt where t.id = kt.notid and kt.kullaniciid =@kullaniciid and kt.etkinlikid = @etkinlikid";
            return _genericRepository.Select<TblNot>(Sql, model);
        }

        public bool Update(TblNot model)
        {
            const string Sql = "update from public.tblnot set konu = @konu, aciklama = @aciklama where id = @id";
            return _genericRepository.Update(Sql, model);
        }
    }
}
