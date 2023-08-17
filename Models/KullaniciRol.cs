namespace codefirst_deneme.Models
{
    public class KullaniciRol
    {
        public int KullaniciId { get; set; }
        public required Kullanici Kullanici { get; set; }

        public int RolId { get; set; }
        public required Rol Rol { get; set; }
        public DateTime EklemeTarihi { get; set; }

    }
}
