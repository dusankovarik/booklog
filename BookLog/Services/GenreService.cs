using BookLog.Dtos;
using BookLog.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLog.Services {
    public class GenreService {
        private ApplicationDbContext _dbContext;

        public GenreService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public IEnumerable<GenreDto> GetAll() {
            var allGenres = _dbContext.Genres;
            var genreDtos = new List<GenreDto>();
            foreach (var genre in allGenres) {
                genreDtos.Add(ModelToDto(genre));
            }
            return genreDtos;
        }

        public async Task CreateAsync(GenreDto newGenre) {
            await _dbContext.Genres.AddAsync(DtoToModel(newGenre));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<GenreDto?> GetByIdAsync(int id) {
            var genre = await _dbContext.Genres.FindAsync(id);
            return genre != null ? ModelToDto(genre) : null;
        }

        public async Task UpdateAsync(GenreDto genreDto) {
            if (genreDto.Id == null) {
                throw new ArgumentNullException("Genre Id is required for update");
            }
            var genreToEdit = await _dbContext.Genres.FindAsync(genreDto.Id.Value);
            if (genreToEdit == null) {
                throw new ArgumentNullException("Genre not found");
            }
            genreToEdit.Name = genreDto.Name;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id) {
            var genreToDelete = await _dbContext.Genres.FindAsync(id);
            if (genreToDelete == null) {
                return false;
            }
            _dbContext.Genres.Remove(genreToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private GenreDto ModelToDto(Genre genre) {
            return new GenreDto() {
                Id = genre.Id,
                Name = genre.Name,
            };
        }

        private Genre DtoToModel(GenreDto genreDto) {
            return new Genre() {
                Name = genreDto.Name,
            };
        }
    }
}
