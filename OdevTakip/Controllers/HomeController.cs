using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdevTakip.Entities;
using OdevTakip.Filters;
using OdevTakip.Models;
using OdevTakip.Services;
using System.Collections.Generic;
using System.Diagnostics;

namespace OdevTakip.Controllers
{
    [CustomAuthorizeAttribute]
    public class HomeController : Controller
    {
        private readonly IGrupService _grupService;

        public HomeController(IGrupService grupService)
        {
            _grupService = grupService;
        }

        public IActionResult Index(string sort)
        {
            int sessionKisiId = HttpContext.Session.GetInt32("kullaniciid").Value;
            List<Grup> grupListesi = _grupService.Select(new Grup() { Olusturankisi = sessionKisiId });

            return View(new GenericIndexDto<Grup>(grupListesi, sort));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}
