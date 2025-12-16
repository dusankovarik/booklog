using BookLog.Dtos;
using BookLog.Services;
using BookLog.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookLog.Controllers {
    public class BooksController : Controller {
        private BookService _bookService;
        private AuthorService _authorService;
        private GenreService _genreService;
        private ReviewService _reviewService;

        public BooksController(
            BookService bookService, AuthorService authorService, GenreService genreService, ReviewService reviewService) {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
            _reviewService = reviewService;
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

        public async Task<IActionResult> Edit(int id) {
            var bookToEdit = await _bookService.GetByIdAsync(id);
            return bookToEdit != null ? View(bookToEdit) : View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BookCreateEditDto bookCreateEditDto) {
            if (!ModelState.IsValid) {
                bookCreateEditDto.Authors = (await _authorService.GetAllAsync()).ToList();
                bookCreateEditDto.Genres = (await _genreService.GetAllAsync()).ToList();
                return View(bookCreateEditDto);
            }
            await _bookService.UpdateAsync(bookCreateEditDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) {
            var deleted = await _bookService.DeleteAsync(id);
            if (!deleted) {
                return View("NotFound");
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id) {
            var book = await _reviewService.GetBookDetailsAsync(id);
            if (book == null) {
                return View("NotFound");
            }
            var reviews = await _reviewService.GetByBookIdAsync(id);
            var bookDetailsVM = new BookDetailsViewModel() {
                Book = book,
                Reviews = reviews,
                NewReview = new ReviewCreateDto() {
                    BookId = id,
                },
            };
            return View(bookDetailsVM);
        }
    }
}
