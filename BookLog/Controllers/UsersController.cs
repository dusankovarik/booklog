using BookLog.Models;
using BookLog.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookLog.Controllers {
    public class UsersController : Controller {
        private UserManager<AppUser> _userManager;

        public UsersController(UserManager<AppUser> userManager) {
            _userManager = userManager;
        }

        public IActionResult Index() {
            return View();
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel user) {
            if (ModelState.IsValid) {
                AppUser appUser = new AppUser {
                    UserName = user.Name,
                    Email = user.Email,
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }
            return View(user);
        }

        private void AddErrors(IdentityResult result) {
            foreach (IdentityError error in result.Errors) {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}
