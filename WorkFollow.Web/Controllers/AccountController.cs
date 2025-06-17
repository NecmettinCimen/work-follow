using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFollow.Entities;
using WorkFollow.Services;
using WorkFollow.Web.Filters;

namespace WorkFollow.Web.Controllers;

public class AccountController : Controller
{
    private readonly IKullaniciService _kullaniciService;

    public AccountController(IKullaniciService kullaniciService)
    {
        _kullaniciService = kullaniciService;
    }

    [CustomAuthorize]
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Login()
    {
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Register(Kullanici model)
    {
        var result = _kullaniciService.Insert(model);
        if (result != null)
        {
            HttpContext.Session.SetInt32("kullaniciid", result.Id);
            HttpContext.Session.SetString("ad", result.Ad);
            HttpContext.Session.SetString("soyad", result.Soyad);

            return Redirect("/Home/Index");
        }

        ViewData["error"] = "true";
        return Redirect("/Account/Register");
    }

    [HttpPost]
    public ActionResult Login(Kullanici model)
    {
        var result = _kullaniciService.LoginKontrol(model);
        if (result != null)
        {
            // o an sisteme giriş yapan kullanıcı sakladıgımız yer
            HttpContext.Session.SetInt32("kullaniciid", result.Id);
            HttpContext.Session.SetString("ad", result.Ad);
            HttpContext.Session.SetString("soyad", result.Soyad);

            return Redirect("/Home/Index");
        }

        ViewData["error"] = "true";
        return View();
    }

    [CustomAuthorize]
    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Redirect("/Account/Login");
    }
}