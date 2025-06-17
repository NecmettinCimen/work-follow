using WorkFollow.Entities;

namespace OdevTakip.Services
{
    public interface IDokumanService
    {
        int Insert(Dokuman model);
    }
    public class DokumanService : IDokumanService
    {
        readonly IGenericRepository _genericRepository;
        public DokumanService(IGenericRepository genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public int Insert(Dokuman model)
        {
            const string Sql = "insert into public.dokuman (ad, tip, boyut, dokumandata) values(@ad, @tip, @boyut, @dokumandata)";
            return _genericRepository.InsertAndGetId(Sql, model);
        }
    }
}
