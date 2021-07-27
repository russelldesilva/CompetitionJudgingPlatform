using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB_ASG.DAL;
using WEB_ASG.Models;

namespace WEB_ASG.Controllers
{
    public class JudgeController : Controller
    {
        private AreaInterestDAL areaInterestContext = new AreaInterestDAL();
        private JudgeDAL judgeContext = new JudgeDAL();
        // GET: JudgeController
        public ActionResult Index()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Judge" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Judge"))
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        // GET: JudgeController/Details/5
        public ActionResult Details(int id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Judge" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Judge"))
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        // GET: JudgeController/Create
        public ActionResult Create()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Judge" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Judge"))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewData["SalutationList"] = GetSalutations();
            ViewData["aoiList"] = GetAreaOfInterest();
            return View();
        }

        public ActionResult CreateCriteria()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Judge"))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewData["CompetitionName"] = GetCompetition();
            return View();
        }

        public ActionResult SelectCompetitor()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Judge"))
            {
                return RedirectToAction("Login", "Home");
            }
            int competitionID = HttpContext.Session.GetInt32("competitionID").Value;
            ViewData["CompetitorNames"] = GetCompetitorList(competitionID);
            return View();
        }
        public ActionResult AddCriteriaScore()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Judge"))
            {
                return RedirectToAction("Login", "Home");
            }
            int competitionID = HttpContext.Session.GetInt32("competitionID").Value;
            List<CompetitionScoreViewModel> criteriaList = judgeContext.GetAllCriteriaCompetition(competitionID);
            return View(criteriaList);
        }

        public ActionResult SelectCompetition()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Judge"))
            {
                return RedirectToAction("Login", "Home");
            }
            ViewData["CompetitionNames"] = GetCompetitionAssignedTo();
            return View();
        }

        public ActionResult ViewCriteria()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Judge" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Judge"))
            {
                return RedirectToAction("Login", "Home");
            }
            List<CriteriaViewModel> criteriaList = judgeContext.GetAllCriteriaViewModel();
            return View(criteriaList);
        }

        public ActionResult EditCriteriaScores()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Judge" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Judge"))
            {
                return RedirectToAction("Login", "Home");
            }
            int competitorID = HttpContext.Session.GetInt32("competitorID").Value;
            int competitionID = HttpContext.Session.GetInt32("competitionID").Value;
            List<CompetitionScoreViewModel> competitionScoreList = judgeContext.GetAllCompetitionScoreViewModel(competitorID, competitionID);
            return View(competitionScoreList);
        }

        private List<SelectListItem> GetCompetitionAssignedTo()
        {
            int judgeID = HttpContext.Session.GetInt32("ID").Value;
            List<CompetitionJudgeViewModel> competitionAssignedTo = judgeContext.GetCompetitionAssigned(judgeID);
            List<SelectListItem> competitionAssigned = new List<SelectListItem>();
            foreach (var com in competitionAssignedTo)
            {
                competitionAssigned.Add(new SelectListItem
                {
                    Value = Convert.ToString(com.CompetitionID),
                    Text = com.CompetitionName
                });

            }
            return competitionAssigned;
        }
        private List<SelectListItem> GetCompetition()
        {
            List<Competition> competition = judgeContext.GetCompetitionName();
            List<SelectListItem> competitionName = new List<SelectListItem>();
            foreach (var com in competition)
            {
                competitionName.Add(new SelectListItem
                {
                    Value = Convert.ToString(com.CompetitionID),
                    Text = com.CompetitionName
                });

            }
            return competitionName;
        }
        private List<SelectListItem> GetAreaOfInterest()
        {
            List<AreaInterest> areaInterests = areaInterestContext.GetAreaInterests();
            List<SelectListItem> areaofInterest = new List<SelectListItem>();
            foreach (var aoi in areaInterests)
            {
                areaofInterest.Add(new SelectListItem
                {
                    Value = Convert.ToString(aoi.AreaInterestID),
                    Text = aoi.Name
                });

            }
            return areaofInterest;
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

        // POST: JudgeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Judge judge)
        {
            ViewData["SalutationList"] = GetSalutations();
            ViewData["aoiList"] = GetAreaOfInterest();
            if (ModelState.IsValid)
            {
                //Add competitor record to database
                judge.JudgeID = judgeContext.Add(judge);
                //Redirect user to Judge/Index view
                return RedirectToAction("Index");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(judge);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCriteriaScore(IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                int criteriaid = Convert.ToInt32(collection["item.CriteriaID"]);
                int score = int.Parse(collection["item.Score"]);
                int competitionID = HttpContext.Session.GetInt32("competitionID").Value;
                int competitorID = HttpContext.Session.GetInt32("competitorID").Value;
                if (score > 10 || score < 0)
                {
                    TempData["Message"] = "Score must be between 0 and 10";
                    return RedirectToAction("AddCriteriaScore");
                }
                else
                {
                    CompetitionScore competitionscore = new CompetitionScore();
                    competitionscore.CriteriaID = criteriaid;
                    competitionscore.CompetitorID = competitorID;
                    competitionscore.CompetitionID = competitionID;
                    competitionscore.Score = score;
                    judgeContext.AddCompetitionScore(competitionscore);
                    TempData["SucessfulMessage"] = "Score Added";
                    return RedirectToAction("AddCriteriaScore");
                }
                
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(collection);
            }
        }
        private List<SelectListItem> GetCompetitorList(int competitionID)
        {
            List<Competitor> competitorList = judgeContext.GetAllCompetitors(competitionID);
            List<SelectListItem> competitorName = new List<SelectListItem>();
            foreach (var com in competitorList)
            {
                competitorName.Add(new SelectListItem
                {
                    Value = Convert.ToString(com.CompetitorID),
                    Text = com.CompetitorName
                });

            }
            return competitorName;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectCompetitor(CompetitionScore competitionScore)
        {
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetInt32("competitorID", competitionScore.CompetitorID);
                //Redirect user to Judge/Index view
                return RedirectToAction("EditCriteriaScores");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(competitionScore);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectCompetition(CompetitionJudgeViewModel competitionjudge)
        {
            ViewData["CompetitionNames"] = GetCompetitionAssignedTo();
            if (ModelState.IsValid)
            {
                HttpContext.Session.SetInt32("competitionID", competitionjudge.CompetitionID);
                //Redirect user to Judge/Index view
                return RedirectToAction("SelectCompetitor");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(competitionjudge);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCriteria(Criteria criteria)
        {
            ViewData["CompetitionName"] = GetCompetition();
            if (ModelState.IsValid)
            {
                //Add staff record to database
                criteria.CriteriaID = judgeContext.AddCriteria(criteria);
                //Redirect user to Judge/ViewCriteria view
                return RedirectToAction("ViewCriteria");
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(criteria);
            }
        }

        // GET: JudgeController/Edit/5
        public ActionResult Edit(int? id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Judge" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Judge"))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            { //Query string parameter not provided
              //Return to listing page, not allowed to edit
                return RedirectToAction("ViewCriteria");
            }
            Criteria criteria = judgeContext.GetDetails(id.Value);
            if (criteria == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("ViewCriteria");
            }
            return View(criteria);
        }

        public ActionResult EditScores(int? id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Judge" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Judge"))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            { //Query string parameter not provided
              //Return to listing page, not allowed to edit
                return RedirectToAction("EditCriteriaScores");
            }
            int competitionID = HttpContext.Session.GetInt32("competitionID").Value;
            int competitorID = HttpContext.Session.GetInt32("competitorID").Value;
            CompetitionScoreViewModel competitionScore = judgeContext.GetScoreDetails(id.Value, competitorID, competitionID);
            if (competitionScore == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("EditCriteriaScores");
            }
            return View(competitionScore);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditScores(CompetitionScoreViewModel competitionScore)
        {
            //Get branch list for drop-down list
            //in case of the need to return to Edit.cshtml view
            if (ModelState.IsValid)
            {
                Competition releaseDate = judgeContext.GetReleasedDate(competitionScore.CompetitionID);
                if (releaseDate.ResultReleaseDate > DateTime.Now)
                {
                    //Update staff record to database
                    judgeContext.UpdateScore(competitionScore);
                    return RedirectToAction("EditCriteriaScores");
                }
                else
                {
                    TempData["Message"] = "You Cannot Edit Scores After Release Date";
                    return View(competitionScore);
                }
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                return View(competitionScore);
            }
        }

        // POST: JudgeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Criteria criteria)
        {
            //Get branch list for drop-down list
            //in case of the need to return to Edit.cshtml view
            if (ModelState.IsValid)
            {
                //Update staff record to database
                judgeContext.Update(criteria);
                return RedirectToAction("ViewCriteria");
            }
            else
            {
                //Input validation fails, return to the view
                //to display error message
                return View(criteria);
            }
        }

        // GET: JudgeController/Delete/5
        public ActionResult Delete(int? id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Judge" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Judge"))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            { //Query string parameter not provided
              //Return to listing page, not allowed to edit
                return RedirectToAction("ViewCriteria");
            }
            Criteria criteria = judgeContext.GetDetails(id.Value);
            if (criteria == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("ViewCriteria");
            }
            return View(criteria);
        }
        public ActionResult DeleteScores(int? id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Judge" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Judge"))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            { //Query string parameter not provided
              //Return to listing page, not allowed to edit
                return RedirectToAction("ViewCriteria");
            }
            int competitionID = HttpContext.Session.GetInt32("competitionID").Value;
            int competitorID = HttpContext.Session.GetInt32("competitorID").Value;
            CompetitionScoreViewModel competitionScore = judgeContext.GetScoreDetails(id.Value, competitorID, competitionID);
            if (competitionScore == null)
            {
                //Return to listing page, not allowed to edit
                return RedirectToAction("ViewCriteria");
            }
            return View(competitionScore);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteScores(CompetitionScoreViewModel competitionScore)
        {
            // Delete the staff record from database
            judgeContext.DeleteScore(competitionScore.CriteriaID, competitionScore.CompetitorID, competitionScore.CompetitionID, competitionScore.Score);
            return RedirectToAction("EditCriteriaScores");
        }

        // POST: JudgeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Criteria criteria)
        {
            // Delete the staff record from database
            judgeContext.Delete(criteria.CriteriaID);
            return RedirectToAction("ViewCriteria");
        }
        
    }
}
