using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace goofy.Controllers
{
    public class Users : Controller
    {
        private Models.UserData.AvailableUser au = new Models.UserData.AvailableUser();

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(au.AllUsers().ToList());
            //return View();
        }
    }
}
