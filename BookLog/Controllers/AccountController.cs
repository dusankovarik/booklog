using BookLog.Models;
using BookLog.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookLog.Controllers {
    [Authorize]
    public class AccountController : Controller {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl) {
            var login = new LoginViewModel {
                ReturnUrl = returnUrl
            };
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel login) {
            if (ModelState.IsValid) {
                var appUser = await _userManager.FindByNameAsync(login.UserName);
                if (appUser != null) {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(
                        appUser, login.Password, login.Remember, false);
                    if (result.Succeeded) {
                        return Redirect(login.ReturnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(login.UserName), "Login failed: Invalid username of password");
            }
            return View(login);
        }

        public async Task<IActionResult> Logout() {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() {
            return View();
        }
    }
}
