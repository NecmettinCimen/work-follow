using OdevTakip.Entities;
using OdevTakip.Services;
using System.Collections.Generic;

namespace OdevTakip.Models
{
    public class GenericModels
    {
        private static GenericModels nesne;

        private static EntityRefinedAbstraction entityRefinedAbstraction;
        public static string kategoriOptionString { get; internal set; }
        public static string durumOptionString { get; internal set; }
        public static string grupOptionString { get; internal set; }
        public static string projeOptionString { get; internal set; }

        private GenericModels()
        {
            KategoriOptionRefresh(null);

            DurumOptionRefresh(null);

            GrupOptionRefresh();

            ProjeOptionRefresh(null);

        }

        public GenericModels(string grup)
        {

        }

        public static void KategoriOptionRefresh(List<KategoriEntity> kategoriList)
        {
            if (kategoriList == null)
            {
                entityRefinedAbstraction = new EntityRefinedAbstraction(new KategoriEntity());
                kategoriList = entityRefinedAbstraction.Select<KategoriEntity>();
            }

            kategoriOptionString = "";

            foreach (KategoriEntity item in kategoriList)
            {
                kategoriOptionString += $"<option value='{item.id}'>{item.ad}</option>";
            }
        }

        public static void DurumOptionRefresh(List<DurumEntity> durumList)
        {
            if (durumList == null)
            {
                entityRefinedAbstraction = new EntityRefinedAbstraction(new DurumEntity());
                durumList = entityRefinedAbstraction.Select<DurumEntity>();
            }

            durumOptionString = "";

            foreach (DurumEntity item in durumList)
            {
                durumOptionString += $"<option value='{item.id}'>{item.ad}</option>";
            }
        }


        public static void ProjeOptionRefresh(List<ProjeEntity> projeList)
        {
            if (projeList == null)
            {
                entityRefinedAbstraction = new EntityRefinedAbstraction(new ProjeEntity());
                projeList = entityRefinedAbstraction.Select<ProjeEntity>();
            }

            projeOptionString = "";

            foreach (AdEntity item in projeList)
            {
                projeOptionString += $"<option value='{item.id}'>{item.ad}</option>";
            }
        }

        public static void GrupOptionRefresh()
        {
            grupOptionString = "";

            entityRefinedAbstraction = new EntityRefinedAbstraction(new GrupEntity());
            List<GrupEntity> grupList = entityRefinedAbstraction.Select<GrupEntity>();

            foreach (GrupEntity item in grupList)
            {
                grupOptionString += $"<option value='{item.id}'>{item.ad}</option>";
            }

        }


        /// <summary>
        /// singleton
        /// </summary>
        public static GenericModels Nesne()
        {
            if (nesne == null)
            {
                nesne = new GenericModels();

            }

            return nesne;
        }
    }
}
