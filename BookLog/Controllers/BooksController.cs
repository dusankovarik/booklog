using BookLog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookLog.Controllers {
    public class BooksController : Controller {
        private BookService _service;

        public BooksController(BookService service) {
            _service = service;
        }

        public async Task<IActionResult> Index() {
            var allBooks = await _service.GetAllAsync();
            return View(allBooks);
        }
    }
}
