using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdevTakip.Entities;
using OdevTakip.Services;

namespace OdevTakip.Controllers
{
    public class AccountController : Controller
    {
        private readonly IKullaniciService _kullaniciService;

        public AccountController(IKullaniciService kullaniciService)
        {
            _kullaniciService = kullaniciService;
        }

        public IActionResult Index() => View("Login");

        public IActionResult Login() => View();

        public IActionResult Register() => View();

        [HttpPost]
        public ActionResult Register(Kullanici model)
        {

            bool result = _kullaniciService.Insert(model);
            if (result)
            {
                HttpContext.Session.SetInt32("kullaniciid", model.Id);
                HttpContext.Session.SetString("ad", model.Ad);
                HttpContext.Session.SetString("soyad", model.Soyad);

                return Redirect("/Home/Index");
            }
            else
            {
                ViewData["error"] = "true";
                return Redirect("/Account/Login");
            }
        }

        [HttpPost]
        public ActionResult Login(Kullanici model)
        {

            Kullanici result = _kullaniciService.LoginKontrol(model);
            if (result!=null)
            {
                HttpContext.Session.SetInt32("kullaniciid", result.Id);
                HttpContext.Session.SetString("ad", result.Ad);
                HttpContext.Session.SetString("soyad", result.Soyad);

                return Redirect("/Home");
            }
            else
            {
                ViewData["error"] = "true";
                return View();
            }
        }

    }
}
