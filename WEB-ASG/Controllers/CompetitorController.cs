using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_ASG.DAL;
using WEB_ASG.Models;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace WEB_ASG.Controllers
{
    [Authorize]
    public class CompetitorController : Controller
    {
        private CompetitorDAL competitorContext = new CompetitorDAL();
        private CompetitionDAL competitionContext = new CompetitionDAL();
        private CompetitionScoreDAL competitionScoreContext = new CompetitionScoreDAL();

        // GET: CompetitorController
        public ActionResult Index(int? compId)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Competitor" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Competitor"))
            {
                return RedirectToAction("Login", "Home");
            }
            int competitorID = HttpContext.Session.GetInt32("ID").Value;
            CompetitorCompetitionViewModel competitorCompetitionVM = new CompetitorCompetitionViewModel();
            competitorCompetitionVM.competitionList = competitionContext.GetCompetitorCompetition(competitorID);

            // Check if CompID (id) presents in the query string
            if (compId != null)
            {
                ViewData["selectedCompID"] = compId.Value;
                // Get list of scores for the Competition
                competitorCompetitionVM.scoreList = competitionScoreContext.GetAllCompetitionScoreViewModel(competitorID, compId.Value);
            }
            else
            {
                ViewData["selectedCompID"] = "";
            }

            return View(competitorCompetitionVM);
        }

        // GET: CompetitorController/Details/5
        public ActionResult Details(int id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Competitor" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Competitor"))
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        // GET: Competitor/Create
        public ActionResult Create()
        {
            ViewData["SalutationList"] = GetSalutations();
            return View();
        }
        private List<SelectListItem> GetSalutations()
        {
            List<SelectListItem> salutations = new List<SelectListItem>();
            salutations.Add(new SelectListItem
            {
                Value = "Dr",
                Text = "Dr"
            });
            salutations.Add(new SelectListItem
            {
                Value = "Mr",
                Text = "Mr"
            });
            salutations.Add(new SelectListItem
            {
                Value = "Ms",
                Text = "Ms"
            });
            salutations.Add(new SelectListItem
            {
                Value = "Mrs",
                Text = "Mrs"
            });
            salutations.Add(new SelectListItem
            {
                Value = "Mdm",
                Text = "Mdm"
            });
            return salutations;
        }

        // POST: CompetitorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Competitor competitor)
        {
            //Get salutation list for drop-down list
            //in case of the need to return to Create.cshtml view
            ViewData["SalutationList"] = GetSalutations();
            if (ModelState.IsValid)
            {
                //Add competitor record to database
                competitor.CompetitorID = competitorContext.Add(competitor);
                //Redirect user to Competitor/Index view
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(competitor);
            }
        }

        // GET: CompetitorController/Edit/5
        public ActionResult Edit(int? id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Competitor" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Competitor"))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            { //Query string parameter not provided
              //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            Competitor competitor = competitorContext.GetDetails(id.Value);
            if (competitor == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            return View(competitor);
        }

        // POST: CompetitorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Competitor competitor)
        {
            if (ModelState.IsValid)
            {
                //Update competitor record to database
                competitorContext.Update(competitor);
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                return View(competitor);
            }
        }

        // GET: CompetitorController/Delete/5
        public ActionResult Delete(int? id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Competitor" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Competitor"))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            Competitor competitor = competitorContext.GetDetails(id.Value);
            if (competitor == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            return View(competitor);
        }

        // POST: CompetitorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Competitor competitor)
        {
            // Delete the competitor record from database
            competitorContext.Delete(competitor.CompetitorID);
            return RedirectToAction("Index");
        }
    }
}
