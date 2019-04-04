using OdevTakip.Entities;
using OdevTakip.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdevTakip.Models
{ 

    public  class GenericModels
    {
        private static GenericModels nesne;

        private readonly SGenericRepository sGenericRepository;
        public static string kategoriOptionString { get; internal set; }
        public static string durumOptionString { get; internal set; }
        public static string grupOptionString { get; internal set; }

        private GenericModels()
        {
            sGenericRepository = new SGenericRepository();
            List<AdEntity> kategoriList = sGenericRepository.Select<AdEntity>("select * from public.kategori", null);

            kategoriOptionString = "";

            foreach (AdEntity item in kategoriList)
            {
                kategoriOptionString += $"<option value='{item.id}'>{item.ad}</option>";
            }

            List<AdEntity> durumList = sGenericRepository.Select<AdEntity>("select * from public.durum", null);

            durumOptionString = "";

            foreach (AdEntity item in durumList)
            {
                durumOptionString += $"<option value='{item.id}'>{item.ad}</option>";
            }

            GrupOptionRefresh();

        }

        public GenericModels(string grup)
        {

        }

        public static void GrupOptionRefresh()
        {
            grupOptionString = "";

            List<AdEntity> grupList = new SGenericRepository().Select<AdEntity>("select * from public.grup", null);

            foreach (AdEntity item in grupList)
            {
                grupOptionString += $"<option value='{item.id}'>{item.ad}</option>";
            }

        }


        public static GenericModels Nesne()
        {
            if (nesne == null)
                nesne = new GenericModels();

            return nesne;
        }
    }
}
