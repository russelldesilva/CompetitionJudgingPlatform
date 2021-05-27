using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB_ASG.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewAreaInterest()
        {
            return View();
        }
        public IActionResult CreateAreaInterest()
        {
            return View();
        }
        public IActionResult EditComp()
        {
            return View();
        }
        public IActionResult AddJudge()
        {
            return View();
        }
    }
}
