using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdevTakip.Entities;
using OdevTakip.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OdevTakip.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IEtkinlikService _etkinlikService;

        public ActivityController(IEtkinlikService etkinlikService)
        {
            _etkinlikService = etkinlikService;
        }

        public IActionResult Index()
        {

            List<Etkinlik> activityList = _etkinlikService.Select(new Etkinlik() {
                Olusturankisi = HttpContext.Session.GetInt32("kullaniciid").Value
            });

            return View(activityList);
        }

        [HttpPost]
        public IActionResult ActivitySave(Etkinlik model)
        {
            model.atananid = HttpContext.Session.GetInt32("kullaniciid").Value;

            if (model.Id == 0)
            {
                model.Olusturankisi = model.atananid;
                _etkinlikService.Insert(model);
            }
            else
            {
                model.Guncelleyenkisi = model.Olusturankisi;

                _etkinlikService.Update(model);
            }

            return Redirect("/Activity/Index");
        }
        
        [HttpPost]
        public IActionResult ActivityEdit([FromBody]Etkinlik model)
        {
            return Json(_etkinlikService.Find(model));
        }

        [HttpPost]
        public IActionResult ActivityDelete([FromBody]Etkinlik model)
        {
            bool result = _etkinlikService.Delete(model);

            return Json(result);
        }


    }
}