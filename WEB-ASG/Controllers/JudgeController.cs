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
