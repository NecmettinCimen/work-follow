using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OdevTakip.Entities;
using OdevTakip.Services;
using System.Collections.Generic;

namespace OdevTakip.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IEtkinlikService _etkinlikService;
        private readonly IKullaniciNotService _kullaniciNotService;

        public ActivityController(IEtkinlikService etkinlikService, IKullaniciNotService kullaniciNotService)
        {
            _etkinlikService = etkinlikService;
            _kullaniciNotService = kullaniciNotService;
        }

        public IActionResult Index()
        {

            List<Etkinlik> activityList = _etkinlikService.Select(new Etkinlik()
            {
                Olusturankisi = HttpContext.Session.GetInt32("kullaniciid").Value
            });

            return View(activityList);
        }

        public class DetailDto
        {
            public Etkinlik etkinlik { get; set; }
            public List<TblNot> notList { get; set; }
        }

        public IActionResult Detail(int id)
        {
            int kullaniciId = HttpContext.Session.GetInt32("kullaniciid").Value;
            Etkinlik activity = _etkinlikService.Find(new Etkinlik()
            {
                Olusturankisi = kullaniciId,
                Id = id
            });

            List<TblNot> tblNotList = _kullaniciNotService.Select(new KullaniciNot(kullaniciId, id));

            return View(new DetailDto
            {
                etkinlik = activity,
                notList = tblNotList
            });
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

        [HttpPost]
        public IActionResult TblNotSave(TblNot model)
        {
            int etkinlikId = model.Olusturankisi;
            model.Olusturankisi = HttpContext.Session.GetInt32("kullaniciid").Value;

            if (model.Id == 0)
            {
                _kullaniciNotService.Insert(etkinlikId, model);
            }
            else
            {
                model.Guncelleyenkisi = model.Olusturankisi;

                _kullaniciNotService.Update(model);
            }

            return Redirect("/Activity/Detail/"+etkinlikId);
        }


    }
}