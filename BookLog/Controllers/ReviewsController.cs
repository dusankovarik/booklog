using Microsoft.AspNetCore.Mvc;

namespace BookLog.Controllers {
    public class ReviewsController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
