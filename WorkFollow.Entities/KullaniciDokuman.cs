namespace OdevTakip.Entities
{
    public class KullaniciDokuman
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public int DokumanId { get; set; }
        public int Etkinlikid { get; set; }
    }
}
