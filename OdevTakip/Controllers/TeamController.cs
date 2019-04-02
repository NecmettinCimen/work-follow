using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdevTakip.Entities;
using OdevTakip.Services;

namespace OdevTakip.Controllers
{
    public class TeamController : Controller
    {
        private readonly IGrupService _grupService;

        public TeamController(IGrupService grupService)
        {
            _grupService = grupService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertTeam(Grup model)
        {
            model.yoneticiid = HttpContext.Session.GetInt32("kullaniciid").Value;
            model.Olusturankisi = model.yoneticiid;
            bool result = _grupService.Insert(model);

            if (result)
            {
                ViewData["success"] = "true";
            }
            else
            {
                ViewData["success"] = "false";
            }

            return Redirect("/Home/Index");
        }
    }
}
