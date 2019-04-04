using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdevTakip.Entities;
using OdevTakip.Services;
using System.Collections.Generic;

namespace OdevTakip.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjeService _projeService;

        public ProjectController(IProjeService projeService)
        {
            _projeService = projeService;
        }

        public IActionResult Index()
        {
            List<Proje> projes = _projeService.Select(new Proje());

            return View(projes);
        }

        [HttpPost]
        public IActionResult InsertProject(Proje model)
        {

            model.yoneticiid = HttpContext.Session.GetInt32("kullaniciid").Value;
            model.Olusturankisi = model.yoneticiid;

            bool result = _projeService.Insert(model);

            return Redirect("/Project/Index");
        }
    }
}