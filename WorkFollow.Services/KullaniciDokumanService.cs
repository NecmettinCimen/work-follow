using System.Collections.Generic;
using WorkFollow.Entities;

namespace WorkFollow.Services;

public interface IKullaniciDokumanService
{
    bool Insert(Dokuman model);
    bool Delete(int id);
    List<Dokuman> Select(KullaniciDokuman model);
}

public class KullaniciDokumanService : IKullaniciDokumanService
{
    private readonly IDokumanService _dokumanService;
    private readonly IGenericRepository _genericRepository;

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
        var kullaniciDokuman = new KullaniciDokuman
        {
            DokumanId = _dokumanService.Insert(model),
            KullaniciId = model.Olusturankisi,
            Etkinlikid = model.Guncelleyenkisi
        };

        const string Sql =
            "insert into public.kullanicidokuman (kullaniciid, dokumanid, etkinlikid) values(@kullaniciid, @dokumanid, @etkinlikid)";
        return _genericRepository.Insert(Sql, kullaniciDokuman);
    }

    public List<Dokuman> Select(KullaniciDokuman model)
    {
        const string Sql =
            "select d.* from public.dokuman d, public.kullanicidokuman kd where d.id = kd.dokumanid and kd.etkinlikid = @etkinlikid";
        return _genericRepository.Select<Dokuman>(Sql, model);
    }
}