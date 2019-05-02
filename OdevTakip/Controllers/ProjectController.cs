using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdevTakip.Entities;
using OdevTakip.Models;
using OdevTakip.Services;
using System.Collections.Generic;

namespace OdevTakip.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjeService _projeService;
        private readonly IEtkinlikService _etkinlikService;

        public ProjectController(IProjeService projeService, IEtkinlikService etkinlikService)
        {
            _projeService = projeService;
            _etkinlikService = etkinlikService;
        }

        public class ProjectIndexDto
        {
            public ProjectIndexDto()
            {
                projes = new List<Proje>();
            }
            public string sort { get; set; }
            public List<Proje> projes { get; set; }

            public string SortSelect()
            {
                if (sort == "Id")
                {
                    return "Date";
                }
                else
                {
                    return "Id";
                }
            }
        }

        public IActionResult Index(string sort)
        {
            int sessionKisiId = HttpContext.Session.GetInt32("kullaniciid").Value;
            List<Proje> projes = _projeService.Select(new Proje() { Olusturankisi = sessionKisiId });

            SortedList sortedList = new SortedList();

            switch (sort)
            {
                case "Id":
                    sortedList.SetSortStrategy(new IdSort());
                    break;
                default:
                case "Date":
                    sortedList.SetSortStrategy(new DateSort());
                    break;
            }

            projes = sortedList.Sort<Proje>(projes);

            return View(new ProjectIndexDto
            {
                projes = projes,
                sort = sort != null ? sort : "Id"
            });
        }
        public class ActivityDto
        {
            public ActivityDto()
            {
                activityList = new List<Etkinlik>();
            }
            public Proje proje { get; set; }
            public List<Etkinlik> activityList { get; set; }
        }

        public IActionResult Activity(int id)
        {
            Proje proje = _projeService.First(new Proje { Id = id });

            List<Etkinlik> activityList = _etkinlikService.Select(new Etkinlik()
            {
                Olusturankisi = HttpContext.Session.GetInt32("kullaniciid").Value,
                projeid = id
            });

            return View(new ActivityDto
            {
                activityList = activityList,
                proje = proje
            });
        }

        [HttpPost]
        public IActionResult InsertProject(Proje model)
        {

            model.yoneticiid = HttpContext.Session.GetInt32("kullaniciid").Value;
            model.Olusturankisi = model.yoneticiid;

            bool result = _projeService.Insert(model);

            if (result)
            {
                GenericModels.ProjeOptionRefresh();
            }

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