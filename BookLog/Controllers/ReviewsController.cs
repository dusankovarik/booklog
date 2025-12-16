using BookLog.Dtos;
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

        [HttpPost]
        public async Task<IActionResult> Create(ReviewCreateDto reviewCreateDto) {
            if (!ModelState.IsValid) {
                return RedirectToAction("Details", "Books", new { id = reviewCreateDto.BookId });
            }
            await _service.CreateAsync(reviewCreateDto);
            return RedirectToAction("Details", "Books", new { id = reviewCreateDto.BookId });
        }
    }
}
