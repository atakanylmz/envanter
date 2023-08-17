namespace codefirst_deneme.Models
{
    public class Rol:BaseEntity
    {
        public Rol()
        {
            KullaniciRols = new List<KullaniciRol>();
        }

        public required string Ad{ get; set; }

        public  ICollection<KullaniciRol> KullaniciRols { get; set; }
    }
}
