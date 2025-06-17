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
            KategoriOptionRefresh();

            DurumOptionRefresh();

            GrupOptionRefresh();

            ProjeOptionRefresh();

        }

        public GenericModels(string grup)
        {

        }

        public static void KategoriOptionRefresh()
        {

            entityRefinedAbstraction = new EntityRefinedAbstraction(new KategoriEntity());
            kategoriOptionString = entityRefinedAbstraction.Select();

        }

        public static void DurumOptionRefresh()
        {

            entityRefinedAbstraction = new EntityRefinedAbstraction(new DurumEntity());
            durumOptionString = entityRefinedAbstraction.Select();

        }


        public static void ProjeOptionRefresh()
        {

            entityRefinedAbstraction = new EntityRefinedAbstraction(new ProjeEntity());
            projeOptionString = entityRefinedAbstraction.Select();

        }

        public static void GrupOptionRefresh()
        {

            entityRefinedAbstraction = new EntityRefinedAbstraction(new GrupEntity());
            grupOptionString = entityRefinedAbstraction.Select();

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
