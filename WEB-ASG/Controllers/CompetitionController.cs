using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_ASG.Models;
using WEB_ASG.DAL;

namespace WEB_ASG.Controllers
{
    public class CompetitionController : Controller
    {
        private CompetitionDAL competitionContext = new CompetitionDAL();

        // GET: CompetitionController
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public ActionResult Index(int? compId1, int? compId2)
        {
            CompetitionViewModel competitionVM = new CompetitionViewModel();
            competitionVM.competitionList = competitionContext.GetAllCompetitions();
            // Check if CompID (id) presents in the query string
            if (compId1 != null)
            {
                ViewData["selectedCompID1"] = compId1.Value;
                ViewData["selectedCompID2"] = "";
                // Get list of Judges for the Competition
                competitionVM.judgeVMList = competitionContext.GetCompetitionJudge(compId1.Value);
            }
            else if(compId2 != null)
            {
                ViewData["selectedCompID1"] = "";
                ViewData["selectedCompID2"] = compId2.Value;
                // Get list of Criterias for the Competition
                competitionVM.criteriaList = competitionContext.GetCompetitionCriteria(compId2.Value);
            }
            else
            {
                ViewData["selectedCompID1"] = "";
                ViewData["selectedCompID2"] = "";
            }
            return View(competitionVM);
        }

        // GET: CompetitionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompetitionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompetitionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompetitionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompetitionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompetitionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompetitionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
