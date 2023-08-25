using codefirst_deneme.Models;
using Microsoft.AspNetCore.Mvc;

namespace codefirst_deneme.ViewModels
{
    public class KullaniciEkleDuzenleViewModel
    {
        public KullaniciEkleDuzenleViewModel()
        {
            TumRoller = new List<Rol>();
            RolIdler_chk = new List<int>();
        }
        public int Id { get; set; }
        public string Eposta { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public List<Rol> TumRoller { get; set; }

        [BindProperty]
        public List<int> RolIdler_chk { get; set; }
    }
}
