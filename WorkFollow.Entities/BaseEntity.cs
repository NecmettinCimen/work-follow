using System;

namespace WorkFollow.Entities;

/// <summary>
///     Bu class database üzerinde tanımladıgımız
///     tüm tablolardaki ortak özellikleri barındırır
/// </summary>
public class BaseEntity
{
    public int Id { get; set; }

    // Bu özellik sayesinde nesne oluştunda otomatik olarak tarih set eder
    public DateTime OlusturmaTarihi { get; set; } = DateTime.Now;
    public int Olusturankisi { get; set; }
    public DateTime GuncellemeTarihi { get; set; } = DateTime.Now;
    public int Guncelleyenkisi { get; set; }
    public bool Aktif { get; set; } = true;
    public bool Sil { get; set; } = false;
}