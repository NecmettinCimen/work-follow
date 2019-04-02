using Microsoft.AspNetCore.Mvc;
using OdevTakip.Entities;
using OdevTakip.Models;
using OdevTakip.Services;
using System.Collections.Generic;
using System.Diagnostics;

namespace OdevTakip.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGrupService _grupService;

        public HomeController(IGrupService grupService)
        {
            _grupService = grupService;
        }

        public IActionResult Index()
        {
            List<Grup> grupListesi = _grupService.Select(new Grup());

            return View(grupListesi);
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
