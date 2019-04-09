using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OdevTakip.Services;

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
            return View();
        }
    }
}