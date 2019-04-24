namespace OdevTakip.Entities
{
    public class KullaniciNot
    {
        public KullaniciNot()
        {

        }
        public KullaniciNot(int kullaniciId, int id)
        {
            KullaniciId = kullaniciId;
            Etkinlikid = id;
        }

        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public int NotId { get; set; }
        public int Etkinlikid { get; set; }
    }
}
