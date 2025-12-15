using BookLog.Dtos;
using BookLog.Models;
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
    }
}
