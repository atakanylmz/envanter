using codefirst_deneme.Models;
using codefirst_deneme.Repositories.Abstract;
using codefirst_deneme.Repositories.Abstract.EfCore;
using codefirst_deneme.Repositories.Concrete;
using codefirst_deneme.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace codefirst_deneme.Controllers
{
    [Authorize(Roles = "A")]
    public class KullaniciYonetimController : Controller
    {
        private readonly IKullaniciRepository _kullaniciRepository;
        private readonly IKullaniciRolRepository _kullaniciRolRepository;
        private readonly IRolRepository _rolRepository;
        public KullaniciYonetimController(
            IKullaniciRepository kullaniciRepository,
            IRolRepository rolRepository,
            IKullaniciRolRepository kullaniciRolRepository)
        {
            _kullaniciRepository = kullaniciRepository;
            _rolRepository = rolRepository;
            _kullaniciRolRepository = kullaniciRolRepository;
        }
        public async Task<IActionResult> Index()
        {
            var kullanicilar = await _kullaniciRepository.TumunuGetirInclude();
            List<KullaniciListeViewModel> kullanicilarListesi = kullanicilar.Select(k => new KullaniciListeViewModel()
            {
                Eposta = k.Eposta,
                Ad = k.Ad,
                Soyad = k.Soyad,
                Roller = string.Join(", ", k.KullaniciRols.Select(kr => kr.Rol.Ad))
            }).ToList();

            return View(kullanicilarListesi);
        }

        public async Task<IActionResult> EkleDuzenle()
        {

            KullaniciEkleDuzenleViewModel kullaniciEkleDuzenleViewModel = new KullaniciEkleDuzenleViewModel();
            kullaniciEkleDuzenleViewModel.Roller = await _rolRepository.TumunuGetir();

            return View(kullaniciEkleDuzenleViewModel);
        }



        [HttpPost]
        public async Task<IActionResult> EkleDuzenle(KullaniciEkleDuzenleViewModel kullaniciEkleDuzenleViewModel)
        {
            kullaniciEkleDuzenleViewModel.Roller = await _rolRepository.TumunuGetir();


            try
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    try
                    {
                        if (kullaniciEkleDuzenleViewModel.Id == 0)
                        {
                            var mevcutKullanici = await _kullaniciRepository.EpostaylaGetirInclude(kullaniciEkleDuzenleViewModel.Eposta);
                            if (mevcutKullanici != null)
                            {
                                scope.Dispose();
                                ViewBag.Mesaj = "Kullanıcı mevcut";
                                return View(kullaniciEkleDuzenleViewModel);
                            }
                            var eklenenKullanici = await _kullaniciRepository.Ekle(new Kullanici
                            {
                                Ad = kullaniciEkleDuzenleViewModel.Ad,
                                Soyad = kullaniciEkleDuzenleViewModel.Soyad,
                                Eposta = kullaniciEkleDuzenleViewModel.Eposta,
                                EklemeTarihi = DateTime.Now,
                            });

                            if (kullaniciEkleDuzenleViewModel.RolA_chk)
                            {
                                await _kullaniciRolRepository.Ekle(new KullaniciRol { KullaniciId = eklenenKullanici.Id, RolId = 1, EklemeTarihi = DateTime.Now });
                            }
                            if (kullaniciEkleDuzenleViewModel.RolB_chk)
                            {
                                await _kullaniciRolRepository.Ekle(new KullaniciRol { KullaniciId = eklenenKullanici.Id, RolId = 2, EklemeTarihi = DateTime.Now });
                            }
                            scope.Complete();

                          return  RedirectToAction("Index");
                        }
                        else
                        {
                            var mevcutKullanici = await _kullaniciRepository.GetirInclude(kullaniciEkleDuzenleViewModel.Id);
                            //kullanıcı yoksa hata mesajı dön
                            //şimdilik yapılmadı

                            if(mevcutKullanici == null) {
                                scope.Dispose();
                                throw new Exception(); }
                            else
                            {
                                //güncelle
                                mevcutKullanici.Ad = kullaniciEkleDuzenleViewModel.Ad;
                                mevcutKullanici.Soyad = kullaniciEkleDuzenleViewModel.Soyad;
                                mevcutKullanici.Eposta = kullaniciEkleDuzenleViewModel.Eposta;
                                await _kullaniciRepository.Guncelle(mevcutKullanici);

                                //rolleri de kontrol edeceğiz 
                                //şimdilik yapılmadı
                           


                            }



                        }

                    }
                    catch (Exception)
                    {
                        scope.Dispose();
                        throw;
                    }

                }


            }
            catch (Exception)
            {

                throw;
            }


            return View(kullaniciEkleDuzenleViewModel);
        }
    }
}
