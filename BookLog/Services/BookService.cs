using BookLog.Dtos;
using BookLog.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLog.Services {
    public class BookService {
        ApplicationDbContext _dbContext;

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
    }
}
