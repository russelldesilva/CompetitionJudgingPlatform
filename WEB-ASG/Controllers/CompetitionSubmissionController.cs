using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_ASG.Models;
using WEB_ASG.DAL;
using System.IO;
using System.Diagnostics;

namespace WEB_ASG.Controllers
{
    public class CompetitionSubmissionController : Controller
    {
        private CompetitionDAL competitionContext = new CompetitionDAL();
        private CompetitionSubmissionDAL competitionSubmissionContext = new CompetitionSubmissionDAL();

        // GET: CompetitionSubmissionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompetitionSubmissionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompetitionSubmissionController/Create
        public ActionResult Create(int? id)
        {
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
            int competitorID = HttpContext.Session.GetInt32("ID").Value;
            Competition competition = competitionContext.GetCompetition(id.Value);
            if (competition == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index");
            }
            CompetitionSubmissionViewModel competitionSubmissionVM = new CompetitionSubmissionViewModel
            {
                CompetitionID = id.Value,
                CompetitionName = competition.CompetitionName,
                CompetitorID = competitorID
            };
            return View(competitionSubmissionVM);
        }

        // POST: CompetitionSubmissionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompetitionSubmissionViewModel competitionSubmissionVM)
        {
            if (ModelState.IsValid)
            {
                //Set current DateTime as DateTimeFileUpload
                //if (competitionSubmissionVM.fileToUpload != null)
                //{
                //    // Find the filename extension of the file to be uploaded.
                //    competitionSubmissionVM.FileSubmitted = Path.GetFileName(competitionSubmissionVM.fileToUpload.FileName);
                //    Console.WriteLine(competitionSubmissionVM.FileSubmitted);
                //    // Get the complete path to the images folder in server
                //    string savePath = Path.Combine(
                //     Directory.GetCurrentDirectory(),
                //     "wwwroot\\images", competitionSubmissionVM.FileSubmitted);
                //    // Upload the file to server
                //    using (var fileSteam = new FileStream(
                //     savePath, FileMode.Create))
                //    {
                //        await competitionSubmissionVM.fileToUpload.CopyToAsync(fileSteam);
                //    }
                //}
                //Add competitor record to database
                CompetitionSubmission competitionSubmissions = MapToCompetitionSubmission(competitionSubmissionVM);
                competitionSubmissionContext.Add(competitionSubmissions);
                //Redirect user to Competitor/Index view
                return RedirectToAction("Index","Competitor");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(competitionSubmissionVM);
            }
        }

        public CompetitionSubmission MapToCompetitionSubmission(CompetitionSubmissionViewModel competitionSubmissionVM)
        {
            return new CompetitionSubmission
            {
                CompetitionID = competitionSubmissionVM.CompetitionID,
                CompetitorID = competitionSubmissionVM.CompetitorID,
                FileSubmitted = competitionSubmissionVM.FileSubmitted,
                DateTimeFileUpload = DateTime.Now,
                VoteCount = 0
            };
        }

        // GET: CompetitionSubmissionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompetitionSubmissionController/Edit/5
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

        // GET: CompetitionSubmissionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompetitionSubmissionController/Delete/5
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
