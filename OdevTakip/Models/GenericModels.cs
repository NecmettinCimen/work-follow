using OdevTakip.Entities;
using OdevTakip.Services;
using System.Collections.Generic;

namespace OdevTakip.Models
{
    public class GenericModels
    {
        private static GenericModels nesne;

        private static readonly SGenericRepository sGenericRepository = new SGenericRepository();
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

        public static void KategoriOptionRefresh(List<AdEntity> kategoriList)
        {
            if (kategoriList == null)
            {
                kategoriList = sGenericRepository.Select<AdEntity>("select * from public.kategori where sil=false");
            }

            kategoriOptionString = "";

            foreach (AdEntity item in kategoriList)
            {
                kategoriOptionString += $"<option value='{item.id}'>{item.ad}</option>";
            }
        }

        public static void DurumOptionRefresh(List<AdEntity> durumList)
        {
            if (durumList == null)
            {
                durumList = sGenericRepository.Select<AdEntity>("select * from public.durum where sil=false");
            }

            durumOptionString = "";

            foreach (AdEntity item in durumList)
            {
                durumOptionString += $"<option value='{item.id}'>{item.ad}</option>";
            }
        }


        public static void ProjeOptionRefresh(List<AdEntity> projeList)
        {
            if (projeList == null)
            {
                projeList = sGenericRepository.Select<AdEntity>("select * from public.proje where sil=false");
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

            List<AdEntity> grupList = sGenericRepository.Select<AdEntity>("select * from public.grup where sil=false");

            foreach (AdEntity item in grupList)
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
