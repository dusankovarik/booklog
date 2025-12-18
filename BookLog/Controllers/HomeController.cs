using System.Diagnostics;
using BookLog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookLog.Controllers {
    public class HomeController : Controller {
        private UserManager<AppUser> _userManager;

        public HomeController(UserManager<AppUser> userMagager) {
            _userManager = userMagager;
        }

        [Authorize]
        public async Task<IActionResult> IndexAsync() {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string message = "Hello";
            if (user != null) {
                message += " " + user.UserName;
            }
            message += "!";
            return View("Index", message);
        }

        public IActionResult About() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
