using Microsoft.AspNetCore.Mvc;
using OdevTakip.Entities;
using OdevTakip.Models;
using OdevTakip.Services;
using System.Diagnostics;

namespace OdevTakip.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()=>View();


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult KullaniciInsert(Kullanici model)
        {
            KullaniciService kullaniciService = new KullaniciService();

            kullaniciService.Insert(model);

            return View("Index");
        }

    }
}
