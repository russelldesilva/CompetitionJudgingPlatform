using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WEB_ASG.Models;
using WEB_ASG.DAL;

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
