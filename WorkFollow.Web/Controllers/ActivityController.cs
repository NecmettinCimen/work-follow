using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFollow.Entities;
using WorkFollow.Services;
using WorkFollow.Web.Filters;
using WorkFollow.Web.Models;

namespace WorkFollow.Web.Controllers;

[CustomAuthorize]
public class ActivityController : Controller
{
    private readonly IHostingEnvironment _env;
    private readonly IEtkinlikService _etkinlikService;
    private readonly IKullaniciDokumanService _kullaniciDokumanService;
    private readonly IKullaniciNotService _kullaniciNotService;

    public ActivityController(
        IEtkinlikService etkinlikService,
        IKullaniciNotService kullaniciNotService,
        IKullaniciDokumanService kullaniciDokumanService,
        IHostingEnvironment env)
    {
        _etkinlikService = etkinlikService;
        _kullaniciNotService = kullaniciNotService;
        _kullaniciDokumanService = kullaniciDokumanService;
        _env = env;
    }

    public IActionResult Index(string sort)
    {
        List<Etkinlik> activityList = _etkinlikService.Select(new Etkinlik
        {
            Olusturankisi = HttpContext.Session.GetInt32("kullaniciid").Value
        });

        return View(new GenericIndexDto<Etkinlik>(activityList, sort));
    }

    public IActionResult Notes(int id)
    {
        var kullaniciId = HttpContext.Session.GetInt32("kullaniciid").Value;
        Etkinlik activity = _etkinlikService.Find(new Etkinlik
        {
            Olusturankisi = kullaniciId,
            Id = id
        });

        List<TblNot> tblNotList = _kullaniciNotService.Select(new KullaniciNot(kullaniciId, id));

        return View(new NotesDto
        {
            etkinlik = activity,
            notList = tblNotList
        });
    }

    public IActionResult Files(int id)
    {
        var kullaniciId = HttpContext.Session.GetInt32("kullaniciid").Value;
        Etkinlik activity = _etkinlikService.Find(new Etkinlik
        {
            Olusturankisi = kullaniciId,
            Id = id
        });

        List<Dokuman> dokumanList = _kullaniciDokumanService.Select(new KullaniciDokuman { Etkinlikid = id });

        return View(new FilesDto
        {
            etkinlik = activity,
            dokumanList = dokumanList
        });
    }

    [HttpPost("Upload")]
    public IActionResult Upload(UploadDto model)
    {
        var kullaniciId = HttpContext.Session.GetInt32("kullaniciid").Value;

        var filePath = _env.WebRootPath + "//files//";

        if (model.file.Length > 0)
        {
            if (!Directory.Exists(filePath)) Directory.CreateDirectory(filePath);
            using (var stream = new FileStream(filePath + model.file.FileName, FileMode.Create))
            {
                model.file.CopyTo(stream);
                _kullaniciDokumanService.Insert(new Dokuman
                {
                    Olusturankisi = kullaniciId,
                    Guncelleyenkisi = model.etkinlikid,
                    DokumanData = filePath,
                    Tip = model.file.ContentType,
                    Ad = model.file.FileName,
                    Boyut = model.file.Length
                });
            }
        }

        return Redirect("/Activity/Files/" + model.etkinlikid);
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
    public IActionResult ActivityEdit([FromBody] Etkinlik model)
    {
        return Json(_etkinlikService.Find(model));
    }

    [HttpPost]
    public IActionResult ActivityDelete([FromBody] Etkinlik model)
    {
        bool result = _etkinlikService.Delete(model);

        return Json(result);
    }

    [HttpPost]
    public IActionResult TblNotSave(TblNot model)
    {
        var etkinlikId = model.Olusturankisi;
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

        return Redirect("/Activity/Notes/" + etkinlikId);
    }

    public class NotesDto
    {
        public NotesDto()
        {
            notList = new List<TblNot>();
        }

        public Etkinlik etkinlik { get; set; }
        public List<TblNot> notList { get; set; }
    }

    public class FilesDto
    {
        public Etkinlik etkinlik { get; set; }
        public List<Dokuman> dokumanList { get; set; }
    }

    public class UploadDto
    {
        public int etkinlikid { get; set; }
        public IFormFile file { get; set; }
    }
}