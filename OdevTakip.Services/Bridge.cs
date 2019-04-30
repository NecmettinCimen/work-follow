using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OdevTakip.Services
{
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
            set { adEntity = value; }
        }
        public abstract void Insert();
        public abstract List<T> Select<T>() where T : AdEntity;
    }

    public class EntityRefinedAbstraction : EntityAbstraction
    {
        private static readonly SGenericRepository sGenericRepository = new SGenericRepository();

        public EntityRefinedAbstraction(AdEntity adEntity) : base(adEntity)
        {

        }

        public override void Insert()
        {
            
        }

        public override List<T> Select<T>()
        {
            return sGenericRepository.Select<T>("select * from public." + adEntity.ToString() + " where sil=false");
        }
    }

}
