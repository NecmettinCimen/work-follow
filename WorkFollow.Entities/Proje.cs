using System;

namespace OdevTakip.Entities
{
    public class Proje:BaseEntity
    {

        public string ad { get; set; }
        public string aciklama { get; set; }
        public int durumid { get; set; }
        public int kategoriid { get; set; }
        public DateTime baslangictarihi { get; set; }
        public DateTime bitistarihi { get; set; }
        public int yoneticiid { get; set; }
        public int grupid { get; set; }
    }
}
