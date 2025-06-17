using System.Collections.Generic;
using WorkFollow.Entities;

namespace WorkFollow.Services;

public interface IProjeService
{
    bool Insert(Proje model);
    List<Proje> Select(Proje model);
    bool Delete(Proje model);
    Proje First(Proje model);
}

public class ProjeService : IProjeService
{
    private readonly IGenericRepository _genericRepository;

    public ProjeService(IGenericRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public bool Delete(Proje model)
    {
        const string sql = @"update public.proje set sil=true where Id = @Id";
        return _genericRepository.Delete(sql, model);
    }

    public Proje First(Proje model)
    {
        const string sql = @"select * from public.proje  where Id = @Id";
        return _genericRepository.First<Proje>(sql, model);
    }

    public bool Insert(Proje model)
    {
        const string sql = @"INSERT INTO public.proje(
	        sil, olusturmatarihi, olusturankisi, guncellemetarihi, guncelleyenkisi, ad, aciklama, durumid, kategoriid, baslangictarihi, bitistarihi, yoneticiid, grupid)
	        VALUES (@sil, @olusturmatarihi, @olusturankisi, @guncellemetarihi, @guncelleyenkisi, @ad, @aciklama, @durumid, @kategoriid, @baslangictarihi, @bitistarihi, @yoneticiid, @grupid);";

        return _genericRepository.Insert(sql, model);
    }

    public List<Proje> Select(Proje model)
    {
        const string sql = "select * from public.proje where sil=@sil and olusturankisi = @olusturankisi";

        return _genericRepository.Select<Proje>(sql, model);
    }
}