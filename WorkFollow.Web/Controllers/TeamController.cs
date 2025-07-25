﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFollow.Entities;
using WorkFollow.Services;
using WorkFollow.Web.Filters;
using WorkFollow.Web.Models;

namespace WorkFollow.Web.Controllers;

[CustomAuthorize]
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
        var result = false;
        if (model.Id == 0)
        {
            model.yoneticiid = HttpContext.Session.GetInt32("kullaniciid").Value;
            model.Olusturankisi = model.yoneticiid;
            result = _grupService.Insert(model);
        }
        else
        {
            model.Guncelleyenkisi = HttpContext.Session.GetInt32("kullaniciid").Value;
            result = _grupService.Update(model);
        }

        if (result)
        {
            GenericModels.GrupOptionRefresh();

            ViewData["success"] = "true";
        }
        else
        {
            ViewData["success"] = "false";
        }

        return Redirect("/Home/Index");
    }

    [HttpPost]
    public ActionResult DeleteTeam([FromBody] Grup model)
    {
        bool result = _grupService.Delete(model);
        return Json(result);
    }

    [HttpPost]
    public ActionResult EditTeam([FromBody] Grup model)
    {
        Grup result = _grupService.First(model);
        return Json(result);
    }
}