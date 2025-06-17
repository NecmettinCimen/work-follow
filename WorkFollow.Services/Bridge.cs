using System.Linq;

namespace WorkFollow.Services;

/// <summary>
///     Implemantor arayüzü ile operasyonlar tanımlanır ve ConcreteImplemantor lar bu arayüzden türeyerek operasyonları
///     gerçekleştirir.
///     Abstraction abstract sınıfı ise içinde Implemantor arayüzünden referans barındırarak Implemantor daki operasyonları
///     çalıştırır.
///     RefinedAbstraction ise Abstraction u uygulayan gerçek sınıf veya senaryoya göre sınıflardır. Client ise Abstraction
///     ve Implemantor türlerinden nesneleri üreterek yapıyı kullanır.
/// </summary>
public abstract class AdEntity
{
    public int id { get; set; }
    public string ad { get; set; }
}

public class DurumEntity : AdEntity
{
    public override string ToString()
    {
        return "durum";
    }
}

public class KategoriEntity : AdEntity
{
    public override string ToString()
    {
        return "kategori";
    }
}

public class ProjeEntity : AdEntity
{
    public override string ToString()
    {
        return "proje";
    }
}

public class GrupEntity : AdEntity
{
    public override string ToString()
    {
        return "grup";
    }
}

public abstract class EntityAbstraction
{
    protected AdEntity adEntity;

    public EntityAbstraction(AdEntity adEntity)
    {
        this.adEntity = adEntity;
    }

    public AdEntity AdEntity
    {
        set => adEntity = value;
    }

    public abstract string Select();
}

public class EntityRefinedAbstraction : EntityAbstraction
{
    public EntityRefinedAbstraction(AdEntity adEntity) : base(adEntity)
    {
    }

    public override string Select()
    {
        if (adEntity.ad == null) return string.Empty;
        var adEntitys = SGenericRepository.Select("select * from public." + adEntity + " where sil=false")
            .Select(item => $"<option value='{item.id}'>{item.ad}</option>").ToList();

        return string.Join("", adEntitys);
    }
}