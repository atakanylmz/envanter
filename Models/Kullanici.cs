using System.ComponentModel.DataAnnotations.Schema;

namespace codefirst_deneme.Models
{
    public class Kullanici:BaseEntity
    {
        public Kullanici()
        {
            Envanters=new List<Envanter>();
            KullaniciRols=new List<KullaniciRol>();
        }
        public required string Ad { get; set; }

        public required string Soyad { get; set; }

        public required string Eposta { get; set; }

        public ICollection<Envanter> Envanters { get; set; }

        public ICollection<KullaniciRol> KullaniciRols { get; set; }

    }
}
