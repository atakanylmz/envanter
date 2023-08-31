using codefirst_deneme.Models;
using codefirst_deneme.Repositories.Abstract;
using codefirst_deneme.Repositories.Abstract.EfCore;
using codefirst_deneme.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace codefirst_deneme.Controllers
{
    public class SecurityController : Controller
    {
        private readonly IKullaniciRepository _kullaniciRepository;
        public SecurityController(IKullaniciRepository kullaniciRepository)
        {
            _kullaniciRepository = kullaniciRepository;
        }
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity!=null&&claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

            [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

//servise gidip var mı?


            //varsa
            var kullanici = await _kullaniciRepository.EpostaylaGetirInclude(model.Eposta);
            if (kullanici == null)
            {
                ViewBag.Mesaj = "Kullanıcı bulunamadı";
                return View();
            }

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,kullanici.Eposta),
                new Claim(ClaimTypes.NameIdentifier,kullanici.Id.ToString())
            };
            foreach (var kr in kullanici.KullaniciRols)
            {
                claims.Add(new Claim(ClaimTypes.Role, kr.Rol.Ad));
            }

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties authenticationProperties=new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true,
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity),authenticationProperties);

            return RedirectToAction("Index","Home");
        }




        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Security");
        }

    }
}
