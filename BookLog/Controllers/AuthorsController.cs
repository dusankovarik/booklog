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

        [HttpPost]
        public async Task<IActionResult> Create(AuthorDto newAuthor) {
            await _service.CreateAsync(newAuthor);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id) {
            var authorToEdit = await _service.GetByIdAsync(id);
            if (authorToEdit == null) {
                return View("NotFound");
            }
            return View(authorToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AuthorDto author) {
            await _service.UpdateAsync(author);
            return RedirectToAction("Index");
        }
    }
}
