﻿using Microsoft.AspNetCore.Http;
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
            int sessionKisiId = HttpContext.Session.GetInt32("kullaniciid").Value;
            List<Proje> projes = _projeService.Select(new Proje() { Olusturankisi = sessionKisiId });

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

        [HttpPost]
        public ActionResult EditProject([FromBody]Proje model)
        {
            Proje result = _projeService.First(model);
            return Json(result);
        }

        [HttpPost]
        public ActionResult DeleteProject([FromBody]Proje model)
        {
            bool result = _projeService.Delete(model);
            return Json(result);
        }
    }
}