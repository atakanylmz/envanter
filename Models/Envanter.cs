namespace codefirst_deneme.Models
{
    public class Envanter:BaseEntity
    {
        public required string Ad { get; set; }
        public required int EkleyenId { get; set; }
        public required Kullanici EkleyenKullanici { get; set; }
    }
}
