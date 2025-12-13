using Microsoft.AspNetCore.Mvc;

namespace BookLog.Controllers {
    public class AuthorController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
