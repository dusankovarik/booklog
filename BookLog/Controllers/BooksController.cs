using BookLog.Dtos;
using BookLog.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookLog.Controllers {
    public class BooksController : Controller {
        private BookService _bookService;
        private AuthorService _authorService;
        private GenreService _genreService;

        public BooksController(BookService bookService, AuthorService authorService, GenreService genreService) {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
        }

        public async Task<IActionResult> Index() {
            var allBooks = await _bookService.GetAllAsync();
            return View(allBooks);
        }

        public async Task<IActionResult> Create() {
            var newBook = new BookCreateEditDto() {
                Authors = (await _authorService.GetAllAsync()).ToList(),
                Genres = (await _genreService.GetAllAsync()).ToList(),
            };
            return View(newBook);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCreateEditDto newBook) {
            if (!ModelState.IsValid) {
                newBook.Authors = (await _authorService.GetAllAsync()).ToList();
                newBook.Genres = (await _genreService.GetAllAsync()).ToList();
                return View(newBook);
            }
            await _bookService.CreateAsync(newBook);
            return RedirectToAction("Index");
        }
    }
}
