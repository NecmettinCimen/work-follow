using WorkFollow.Entities;

namespace OdevTakip.Services
{
    public interface ITblNotService
    {
        int Insert(TblNot model);
        bool Update(TblNot model);
        bool Delete(TblNot model);
    }
    public class TblNotService : ITblNotService
    {
        readonly IGenericRepository _genericRepository;

        public TblNotService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public bool Delete(TblNot model)
        {
            const string Sql = "delete from public.tblnot where id = @id";
            return _genericRepository.Delete(Sql, model);
        }

        public int Insert(TblNot model)
        {
            const string Sql = "insert into public.tblnot (konu, aciklama) values (@konu, @aciklama)";
            return _genericRepository.InsertAndGetId(Sql, model);
        }

        public bool Update(TblNot model)
        {
            const string Sql = "update from public.tblnot set konu = @konu, aciklama = @aciklama where id = @id";
            return _genericRepository.Update(Sql, model);
        }
    }
}
