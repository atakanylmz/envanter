using codefirst_deneme.Models;

namespace codefirst_deneme.ViewModels
{
    public class KullaniciEkleDuzenleViewModel
    {
        public KullaniciEkleDuzenleViewModel()
        {
            Roller = new List<Rol>();
        }
        public int Id { get; set; }
        public string Eposta { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int RolId { get; set; }
        public List<Rol> Roller { get; set; }

        public bool RolA_chk { get; set; }
        public bool RolB_chk { get; set; }
    }
}
