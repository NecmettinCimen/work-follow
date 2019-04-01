using Microsoft.AspNetCore.Mvc;
using OdevTakip.Entities;
using OdevTakip.Models;
using OdevTakip.Services;
using System.Diagnostics;

namespace OdevTakip.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index() => View("Login");

        public IActionResult Login() => View();

        public IActionResult Register() => View();
    }
}
