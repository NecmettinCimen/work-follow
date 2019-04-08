using OdevTakip.Entities;
using OdevTakip.Services;
using System.Collections.Generic;

namespace OdevTakip.Models
{
    /// <summary>
    /// singleton
    /// </summary>
    public class GenericModels
    {
        private static GenericModels nesne;

        private static readonly SGenericRepository sGenericRepository = new SGenericRepository();
        public static string kategoriOptionString { get; internal set; }
        public static string durumOptionString { get; internal set; }
        public static string grupOptionString { get; internal set; }

        private GenericModels()
        {
            KategoriOptionRefresh(null);

            DurumOptionRefresh(null);

            GrupOptionRefresh();

        }

        public GenericModels(string grup)
        {

        }

        public static void KategoriOptionRefresh(List<AdEntity> kategoriList)
        {
            if (kategoriList == null)
            {
                kategoriList = sGenericRepository.Select<AdEntity>("select * from public.kategori");
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
                durumList = sGenericRepository.Select<AdEntity>("select * from public.durum");
            }

            durumOptionString = "";

            foreach (AdEntity item in durumList)
            {
                durumOptionString += $"<option value='{item.id}'>{item.ad}</option>";
            }
        }

        public static void GrupOptionRefresh()
        {
            grupOptionString = "";

            List<AdEntity> grupList = sGenericRepository.Select<AdEntity>("select * from public.grup");

            foreach (AdEntity item in grupList)
            {
                grupOptionString += $"<option value='{item.id}'>{item.ad}</option>";
            }

        }


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
