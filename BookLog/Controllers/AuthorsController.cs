using BookLog.Dtos;
using BookLog.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookLog.Controllers {
    public class AuthorsController : Controller {
        private AuthorService _service;

        public AuthorsController(AuthorService service) {
            _service = service;
        }

        public IActionResult Index() {
            var allAuthors = _service.GetAll();
            return View(allAuthors);
        }

        public IActionResult Create() {
            return View();
        }
    }
}
