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
using Microsoft.AspNetCore.Authentication.Cookies;
using Google.Apis.Auth.OAuth2;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Google.Apis.Auth;
using static Google.Apis.Auth.GoogleJsonWebSignature;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;

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
        public IActionResult Login()
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
                    HttpContext.Session.SetString("Name", "Admin");
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
                        HttpContext.Session.SetString("Name", judge.JudgeName);
                        HttpContext.Session.SetInt32("ID", judge.JudgeID);
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
                        // Store user role “Competitor” as a string in session with the key “Role”
                        HttpContext.Session.SetString("Role", "Competitor");
                        HttpContext.Session.SetString("Name", competitor.CompetitorName);
                        HttpContext.Session.SetInt32("ID", competitor.CompetitorID);
                        return RedirectToAction("Index", "Competitor");
                    }
                }
                //If user does not belong to any the above, return error message
                TempData["Message"] = "Invalid Login Credentials!";
                return View();
            }
            else
            {
                //Input validation fails, return to the Login view
                //to display error message
                return View(loginVM);
            }
        }
        public async Task<ActionResult> Logout()
        {
            // Clear authentication cookie
            await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
            // Clear all key-values pairs stored in session state
            HttpContext.Session.Clear();
            // Call the Index action of Home controller
            return RedirectToAction("Login");
        }
        public IActionResult Competition(int compID)
        {
            Competition comp = competitionContext.GetDetails("CompetitionID", compID)[0];
            comp.CompetitorList = competitorContext.GetAllCompetitor();
            return View(comp);
        }

        public IActionResult ViewCompetition()
        {
            List<CompetitionDetailsViewModel> compList = competitionContext.GetAllCompetitions();
            ViewData["compList"] = compList;

            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [Authorize]
        public async Task<ActionResult> GoogleLogin()
        {
            // The user is already authenticated, so this call won't
            // trigger login, but it allows us to access token related values.
            AuthenticateResult auth = await HttpContext.AuthenticateAsync();
            string idToken = auth.Properties.GetTokenValue(
             OpenIdConnectParameterNames.IdToken);
            try
            {
                // Verify the current user logging in with Google server
                // if the ID is invalid, an exception is thrown
                Payload currentUser = await
                GoogleJsonWebSignature.ValidateAsync(idToken);
                string userName = currentUser.Name;
                string eMail = currentUser.Email;
                CompetitorDAL competitorContext = new CompetitorDAL();
                List<Competitor> competitorList = competitorContext.GetAllCompetitor();
                foreach (Competitor competitor in competitorList)
                {
                    if (eMail == competitor.EmailAddr)
                    {
                        // Store user role “Competitor” as a string in session with the key “Role”
                        HttpContext.Session.SetString("Role", "Competitor");
                        HttpContext.Session.SetString("Name", competitor.CompetitorName);
                        HttpContext.Session.SetInt32("ID", competitor.CompetitorID);
                        HttpContext.Session.SetString("LoginID", userName + " / " + eMail);
                        HttpContext.Session.SetString("LoggedInTime",
                         DateTime.Now.ToString());
                        return RedirectToAction("Index", "Competitor");
                    }
                }
                //If user does not belong to any the above, return error message
                TempData["Message"] = "Invalid Login Credentials!";
                return RedirectToAction("Logout");
            }
            catch (Exception e)
            {
                // Token ID is may be tempered with, force user to logout
                return RedirectToAction("Logout");
            }
        }
    }
}
