namespace codefirst_deneme.Models
{
    public class KullaniciRol
    {
        public int KullaniciId { get; set; }
        public  Kullanici Kullanici { get; set; }

        public int RolId { get; set; }
        public  Rol Rol { get; set; }
        public DateTime EklemeTarihi { get; set; }

    }
}
