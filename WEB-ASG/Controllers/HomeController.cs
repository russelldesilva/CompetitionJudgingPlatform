using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WEB_ASG.Models;
using WEB_ASG.DAL;
using Microsoft.AspNetCore.Http;

namespace WEB_ASG.Controllers
{
    public class HomeController : Controller
    {
        private CompetitionDAL competitionContext = new CompetitionDAL();
        private CompetitorDAL competitorContext = new CompetitorDAL();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                //Check if user is admin
                if (loginVM.Email == "admin1@lcu.edu.sg" && loginVM.Password == "p@55Admin")
                {
                    // Store user role “Admin” as a string in session with the key “Role”
                    HttpContext.Session.SetString("Role", "Admin");
                    return RedirectToAction("Index", "Admin");
                }
                //Check if user is judge
                JudgeDAL judgeContext = new JudgeDAL();
                List<Judge> judgeList = judgeContext.GetAllJudges();
                foreach (Judge judge in judgeList)
                {
                    if (loginVM.Email == judge.EmailAddr && loginVM.Password == judge.Password)
                    {
                        // Store user role “Judge” as a string in session with the key “Role”
                        HttpContext.Session.SetString("Role", "Judge");
                        return RedirectToAction("Index", "Judge");
                    }
                }
                //Check if user is competitor
                CompetitorDAL competitorContext = new CompetitorDAL();
                List<Competitor> competitorList = competitorContext.GetAllCompetitor();
                foreach (Competitor competitor in competitorList)
                {
                    if (loginVM.Email == competitor.EmailAddr && loginVM.Password == competitor.Password)
                    {
                        // Store user role “Judge” as a string in session with the key “Role”
                        HttpContext.Session.SetString("Role", "Competitor");
                        return RedirectToAction("Index", "Competitor");
                    }
                }
                //If user does not belong to any the above, return error message
                TempData["Message"] = "Invalid Login Credentials!";
                return View();
            }
            else
            {
                //Input validation fails, return to the Create view
                //to display error message
                return View(loginVM);
            }
        }

        public IActionResult Competition(int compID)
        {
            Competition comp = competitionContext.GetDetails("CompetitionID", compID)[0];
            comp.CompetitorList = competitorContext.GetAllCompetitor();
            return View(comp);           
        }

        public IActionResult ViewCompetition()
        {
            List<Competition> compList = competitionContext.GetCompetitions();
            ViewData["compList"] = compList;

            return View();
        }

        public IActionResult ViewTeams(int competitorID)
        {
            Competitor c = competitorContext.GetDetails(competitorID);

            return View(c);
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
