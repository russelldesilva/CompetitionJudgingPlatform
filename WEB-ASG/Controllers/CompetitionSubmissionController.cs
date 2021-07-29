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
using System.Text.RegularExpressions;

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
            { 
                //Query string parameter not provided
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index", "Competition");
            }
            int competitorID = HttpContext.Session.GetInt32("ID").Value;
            Competition competition = competitionContext.GetCompetition(id.Value);
            if (competition == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index", "Competition");
            }
            if (DateTime.Now > competition.StartDate.AddDays(-3))
            {
                TempData["ErrorMessage"] = "You can only join competition 3 days before the start date";
                return RedirectToAction("Index", "Competition");
            }
            if (ValidateCompetitorExist(id.Value, competitorID))
            {
                TempData["ErrorMessage"] = "Cannot join the same competition again";
                return RedirectToAction("Index", "Competition");
            }
            CompetitionSubmissionViewModel competitionSubmissionVM = new CompetitionSubmissionViewModel
            {
                CompetitionID = id.Value,
                CompetitionName = competition.CompetitionName,
                CompetitorID = competitorID
            };
            return View(competitionSubmissionVM);
        }

        public bool ValidateCompetitorExist(int competitionID, int competitorID)
        {
            List<CompetitionSubmission> competitionSubmissionList = competitionSubmissionContext.GetAllCompetitionSubmission(competitorID);
            foreach(CompetitionSubmission competitionSubmission in competitionSubmissionList)
            {
                if((competitionSubmission.CompetitionID == competitionID) && (competitionSubmission.CompetitorID == competitorID))
                {
                    return true;
                }
            }
            return false;
        }

        // POST: CompetitionSubmissionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompetitionSubmissionViewModel competitionSubmissionVM)
        {
            if (competitionSubmissionVM.fileToUpload != null &&
            competitionSubmissionVM.fileToUpload.Length > 0)
            {
                try
                {
                    // Find the filename extension of the file to be uploaded.
                    string fileName = Path.GetFileName(competitionSubmissionVM.fileToUpload.FileName);
                    // Save uploaded file name
                    if (Regex.IsMatch(fileName, @"^[File]+_+([1-9]|[1-9][0-9]|100)+_+([1-9]|[1-9][0-9]|100)+(.doc|.docx|.pdf|.png|.jpg|.gif|.txt)$"))
                    {
                        competitionSubmissionVM.FileSubmitted = fileName;
                    }
                    else
                    {
                        ViewData["Message1"] = "File must be in following format 'File_CompetitorID_CompetitionID.FileType'. ";
                        ViewData["Message2"] = "File type can only be in .doc/.docx/.pdf/.png/.jpg/.gif/.txt";
                        return View(competitionSubmissionVM);
                    }
                    // Get the complete path to the images folder in server
                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\files", fileName);
                    // Upload the file to server
                    using (var fileSteam = new FileStream(
                        savePath, FileMode.Create))
                    {
                        await competitionSubmissionVM.fileToUpload.CopyToAsync(fileSteam);
                    }
                    ViewData["Message"] = "File uploaded successfully.";
                    System.Diagnostics.Debug.WriteLine("Niceeeeeeeeeeeeeeeeeeeeeeeee");
                }
                catch (IOException)
                {
                    //File IO error, could be due to access rights denied
                    ViewData["Message1"] = "File uploading fail!";
                }
                catch (Exception ex) //Other type of error
                {
                    ViewData["Message1"] = ex.Message;
                }
            }
            //Add competitor record to database
            CompetitionSubmission competitionSubmissions = MapToCompetitionSubmission(competitionSubmissionVM);
            competitionSubmissionContext.Add(competitionSubmissions);
            return RedirectToAction("Index", "Competitor");
        }

        public CompetitionSubmission MapToCompetitionSubmission(CompetitionSubmissionViewModel competitionSubmissionVM)
        {
            return new CompetitionSubmission
            {
                CompetitionID = competitionSubmissionVM.CompetitionID,
                CompetitorID = competitionSubmissionVM.CompetitorID,
                FileSubmitted = competitionSubmissionVM.FileSubmitted,
                DateTimeFileUpload = competitionSubmissionVM.FileSubmitted != null ? DateTime.Now : (DateTime?)null,
                VoteCount = 0
            };
        }

        // GET: CompetitionSubmissionController/Edit/5
        public ActionResult Edit(int? id)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Competitor"))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                //Query string parameter not provided
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index", "Competitor");
            }
            int competitorID = HttpContext.Session.GetInt32("ID").Value;
            Competition competition = competitionContext.GetCompetition(id.Value);
            if (competition == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("Index", "Competitor");
            }
            CompetitionSubmissionViewModel competitionSubmissionVM = competitionSubmissionContext.GetDetails(id.Value, competitorID);
            return View(competitionSubmissionVM);
        }

        // POST: CompetitionSubmissionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CompetitionSubmissionViewModel competitionSubmissionVM)
        {
            if (competitionSubmissionVM.fileToUpload != null &&
            competitionSubmissionVM.fileToUpload.Length > 0)
            {
                try
                {
                    // Find the filename extension of the file to be uploaded.
                    string fileName = Path.GetFileName(competitionSubmissionVM.fileToUpload.FileName);
                    // Save uploaded file name
                    if (Regex.IsMatch(fileName, @"^[File]+_+([1-9]|[1-9][0-9]|100)+_+([1-9]|[1-9][0-9]|100)+(.doc|.docx|.pdf|.png|.jpg|.gif|.txt)$") 
                        && fileName.Split(".")[0] == "File_" + competitionSubmissionVM.CompetitorID + "_" + competitionSubmissionVM.CompetitionID)
                    {
                        competitionSubmissionVM.FileSubmitted = fileName;
                    }
                    else
                    {
                        ViewData["Message1"] = "File must be in following format 'File_CompetitorID_CompetitionID.FileType'. ";
                        ViewData["Message2"] = "File type can only be in .doc/.docx/.pdf/.png/.jpg/.gif/.txt";
                        return View(competitionSubmissionVM);
                    }
                    // Get the complete path to the images folder in server
                    string savePath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\files", fileName);
                    // Upload the file to server
                    using (var fileSteam = new FileStream(
                        savePath, FileMode.Create))
                    {
                        await competitionSubmissionVM.fileToUpload.CopyToAsync(fileSteam);
                    }
                    ViewData["Message"] = "File uploaded successfully.";
                }
                catch (IOException)
                {
                    //File IO error, could be due to access rights denied
                    ViewData["Message1"] = "File uploading fail!";
                }
                catch (Exception ex) //Other type of error
                {
                    ViewData["Message1"] = ex.Message;
                }
            }
            if(competitionSubmissionVM.FileSubmitted == null)
            {
                ViewData["Message1"] = "Please upload a file to submit";
                return View(competitionSubmissionVM);
            }
            //Add competitor record to database
            CompetitionSubmission competitionSubmissions = MapToCompetitionSubmission(competitionSubmissionVM);
            competitionSubmissionContext.Update(competitionSubmissions);
            return RedirectToAction("Index", "Competitor");
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
