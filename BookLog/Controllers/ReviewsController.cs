using BookLog.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookLog.Controllers {
    public class ReviewsController : Controller {
        private ReviewService _service;

        public ReviewsController(ReviewService service) {
            _service = service;
        }

        public IActionResult Index() {
            return View();
        }
    }
}
