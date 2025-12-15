using BookLog.Dtos;
using BookLog.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookLog.Controllers {
    public class GenresController : Controller {
        private GenreService _service;

        public GenresController(GenreService service) {
            _service = service;
        }

        public async Task<IActionResult> IndexAsync() {
            var allGenres = await _service.GetAllAsync();
            return View(allGenres);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GenreDto newGenre) {
            await _service.CreateAsync(newGenre);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id) {
            var genreToEdit = await _service.GetByIdAsync(id);
            return genreToEdit != null ? View(genreToEdit) : View("NotFound");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GenreDto genreDto) {
            await _service.UpdateAsync(genreDto);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) {
                return View("NotFound");
            }
            return RedirectToAction("Index");
        }
    }
}
