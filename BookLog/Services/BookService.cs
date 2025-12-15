using BookLog.Dtos;
using BookLog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookLog.Services {
    public class BookService {
        private ApplicationDbContext _dbContext;

        public BookService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<BookListDto>> GetAllAsync() {
            var allBooks = await _dbContext.Books.Include(b => b.Authors).Include(b => b.Genres)
                .ToListAsync();
            var bookListDtos = new List<BookListDto>();
            foreach (var book in allBooks) {
                bookListDtos.Add(ModelToListDto(book));
            }
            return bookListDtos;
        }

        public async Task CreateAsync(BookCreateEditDto newBook) {
            await _dbContext.Books.AddAsync(await CreateEditDtoToModel(newBook));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<BookCreateEditDto?> GetByIdAsync(int id) {
            var book = await _dbContext.Books.Include(b => b.Authors).Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == id);
            return book != null ? await ModelToCreateEditDto(book) : null;
        }

        public async Task UpdateAsync(BookCreateEditDto bookCreateEditDto) {
            if (bookCreateEditDto.Id == null) {
                throw new ArgumentNullException("Book Id is required");
            }
            var book = await _dbContext.Books.Include(b => b.Authors).Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.Id == bookCreateEditDto.Id.Value);
            if (book == null) {
                throw new ArgumentNullException("Book not found");
            }
            book.Title = bookCreateEditDto.Title;
            book.Authors.Clear();
            var authors = await _dbContext.Authors.Where(a => bookCreateEditDto.SelectedAuthorIds.Contains(a.Id))
                .ToListAsync();
            foreach (var author in authors) {
                book.Authors.Add(author);
            }
            book.Genres.Clear();
            var genres = await _dbContext.Genres.Where(g => bookCreateEditDto.SelectedGenreIds.Contains(g.Id))
                .ToListAsync();
            foreach (var genre in genres) {
                book.Genres.Add(genre);
            }
            book.CoverImageUrl = bookCreateEditDto.CoverImageUrl;
            book.DatabazeKnihUrl = bookCreateEditDto.DatabazeKnihUrl;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id) {
            var bookToDelete = await _dbContext.Books.FindAsync(id);
            if (bookToDelete == null) {
                return false;
            }
            _dbContext.Books.Remove(bookToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        private BookListDto ModelToListDto(Book book) {
            return new BookListDto() {
                Id = book.Id,
                Title = book.Title,
                Authors = book.Authors
                    .Select(a => string.Join(' ', new[] { a.FirstName, a.MiddleName, a.LastName }
                    .Where(x => !string.IsNullOrWhiteSpace(x)))).ToList(),
                Genres = book.Genres.Select(g => g.Name).ToList(),
                CoverImageUrl = book.CoverImageUrl,
                DatabazeKnihUrl = book.DatabazeKnihUrl,
            };
        }

        private async Task<Book> CreateEditDtoToModel(BookCreateEditDto bookCreateEditDto) {
            var authors = await _dbContext.Authors
                .Where(a => bookCreateEditDto.SelectedAuthorIds.Contains(a.Id))
                .ToListAsync();
            var genres = await _dbContext.Genres
                .Where(g => bookCreateEditDto.SelectedGenreIds.Contains(g.Id))
                .ToListAsync();
            return new Book() {
                Title = bookCreateEditDto.Title,
                Authors = authors,
                Genres = genres,
                CoverImageUrl= bookCreateEditDto.CoverImageUrl,
                DatabazeKnihUrl = bookCreateEditDto.DatabazeKnihUrl,
            };
        }

        private async Task<BookCreateEditDto> ModelToCreateEditDto(Book book) {
            var authors = await _dbContext.Authors.OrderBy(a => a.LastName).ToListAsync();
            var genres = await _dbContext.Genres.OrderBy(g => g.Name).ToListAsync();
            return new BookCreateEditDto() {
                Id = book.Id,
                Title = book.Title,
                SelectedAuthorIds = book.Authors.Select(a => a.Id).ToList(),
                SelectedGenreIds = book.Genres.Select(g => g.Id).ToList(),

                Authors = authors.Select(a => new AuthorDto {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                }).ToList(),

                Genres = genres.Select(g => new GenreDto {
                    Id = g.Id,
                    Name = g.Name,
                }).ToList(),

                CoverImageUrl = book.CoverImageUrl,
                DatabazeKnihUrl = book.DatabazeKnihUrl,
            };
        }
    }
}
