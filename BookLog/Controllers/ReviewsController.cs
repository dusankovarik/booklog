using BookLog.Dtos;
using BookLog.Services;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> Create(ReviewCreateDto reviewCreateDto) {
            if (!ModelState.IsValid) {
                return RedirectToAction("Details", "Books", new { id = reviewCreateDto.BookId });
            }
            var userName = User.Identity!.Name!;
            await _service.CreateAsync(reviewCreateDto, userName);
            return RedirectToAction("Details", "Books", new { id = reviewCreateDto.BookId });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int reviewId, int bookId) {
            var deleted = await _service.DeleteAsync(reviewId);
            if (!deleted) {
                return View("NotFound");
            };
            return RedirectToAction("Details", "Books", new { id = bookId });
        }
    }
}
