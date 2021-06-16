using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_ASG.Models;

namespace WEB_ASG.Controllers
{
    public class AdminController : Controller
    {
        private AreaInterest aoi = new AreaInterest { AreaInterestID = 1, Name = "Coding", JudgeList = new List<Judge>() };
        private Judge j = new Judge
        {
            AreaIntrestID = 1,
            JudgeID = 1,
            JudgeName = "Ayken",
            EmailAddr = "hfcaltarservers@gmail.com",
            Salutation = "Mrs"
        };
        private Judge j2 = new Judge
        {
            AreaIntrestID = 1,
            JudgeID = 2,
            JudgeName = "Russell",
            EmailAddr = "hfcaltarservers@gmail.com",
            Salutation = "Mrs"
        };
        public AdminController()
        {
            aoi.JudgeList.Add(j);
            aoi.JudgeList.Add(j2);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ViewAreaInterest()
        {
            return View(aoi);
        }
        public IActionResult CreateAreaInterest()
        {
            return View(aoi);
        }
        [HttpPost]
        public ActionResult CreateAreaInterest(AreaInterest areaInterest)
        {
            aoi = areaInterest;
            return View(aoi);
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
