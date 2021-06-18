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
        private Competition c = new Competition
        {
            AreaInterestID = 1,
            CompetitionID = 1,
            CompetitionName = "C# Challenge",
            StartDate = new DateTime(2021, 06, 17, 14, 14, 0, 0),
            EndDate = new DateTime(2021, 12, 31, 15, 0, 0, 0),
            ResultReleaseDate = new DateTime(2022, 01, 01, 15, 0, 0, 0)
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
            return View(areaInterest);
        }
        public IActionResult EditComp(Competition comp)
        {
            comp = c;
            return View(comp);
        }
        public IActionResult AddJudge()
        {
            return View();
        }
    }
}
