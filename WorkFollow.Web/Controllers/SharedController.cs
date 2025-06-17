﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WorkFollow.Services;
using WorkFollow.Web.Filters;
using WorkFollow.Web.Models;

namespace WorkFollow.Web.Controllers;

[CustomAuthorize]
public class SharedController : Controller
{
    private readonly IGenericRepository _genericRepository;

    public SharedController(IGenericRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }

    [HttpPost]
    public IActionResult Add([FromBody] SharedAddDto model)
    {
        var sessionKisiId = HttpContext.Session.GetInt32("kullaniciid").Value;

        var sql =
            $"INSERT INTO public.{model.type}(aktif, sil, olusturmatarihi, olusturankisi, ad) VALUES(true, false, now(), {sessionKisiId}, '{model.name}'); ";

        _genericRepository.Insert(sql);

        sql = "select id, ad from public." + model.type;

        List<AdEntity> adEntities = _genericRepository.Select<AdEntity>(sql);

        switch (model.type)
        {
            case "durum":
                GenericModels.DurumOptionRefresh();
                break;
            case "kategori":
                GenericModels.KategoriOptionRefresh();
                break;
        }

        return Json(adEntities);
    }
}