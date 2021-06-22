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
        public ActionResult EditComp(int compID)
        {
            Competition comp = new Competition();
            ViewData["aoiList"] = areaInterestContext.GetAreaInterests();
            if (compID != 0)
            {
                comp = competitionContext.GetDetails("CompetitionID", compID)[0];
                List<Judge> judgeList = judgeContext.GetJudges(comp.AreaInterestID);
                comp.JudgeList = judgeContext.GetCompetitionJudges(judgeList, compID);
                HttpContext.Session.SetString("originalList",JsonConvert.SerializeObject(comp.JudgeList));
            }
            return View(comp);
        }
        [HttpPost]
        public ActionResult EditComp(Competition comp)
        {
            if (comp.CompetitionID == 0)
            {
                competitionContext.Add(comp);
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
            }
            return RedirectToAction("Index");
        }
        /*public ActionResult AddJudge(int compID)
        {
            List<Judge> judgeList = judgeContext.GetJudges(comp.AreaInterestID);
            comp.JudgeList = judgeContext.GetCompetitionJudges(judgeList, compID);
            return View(comp);
        }
        [HttpPost]
        public ActionResult AddJudge(Competition comp)
        {
            foreach (Judge j in comp.JudgeList)
            {
                if (j.Selected)
                {
                    judgeContext.UpdateCompetitionJudge(j.JudgeID, comp.CompetitionID);
                }
                else
                {
                    judgeContext.UpdateCompetitionJudge(j.JudgeID, 0);
                }
            }
            return RedirectToAction("Index");
        }*/
    }
}
