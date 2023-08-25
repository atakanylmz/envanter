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
                Id= k.Id,
                Eposta = k.Eposta,
                Ad = k.Ad,
                Soyad = k.Soyad,
                Roller = string.Join(", ", k.KullaniciRols.Select(kr => kr.Rol.Ad))
            }).ToList();

            return View(kullanicilarListesi);
        }

        public async Task<IActionResult> EkleDuzenle(int id)
        {
            KullaniciEkleDuzenleViewModel kullaniciEkleDuzenleViewModel;
            if (id == 0)
            {
                kullaniciEkleDuzenleViewModel = new KullaniciEkleDuzenleViewModel();
            }
            else
            {
                Kullanici? kullanici = await _kullaniciRepository.GetirInclude(id);
                if(kullanici == null)
                {
                    //hata mesajı verilebilir
                    RedirectToAction("Index");
                }
                kullaniciEkleDuzenleViewModel = new KullaniciEkleDuzenleViewModel
                {
                    Ad = kullanici.Ad,
                    Eposta = kullanici.Eposta,
                    Id = id,
                    Soyad = kullanici.Soyad,
                    RolIdler_chk=kullanici.KullaniciRols.Select(x=>x.RolId).ToList(),
                };

            }
            
            kullaniciEkleDuzenleViewModel.TumRoller = await _rolRepository.TumunuGetir();

            return View(kullaniciEkleDuzenleViewModel);
        }



        [HttpPost]
        public async Task<IActionResult> EkleDuzenle(KullaniciEkleDuzenleViewModel kullaniciEkleDuzenleViewModel)
        {
            kullaniciEkleDuzenleViewModel.TumRoller = await _rolRepository.TumunuGetir();


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

                            List<KullaniciRol> kullaniciRoller= new List<KullaniciRol>();
                            DateTime eklemeTarih = DateTime.Now;
                            foreach (var rolId in kullaniciEkleDuzenleViewModel.RolIdler_chk)
                            {
                                kullaniciRoller.Add(new KullaniciRol { KullaniciId = eklenenKullanici.Id, RolId = rolId, EklemeTarihi = eklemeTarih });
                            }
                            await _kullaniciRolRepository.TopluEkle(kullaniciRoller);

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

                                List<KullaniciRol> kullaniciRoller = new List<KullaniciRol>();
                                DateTime eklemeTarih = DateTime.Now;
                                foreach (var rolId in kullaniciEkleDuzenleViewModel.RolIdler_chk)
                                {
                                    kullaniciRoller.Add(new KullaniciRol { KullaniciId = mevcutKullanici.Id, RolId = rolId, EklemeTarihi = eklemeTarih });
                                }
                                mevcutKullanici.KullaniciRols = kullaniciRoller;

                                await _kullaniciRepository.Guncelle(mevcutKullanici);

                                //rolleri de kontrol edeceğiz 
                                //şimdilik yapılmadı


                                scope.Complete();
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


            return RedirectToAction("Index");
        }
    }
}
