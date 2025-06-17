using System;

namespace WorkFollow.Entities;

public class Etkinlik : BaseEntity
{
    public string baslik { get; set; }
    public string aciklama { get; set; }
    public int durumid { get; set; }
    public int kategoriid { get; set; }
    public DateTime baslangictarihi { get; set; }
    public DateTime bitistarihi { get; set; }
    public int atananid { get; set; }
    public int projeid { get; set; }
}