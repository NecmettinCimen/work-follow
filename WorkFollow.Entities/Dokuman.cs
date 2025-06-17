namespace WorkFollow.Entities;

public class Dokuman : BaseEntity
{
    public string Ad { get; set; }
    public string Tip { get; set; }
    public long Boyut { get; set; }
    public string DokumanData { get; set; }
}