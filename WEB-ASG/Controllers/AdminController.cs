using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_ASG.Models;
using WEB_ASG.DAL;

namespace WEB_ASG.Controllers
{
    public class AdminController : Controller
    {
        private AreaInterestDAL areaInterestContext = new AreaInterestDAL();
        private CompetitionDAL competitionContext = new CompetitionDAL();
        private JudgeDAL judgeContext = new JudgeDAL();
        public AdminController()
        {

        }
        public ActionResult Index()
        {
            List<AreaInterest> areaInterests = areaInterestContext.GetAreaInterests();
            ViewData["aoiList"] = areaInterests;
            return View();
        }
        public ActionResult ViewAreaInterest(int aoiID)
        {
            AreaInterest aoi = areaInterestContext.GetDetails(aoiID);
            competitionContext.GetCompetitions();
            aoi.CompetitonList = competitionContext.GetDetails("AreaInterestID", aoiID);
            return View(aoi);
        }
        public IActionResult CreateAreaInterest()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateAreaInterest(AreaInterest aoi)
        {
            aoi.AreaInterestID = areaInterestContext.Add(aoi);
            return RedirectToAction("Index");
        }
        public ActionResult EditComp()
        {
            List<AreaInterest> areaInterests = areaInterestContext.GetAreaInterests();
            return View();
        }
        public ActionResult AddJudge()
        {
            List<Judge> judges = judgeContext.GetJudges();
            return View();
        }
    }
}
