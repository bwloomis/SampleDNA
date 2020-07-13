using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// custom
using Microsoft.AspNetCore.Authorization;

namespace GeneSeekDemo.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            // bool IsAdmin = currentUser.IsInRole("Admin");
            //var id = _userManager.GetUserId(User); // Get user id:
            ViewData["UserName"] = currentUser.Identity.Name;
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Tables()
        {
            return View();
        }
    }
}
