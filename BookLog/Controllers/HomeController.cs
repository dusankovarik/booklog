using System.Diagnostics;
using BookLog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookLog.Controllers {
    public class HomeController : Controller {
        private UserManager<AppUser> _userMagager;

        public HomeController(UserManager<AppUser> userMagager) {
            _userMagager = userMagager;
        }

        [Authorize]
        public IActionResult Index() {
            string message = "Hello ";
            return View("Index", message);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
