using BookLog.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookLog.Controllers {
    public class BooksController : Controller {
        private BookService _service;

        BooksController(BookService service) {
            _service = service;
        }

        public IActionResult Index() {
            return View();
        }
    }
}
