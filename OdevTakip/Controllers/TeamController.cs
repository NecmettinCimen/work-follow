using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            bool result = _grupService.Insert(model);

            if (result)
            {

            }
            else
            {

            }

            return Redirect("/Home/Index");
        }
    }
}
