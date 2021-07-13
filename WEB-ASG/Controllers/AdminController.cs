using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_ASG.Models;
using WEB_ASG.DAL;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

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
            // Stop accessing the action if not logged in
            // or account not in the "Admin" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Admin"))
            {
                return RedirectToAction("Login", "Home");
            }
            List<AreaInterest> areaInterests = areaInterestContext.GetAreaInterests();
            ViewData["aoiList"] = areaInterests;
            return View();
        }
        public ActionResult ViewAreaInterest(int aoiID)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Admin" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Admin"))
            {
                return RedirectToAction("Login", "Home");
            }
            AreaInterest aoi = areaInterestContext.GetDetails(aoiID);
            aoi.CompetitonList = competitionContext.GetDetails("AreaInterestID", aoiID);
            return View(aoi);
        }
        public IActionResult CreateAreaInterest()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Admin" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Admin"))
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        [HttpPost]
        public ActionResult CreateAreaInterest(AreaInterest aoi)
        {
            aoi.AreaInterestID = areaInterestContext.Add(aoi);
            return RedirectToAction("Index");
        }
        public ActionResult EditComp(int compID)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Admin" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Admin"))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewData["Message0"] = "";
            ViewData["Message1"] = "";
            Competition comp = new Competition();
            ViewData["aoiList"] = areaInterestContext.GetAreaInterests();
            if (compID != 0)
            {
                comp = competitionContext.GetDetails("CompetitionID", compID)[0];
                List<Judge> judgeList = judgeContext.GetJudges(comp.AreaInterestID);
                comp.JudgeList = judgeContext.GetCompetitionJudges(judgeList, compID);
                HttpContext.Session.SetString("originalList", JsonConvert.SerializeObject(comp.JudgeList));
            }
            if (comp.StartDate == DateTime.MinValue)
            {
                comp.StartDate = DateTime.Today;
                comp.EndDate = DateTime.Today.AddDays(1);
                comp.ResultReleaseDate = DateTime.Today.AddDays(2);
            }
            return View(comp);
        }
        [HttpPost]
        public ActionResult EditComp(Competition comp)
        {
            bool error = false;
            if (comp.StartDate.CompareTo(comp.EndDate) >= 0)
            {
                ViewData["Message0"] = "End Date cannot be earlier or same as Start Date!";
                error = true;
            }
            if (comp.StartDate.CompareTo(comp.ResultReleaseDate) >= 0)
            {
                ViewData["Message1"] = "Result release date cannot be earlier or same as Start Date!";
                error = true;
            }
            if (comp.EndDate.CompareTo(comp.ResultReleaseDate) >= 0)
            {
                ViewData["Message1"] = "Result release date cannot be earlier or same as End Date!";
                error = true;
            }
            if (error)
            {
                ViewData["aoiList"] = areaInterestContext.GetAreaInterests();
                return View(comp);
            }
            else if (comp.CompetitionID == 0)
            {
                competitionContext.Add(comp);
                return RedirectToAction("Index");
            }
            else
            {
                competitionContext.Update(comp);
                List<Judge> originalList = JsonConvert.DeserializeObject<List<Judge>>(HttpContext.Session.GetString("originalList"));
                for (int i = 0; i<comp.JudgeList.Count; i++)
                {
                    if (comp.JudgeList[i].Selected)
                    {
                        if (comp.JudgeList[i].Selected != originalList[i].Selected)
                        {
                            judgeContext.InsertCompetitionJudge(comp.CompetitionID, comp.JudgeList[i].JudgeID);
                        }
                    }
                    else
                    {
                        judgeContext.RemoveCompetitionJudge(comp.CompetitionID, comp.JudgeList[i].JudgeID);
                    }
                }
                return RedirectToAction("Index");
            }
        }
        public ActionResult DeleteAreaInterest(int aoiID)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Admin" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Admin"))
            {
                return RedirectToAction("Login", "Home");
            }
            AreaInterest aoi = areaInterestContext.GetDetails(aoiID);
            aoi.CompetitonList = competitionContext.GetDetails("AreaInterestID", aoiID);
            return View(aoi);
        }
        [HttpPost]
        public ActionResult DeleteAreaInterest(AreaInterest aoi)
        {
            areaInterestContext.Delete(aoi.AreaInterestID);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteComp(int compID)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Admin" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Admin"))
            {
                return RedirectToAction("Login", "Home");
            }
            Competition comp = competitionContext.GetDetails("CompetitionID", compID)[0];
            return View(comp);
        }
        [HttpPost]
        public ActionResult DeleteComp(Competition comp)
        {
            competitionContext.Delete(comp.CompetitionID);
            return RedirectToAction("Index");
        }
    }
}
