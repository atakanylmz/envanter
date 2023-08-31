using codefirst_deneme.Models;
using codefirst_deneme.Repositories.Abstract;
using codefirst_deneme.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace codefirst_deneme.Controllers
{
    [Authorize(Roles = "A")]
    public class DonanimYonetimController : Controller
    {
        private readonly IDonanimMarkaRepository _markaRepository;

        public DonanimYonetimController( IDonanimMarkaRepository donanimMarkaRepository)
        {
            _markaRepository = donanimMarkaRepository;
        }


        public async Task<IActionResult> MarkaListe()
        {
            //kullanıcı bilgisi lazım olursa kullanılabilecek kodlar
            //var userIdClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            //string userId=userIdClaim!=null? userIdClaim.Value : "";
            //var Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var markalar = await _markaRepository.TumunuGetir();
            List<DonanimMarkaListeViewModel> markaListeViewModels = markalar.Select(x => new DonanimMarkaListeViewModel()
            {
                Ad = x.Ad,
                Id = x.Id,
                Kullanimda= x.Kullanimda,
            }).ToList();
            return View(markaListeViewModels);
        }

        [HttpPost]
        public async Task<JsonResult?> MarkaKaydet(DonanimMarkaListeViewModel donanimMarka)
        {
            DonanimMarka? markaEntity;
            if (donanimMarka.Id == 0)
            {
                markaEntity = new DonanimMarka { Ad = donanimMarka.Ad, Kullanimda = true };
                var sonuc = await _markaRepository.Ekle(markaEntity);
                return Json(sonuc);
            }
            else
            {
                markaEntity = await _markaRepository.Getir(donanimMarka.Id);
                if (markaEntity == null)
                    return null;
                markaEntity.Kullanimda = true;
                markaEntity.Ad = donanimMarka.Ad;
                await _markaRepository.Guncelle(markaEntity);
                return Json(markaEntity);
            }
        }
    }
}
